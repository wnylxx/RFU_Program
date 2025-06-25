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

namespace RemoteFileUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // local.json 파일 읽기
        private Dictionary<string, string> LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show($"설정 파일이 없습니다: {path}");
                return new Dictionary<string, string>();
            }

            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        private string GetConfigOrError(Dictionary<string, string> config, string key)
        {
            if (!config.TryGetValue(key, out string value) || string.IsNullOrWhiteSpace(value))
            {
                throw new Exception($"설정 파일에 '{key}' 값이 누락되어 있습니다.");
            }
            return value;
        }


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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string configPath = Path.Combine(Application.StartupPath, "config.local.json");
            var config = LoadConfig(configPath);
            string ip, port;
            try
            {
                ip = GetConfigOrError(config, "serverIP");
                port = GetConfigOrError(config, "serverPort");
                MessageBox.Show(ip);
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
