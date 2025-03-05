using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Security;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace HTTPInterceptor
{
    public partial class MainForm : Form
    {
        private Process? targetProcess;
        private List<string> domains = new List<string>();
        private bool isRunning = false;
        private bool isEnglish = false;
        private LogForm? logForm;

        private ProxyServer proxyServer = null!;
        private ExplicitProxyEndPoint explicitEndPoint = null!;
        private CancellationTokenSource? cts;

        public MainForm()
        {
            InitializeComponent();
            DetermineLanguage();
            LocalizeUI();
            this.FormClosing += MainForm_FormClosing;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private async void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (isRunning)
            {
                await StopProxy();
            }
            logForm?.Close();
        }

        private void DetermineLanguage()
        {
            string culture = CultureInfo.CurrentUICulture.Name;
            isEnglish = culture.StartsWith("en", StringComparison.OrdinalIgnoreCase);
        }

        private void LocalizeUI()
        {
            lblExePath.Text = isEnglish ? "Target Application:" : "目標程式:";
            btnBrowse.Text = isEnglish ? "Browse..." : "瀏覽...";
            lblTarget.Text = isEnglish ? "Target Address:" : "目標地址:";
            lblOrigin.Text = isEnglish ? "Origin Domains:" : "原始域名列表:";
            btnControl.Text = isRunning ? (isEnglish ? "Stop" : "停止")
                                        : (isEnglish ? "Start" : "開始");
            lblStatus.Text = isEnglish ? $"Proxy Status: {(isRunning ? "Running" : "Stopped")}"
                                       : $"Proxy 狀態: {(isRunning ? "運行中" : "已停止")}";
            this.Text = isEnglish ? "Advanced Traffic Management Tool" : "高級流量管理工具";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = isEnglish ? "Executable Files|*.exe" : "可執行檔|*.exe";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtExePath.Text = dlg.FileName;
                }
            }
        }

        private async void btnControl_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                await StopProxy();
            }
            else
            {
                if (!ValidateInputs()) return;
                await StartProxy();
            }
            UpdateUIState();
            LocalizeUI();
        }

        private bool ValidateInputs()
        {
            if (!File.Exists(txtExePath.Text))
            {
                MessageBox.Show(isEnglish ? "Please select a valid executable file." : "請選擇有效的可執行檔");
                return false;
            }
            if (!ParseTargetAddress())
            {
                MessageBox.Show(isEnglish ? "Invalid target address format (e.g., localhost:443)" : "無效的目標地址格式 (正確範例: localhost:443)");
                return false;
            }
            if (txtOriginList.Text == null)
            {
                MessageBox.Show(isEnglish ? "Domain list cannot be empty." : "域名列表不能為空");
                return false;
            }
            return true;
        }

        private bool ParseTargetAddress()
        {
            var parts = txtTarget.Text.Split(':');
            return parts.Length == 2 && int.TryParse(parts[1], out _);
        }

        private async Task StartProxy()
        {
            try
            {

                domains = txtOriginList.Text.Split(
                    new[] { '\n', ',', ' ' },
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                ).ToList();

                cts = new CancellationTokenSource();
                proxyServer = new ProxyServer();


                proxyServer.CertificateManager.EnsureRootCertificate();

                proxyServer.ServerCertificateValidationCallback += OnCertificateValidation;
                proxyServer.ClientCertificateSelectionCallback += OnCertificateSelection;
                proxyServer.BeforeRequest += OnRequest;
                proxyServer.BeforeResponse += OnResponse;

                explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8888, true);
                explicitEndPoint.BeforeTunnelConnectRequest += OnBeforeTunnelConnectRequest;

                proxyServer.AddEndPoint(explicitEndPoint);
                proxyServer.Start();


                logForm = new LogForm();
                logForm.Show();
                StartTargetProcess();

                isRunning = true;
                AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ? "Proxy started." : "Proxy 啟動。"));
            }
            catch (Exception ex)
            {
                AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ? $"Start Proxy Error: {ex}" : $"Proxy 啟動錯誤: {ex}"));
            }
            await Task.CompletedTask;
        }

        private Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            e.IsValid = e.SslPolicyErrors == SslPolicyErrors.None;
            return Task.CompletedTask;
        }

        private Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
        {
            e.DecryptSsl = true;
            await Task.CompletedTask;
        }

        private async Task OnRequest(object sender, SessionEventArgs e)
        {
            try
            {
                string originalHost = e.HttpClient.Request.RequestUri.Host;
                bool shouldRedirect = domains.Any(d =>
                    originalHost.EndsWith(d.Replace("*.", ""), StringComparison.OrdinalIgnoreCase));

                if (shouldRedirect)
                {
                    var targetParts = txtTarget.Text.Split(':');
                    if (targetParts.Length == 2 && int.TryParse(targetParts[1], out int targetPort))
                    {
                        string targetHost = targetParts[0];

                        var uriBuilder = new UriBuilder(e.HttpClient.Request.RequestUri)
                        {
                            Scheme = "https",
                            Host = targetHost,
                            Port = targetPort
                        };

                        uriBuilder.Path = e.HttpClient.Request.RequestUri.AbsolutePath;
                        uriBuilder.Query = e.HttpClient.Request.RequestUri.Query;

                        e.HttpClient.Request.Url = uriBuilder.Uri.ToString();

                        AddLog($"[{DateTime.Now:HH:mm:ss}] 重定向 {originalHost} -> {targetHost}:{targetPort}");
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog($"[{DateTime.Now:HH:mm:ss}] 請求處理錯誤: {ex.Message}");
            }
            await Task.CompletedTask;
        }

        private async Task OnResponse(object sender, SessionEventArgs e)
        {
            try
            {
                if (e.HttpClient.Response.StatusCode >= 400)
                {
                    string requestBody = string.Empty;
                    string responseBody = string.Empty;

                    var method = e.HttpClient.Request.Method.ToUpperInvariant();
                    if (method is "POST" or "PUT" or "PATCH" && e.HttpClient.Request.HasBody)
                    {
                        requestBody = await e.GetRequestBodyAsString();
                    }

                    if (e.HttpClient.Response.HasBody)
                    {
                        responseBody = await e.GetResponseBodyAsString();
                    }

                    AddLog($"[錯誤詳情]\n" +
                           $"請求URL: {e.HttpClient.Request.Url}\n" +
                           $"請求頭: {string.Join("\n", e.HttpClient.Request.Headers)}\n" +
                           $"請求體: {requestBody}\n" +
                           $"狀態碼: {e.HttpClient.Response.StatusCode}\n" +
                           $"響應頭: {string.Join("\n", e.HttpClient.Response.Headers)}\n" +
                           $"響應體: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ?
                    $"Response error: {ex}" :
                    $"回應錯誤: {ex}"));
            }
            await Task.CompletedTask;
        }

        private void StartTargetProcess()
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = txtExePath.Text,
                    UseShellExecute = false,
                    Environment =
                    {
                        ["HTTP_PROXY"] = "http://127.0.0.1:8888",
                        ["HTTPS_PROXY"] = "http://127.0.0.1:8888",
                        ["NO_PROXY"] = "localhost,127.0.0.1",
                        ["DOTNET_SYSTEM_NET_HTTP_USEPORTINSPN"] = "1",
                        ["DOTNET_SYSTEM_NET_HTTP_USEPROXY"] = "1"
                    }
                };

                startInfo.WorkingDirectory = Path.GetDirectoryName(txtExePath.Text)!;

                targetProcess = Process.Start(startInfo);
                AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ? "Target process started." : "目標程式啟動。"));
            }
            catch (Exception ex)
            {
                AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ?
                    $"Error starting target process: {ex}" :
                    $"啟動目標程式錯誤: {ex}"));
            }
        }

        private async Task StopProxy()
        {
            try
            {
                cts?.Cancel();

                if (proxyServer != null)
                {
                    explicitEndPoint.BeforeTunnelConnectRequest -= OnBeforeTunnelConnectRequest;
                    proxyServer.BeforeRequest -= OnRequest;
                    proxyServer.BeforeResponse -= OnResponse;
                    proxyServer.Stop();
                }

                if (targetProcess != null && !targetProcess.HasExited)
                {
                    try
                    {
                        targetProcess.Kill();
                        await Task.Run(() => targetProcess.WaitForExit());
                        targetProcess.Dispose();
                        AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ? "Target process terminated." : "目標程式已結束。"));
                    }
                    catch (Exception ex)
                    {
                        AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ?
                            $"Error stopping target process: {ex}" :
                            $"停止目標程式錯誤: {ex}"));
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ?
                    $"Stop Proxy Error: {ex}" :
                    $"Proxy 停止錯誤: {ex}"));
            }
            finally
            {
                isRunning = false;
                AddLog($"[{DateTime.Now:HH:mm:ss}] " + (isEnglish ? "Proxy stopped." : "Proxy 停止。"));
                logForm?.Close();
            }
        }

        private void UpdateUIState()
        {
            btnControl.Text = isRunning ? (isEnglish ? "Stop" : "停止")
                                        : (isEnglish ? "Start" : "開始");
            lblStatus.Text = isEnglish ? $"Proxy Status: {(isRunning ? "Running" : "Stopped")}"
                                       : $"Proxy 狀態: {(isRunning ? "運行中" : "已停止")}";
        }

        private void AddLog(string message)
        {
            if (logForm != null && !logForm.IsDisposed)
            {
                logForm.Invoke((Action)(() =>
                {
                    logForm.AppendLog(message);
                }));
            }
            Debug.WriteLine(message);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblExePath_Click(object sender, EventArgs e)
        {
        }

        private void txtExePath_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblTarget_Click(object sender, EventArgs e)
        {

        }

        private void lblOrigin_Click(object sender, EventArgs e)
        {

        }
    }
}
