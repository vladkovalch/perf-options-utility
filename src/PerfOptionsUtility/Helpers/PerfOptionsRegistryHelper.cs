using PerfOptionsUtility.Enums;
using PerfOptionsUtility.Interfaces;
using System;
using System.IO;
using System.Text;

namespace PerfOptionsUtility.Service
{
    public class PerfOptionsRegistryHelper : IPerfOptionsRegistryHelper
    {
        private readonly IFileManager _fileManager;

        public PerfOptionsRegistryHelper(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string GenerateRegistryFileForPerfOptions(string filePath, string fileName, PerfOptions perfOption)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Windows Registry Editor Version 5.00");
                sb.AppendLine();
                sb.AppendLine($@"[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\{fileName}\PerfOptions]");
                sb.AppendLine($"\"CpuPriorityClass\" = dword:0000000{(int)perfOption}");

                string file = Path.GetFileNameWithoutExtension(fileName).Replace(" ", "");
                string path = $@"{filePath}\{file}{perfOption}Priority.reg";

                _fileManager.CreateFile(path, sb.ToString());

                return $"{file}{perfOption}Priority.reg";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
