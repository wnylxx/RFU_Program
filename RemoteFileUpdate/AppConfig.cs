using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


// 설정 로딩 전용 클래스
public static class AppConfig
{
    public static string ServerIP { get; private set; }
    public static string ServerPort { get; private set; }

    public static void Load()
    {
        string path = Path.Combine(Application.StartupPath, "config.local.json");

        if(!File.Exists(path))
        {
            MessageBox.Show($"설정 파일이 없습니다: {path}");
            Environment.Exit(1);
        }

        var json = File.ReadAllText(path);
        var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        ServerIP = GetConfigOrError(config, "serverIP");
        ServerPort = GetConfigOrError(config, "serverPort");
    }

    private static string GetConfigOrError(Dictionary<string, string> config, string key)
    {
        if (!config.TryGetValue(key, out string value) || string.IsNullOrWhiteSpace(value))
        {
            throw new Exception($"설정 파일에 '{key}' 값이 누락되어 있습니다.");
        }
        return value;
    }
    
    // Form1.cs에서 컴포넌트 초기화 할 때 읽어오기
    // 근데, Form1.cs에서 전역 변수로 선언해서 여러 함수에 사용할려고 했지만,
    // AppConfig.ServerIP는 실행 도중에 로드되는 값이고, const는 코드가 컴파일 될 때 값이 있어야 하므로,
    // 필드 변수로 필요한 곳에서 IP = AppConfig.ServerIP 이런식으로 사용해야한다.
}