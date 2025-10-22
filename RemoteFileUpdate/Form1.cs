using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net.Http;
//using System.Linq;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;


namespace RemoteFileUpdate
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            try
            {
                AppConfig.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 로딩 실패: " + ex.Message);
                Environment.Exit(1);
            }
        }

        public class DeviceStatus
        {
            public string deviceId { get; set; }
            public bool isConnected { get; set; }
            public string version { get; set; }
            public string lastUpdateStatus { get; set; }
        }

        // upload 전 version과 파일 업로드 확인
        private bool TryGetProjectAndVersion(out string project, out string version)
        {
            project = comboProject.SelectedItem?.ToString();
            version = txtVersion.Text.Trim();
            string versionPatten = @"^\d+\.\d+\.\d+$"; // 유효성 검사 0.0.0 형태

            if (string.IsNullOrEmpty(project) || string.IsNullOrEmpty(version))
            {
                MessageBox.Show("프로젝트와 버전을 입력해주세요.");
                return false;
            }

            if (!Regex.IsMatch(version, versionPatten))
            {
                MessageBox.Show("버전 형식이 잘못되었습니다. 예: 1.0.0");
                return false;
            }

            WriteLog("파일 업로드 시작");

            return true;
        }

        // 파일 압축 및 업로드
        private async Task<string> CreateUpdateZipAsync(string project, string version)
        {
            // 파일 목록 수집
            var files = new List<(string FilePath, string TargetPath)>();
            foreach (DataGridViewRow row in gridFiles.Rows)
            {
                string file = row.Cells[0].Value?.ToString();
                string dest = row.Cells[1].Value?.ToString();
                if (!string.IsNullOrEmpty(file) && !string.IsNullOrEmpty(dest))
                {
                    files.Add((file, dest));
                }
            }

            if (files.Count == 0)
            {
                MessageBox.Show("업로드할 파일이 없습니다.");
                return null;
            }

            // 임시 디렉토리 및 manifest 생성
            string tempDir = Path.Combine(Path.GetTempPath(), "Uploader_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            string manifestPath = Path.Combine(tempDir, "manifest.txt");

            using (var writer = new StreamWriter(manifestPath, false, Encoding.UTF8))
            {
                foreach (var (file, dest) in files)
                {
                    string destFileName = Path.GetFileName(file);
                    string destPath = dest.Trim().Replace("\\", "/");

                    if (!destPath.EndsWith("/"))
                        destPath += "/";

                    string fullDestPath = destPath + destFileName;
                    string tempDestFile = Path.Combine(tempDir, destFileName);
                    File.Copy(file, tempDestFile, true);

                    writer.WriteLine($"{destFileName}:{fullDestPath}");
                }
            }

            // zip 압축
            string zipPath = Path.Combine(Path.GetTempPath(), $"{version}.zip");
            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(tempDir, zipPath);
            Directory.Delete(tempDir, true); // 임시 폴더 삭제

            WriteLog("파일 압축 성공!");


            // cleanup은 호출한 쪽에서 할 수 있도록 zipPath만 반환
            return zipPath;

        }


        // project-version 받아오기
        public async Task FetchAndSetVersionAsync(string selectedProjectName, Label targetLabel)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string ip = AppConfig.ServerIP;
                    string port = AppConfig.ServerPort;

                    string apiUrl = $"http://{ip}:{port}/api/version?project={selectedProjectName}";
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(json);

                    if (obj["version"] != null)
                    {
                        string version = obj["version"].ToString();
                        lblVersion.Text = version;
                    }
                    else if (obj["message"] != null)
                    {
                        lblVersion.Text = obj["message"].ToString(); // "버전 정보가 없습니다."
                    }
                    else
                    {
                        lblVersion.Text = "알 수 없는 응답";
                    }
                }
                catch (HttpRequestException ex)
                {
                    targetLabel.Text = "서버 오류";
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void WriteLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() =>
                {
                    AppendLog(message);
                }));
            }
            else
            {
                AppendLog(message);
            }
        }

        private void AppendLog(string message)
        {
            string timeStampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
            txtLog.AppendText(timeStampedMessage);
        }


        private System.Windows.Forms.Timer updateSummaryTimer;
        private string currentProject = "";
        private bool updateCompleted = false;

        private void StartUpdateMonitoring(string project)
        {
            currentProject = project;
            updateCompleted = false;

            updateSummaryTimer = new System.Windows.Forms.Timer();
            updateSummaryTimer.Interval = 3000; // 3초
            updateSummaryTimer.Tick += async (s, e) => await CheckUpdateSummary();
            updateSummaryTimer.Start();
        }

        private async Task CheckUpdateSummary()
        {
            if (updateCompleted) return;

            string ip = AppConfig.ServerIP;
            string port = AppConfig.ServerPort;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"http://{ip}:{port}/api/update-summary?project={currentProject}");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(json);

                    var status = parsed["status"]?.ToString();

                    if (status == "progress")
                    {
                        int total = parsed["total"]?.Value<int>() ?? 0;
                        int received = parsed["received"]?.Value<int>() ?? 0;

                        WriteLog($"[진행 중] {received}/{total} 응답 도착");
                        return;
                    }

                    if (status == "done")
                    {
                        int total = parsed["total"]?.Value<int>() ?? 0;
                        int success = parsed["success"]?.Value<int>() ?? 0;
                        int failure = parsed["failure"]?.Value<int>() ?? 0;

                        updateCompleted = true;
                        updateSummaryTimer.Stop();

                        WriteLog($"업데이트 완료: 총 {total}대, 성공 {success}대, 실패 {failure}대");

                        var failedDevices = parsed["failedDevices"];
                        if (failedDevices != null && failedDevices.Any())
                        {
                            WriteLog("실패한 장비 목록");
                            foreach (var device in failedDevices)
                            {
                                WriteLog($"    - {device}");
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog($"[오류] 업데이트 상태 조회 실패: {ex.Message}");
            }
        }





        // Component
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "업로드할 파일 선택"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    int rowIndex = gridFiles.Rows.Add();
                    gridFiles.Rows[rowIndex].Cells[0].Value = Path.GetFileName(file);
                    gridFiles.Rows[rowIndex].Cells[1].Value = "/home/pi/jdconnect/"; // 기본 경로
                }
            }
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            // 유효성 검사
            if (!TryGetProjectAndVersion(out string project, out string version)) return;

            // 파일 압축
            string zipPath = await CreateUpdateZipAsync(project, version);
            if (zipPath == null) return;


            string ip = AppConfig.ServerIP;
            string port = AppConfig.ServerPort;

            string uploadUrl = $"http://{ip}:{port}/api/upload";

            // 전송
            using (var client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(project), "project");
                content.Add(new StringContent(version), "version");
                content.Add(new StreamContent(File.OpenRead(zipPath)), "file", $"{version}.zip");

                try
                {
                    var response = await client.PostAsync(uploadUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        WriteLog($"{version} 업로드 성공");

                        await FetchAndSetVersionAsync(project, lblVersion);

                        StartUpdateMonitoring(project);
                    }
                    else
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        WriteLog("업로드 실패");
                        MessageBox.Show("업로드 실패");
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("전송 중 오류 발생");
                    MessageBox.Show("전송 중 오류 발생: " + ex.Message);
                }
            }

            // 정리
            //Directory.Delete(tempDir, true);
            File.Delete(zipPath);
        }

        private async void comboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProject = comboProject.SelectedItem.ToString();
            await FetchAndSetVersionAsync(selectedProject, lblVersion);

            string ip = AppConfig.ServerIP;
            string port = AppConfig.ServerPort;

            string apiUrl = $"http://{ip}:{port}/api/devices/{selectedProject}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var deviceList = JsonConvert.DeserializeObject<List<DeviceStatus>>(jsonResponse);


                    gridDevices.Rows.Clear();

                    foreach (var device in deviceList)
                    {
                        gridDevices.Rows.Add(
                            device.deviceId,
                            device.isConnected ? "연결됨" : "미연결",
                            device.version ?? "-",
                            device.lastUpdateStatus ?? "-"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog($"Device 목록을 불러오는 중 오류가 발생했습니다. {ex.Message}");
            }




            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"http://{ip}:{port}/api/connected-devices");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(json);
                    var devicesToken = parsed["projects"]?[selectedProject];

                    if (devicesToken != null && devicesToken.Type == JTokenType.Object)
                    {
                        var devicesObj = (JObject)devicesToken;
                        var sortedDevices = devicesObj.Properties()
                            .OrderBy(p => p.Name)
                            .ToList();

                        int count = sortedDevices.Count();
                        WriteLog($"[프로젝트: {selectedProject}] 총 연결된 장비 수: {count}");
                        foreach (var device in sortedDevices)
                        {
                            WriteLog($"    - Device ID: {device.Name}");
                        }
                    }
                    else
                    {
                        WriteLog($"[프로젝트: {selectedProject}] 연결된 장비가 없습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog($"[오류] 장비 목록 조회 실패: {ex.Message}");
            }
        }


        private async void btnRollbackOnly_Click(object sender, EventArgs e)
        {
            string project = comboProject.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(project))
            {
                MessageBox.Show("프로젝트를 선택해 주세요.");
                return;
            }

            string ip = AppConfig.ServerIP;
            string port = AppConfig.ServerPort;
            string url = $"http://{ip}:{port}/api/rollback-only";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(new { project }), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    var responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        WriteLog("롤백 명령 전송 완료");
                        StartUpdateMonitoring(project);
                    }
                    else
                    {
                        WriteLog("롤백 실패: " + responseBody);
                        MessageBox.Show("롤백 실패");
                    }

                }
            }
            catch (Exception ex)
            {
                WriteLog("롤백 명령 오류: " + ex.Message);
            }
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            gridFiles.Rows.Clear();
        }
    }
}
