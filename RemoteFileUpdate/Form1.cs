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
                    gridFiles.Rows[rowIndex].Cells[0].Value = file;
                    gridFiles.Rows[rowIndex].Cells[1].Value = "/home/pi/jdconnect/"; // 기본 경로
                }
            }
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            string project = comboProject.SelectedItem?.ToString();
            string version = txtVersion.Text.Trim();

            if (string.IsNullOrEmpty(project) || string.IsNullOrEmpty(version))
            {
                MessageBox.Show("프로젝트와 버전을 입력해주세요.");
                return;
            }

            string ip = AppConfig.ServerIP;
            string port = AppConfig.ServerPort;

            string uploadUrl = $"http://{ip}:{port}/api/upload";

            // 파일 목록 확인
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
                return;
            }

            // 임시 디렉토리 생성 (중복방지를 위한 GUID 기반 새폴더 생성)
            string tempDir = Path.Combine(Path.GetTempPath(), "Uploader_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            string manifestPath = Path.Combine(tempDir, "manifest.txt");

            // 파일 복사 및 manifest 작성
            using (var writer = new StreamWriter(manifestPath, false, Encoding.UTF8))
            {
                foreach (var (file, dest) in files)
                {
                    string destFileName = Path.GetFileName(file);
                    string destPath = dest.Trim().Replace("\\", "/");

                    
                    // 경로가 "/"로 끝나지 않으면 붙혀주기
                    if(!destPath.EndsWith("/"))
                    {
                        destPath += "/";
                    }

                    string fullDestPath = destPath + destFileName;

                    // 파일을 임시 디렉토리로 복사
                    string tempDestFile = Path.Combine(tempDir, destFileName);
                    File.Copy(file, tempDestFile, true);

                    // manifest.txt 작성
                    writer.WriteLine($"{destFileName}:{fullDestPath}");
                }
            }

            // zip 압축
            string zipPath = Path.Combine(Path.GetTempPath(), $"{version}.zip");
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }
            ZipFile.CreateFromDirectory(tempDir, zipPath);

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
                        WriteLog("업로드 성공");

                        await FetchAndSetVersionAsync(project, lblVersion);
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
            Directory.Delete(tempDir, true);
            File.Delete(zipPath);
        }

        private async void comboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProject = comboProject.SelectedItem.ToString();
            await FetchAndSetVersionAsync(selectedProject, lblVersion);

            string ip = AppConfig.ServerIP;
            string port = AppConfig.ServerPort;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"http://{ip}:{port}/api/connected-devices");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(json);
                    var devices = parsed["projects"]?[selectedProject];

                    if (devices != null)
                    {
                        int count = devices.Count();
                        WriteLog($"[프로젝트: {selectedProject}] 총 연결된 장비 수: {count}");
                        foreach (var device in devices)
                        {
                            WriteLog($" - Device ID: {device.Path}");
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

        
    }
}
