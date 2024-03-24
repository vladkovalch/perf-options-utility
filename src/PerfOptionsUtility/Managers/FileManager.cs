using PerfOptionsUtility.Interfaces;
using System;
using System.IO;
using System.Text;

namespace PerfOptionsUtility.Managers
{
    public class FileManager : IFileManager
    {
        public bool CreateFile(string filePath)
        {
            using (FileStream fileStream = File.Create(filePath)) { }
            return true;
        }

        public bool CreateFile(string filePath, string text)
        {
            using (FileStream fileStream = File.Create(filePath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(text);
                fileStream.Write(info, 0, info.Length);
            }

            return true;
        }

        public string ReadFile(string filePath)
        {
            string info = string.Empty;
            using (StreamReader streamReader = File.OpenText(filePath))
            {
                while ((info = streamReader.ReadLine()) != null)
                {
                    Console.WriteLine(info);
                }
            }

            return info;
        }

        public bool WriteFile(string filePath, string text)
        {
            using (FileStream fileStream = File.OpenWrite(filePath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(text);
                fileStream.Write(info, 0, info.Length);
            }

            return true;
        }
    }
}
