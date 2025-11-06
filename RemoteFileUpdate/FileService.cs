using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace RemoteFileUpdate
{
    // 압축파일 만들기 
    public class FileGridItem
    {
        public string FilePath { get; set; }
        public string TargetPath { get; set; }
    }

    public static class FileService
    {
        public static string CreateUpdateZip(string version, IEnumerable<FileGridItem> files)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "Uploader_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            string manifestPath = Path.Combine(tempDir, "manifest.txt");

            using (var writer = new StreamWriter(manifestPath, false, Encoding.UTF8))
            {
                foreach (var item in files) 
                {
                    string destFileName = Path.GetFileName(item.FilePath);    // file 이름 + 확장자
                    string destPath = item.TargetPath.Trim().Replace("\\", "/");  // Rpi 폴더 경로

                    if (!destPath.EndsWith("/"))
                        destPath += "/";

                    string fullDestPath = destPath + destFileName; // Rpi 폴더 경로 + file 이름.확장자
                    string tempDestFile = Path.Combine(tempDir, destFileName);  // 임시폴더는 왜?
                    File.Copy(item.FilePath, tempDestFile, true);

                    writer.WriteLine($"{destFileName}:{fullDestPath}");  // 파일이름 : Rpi 폴더 경로
                }
            }

            string zipPath = Path.Combine(Path.GetTempPath(), $"{version}.zip");
            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(tempDir, zipPath);
            Directory.Delete(tempDir, true); // 임시 폴더 삭제

            return zipPath;
        }
    }
}
