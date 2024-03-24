using System;
using System.IO;

namespace PerfOptionsManager
{
	public enum PerfOptions
	{
		Idle = 1,
		Normal,
		High,
		Real_Time,
		Below_Normal,
		Above_Normal
	}

	public class PerfOptionsMngr
	{
		private IFileManager fileManager;
		public PerfOptionsMngr() => fileManager = new FileManager();

		public string ChangePerfOptions(string filePath, string fileName, PerfOptions perfOption)
		{
			try
			{
				string text =
					@"Windows Registry Editor Version 5.00" +
					Environment.NewLine + Environment.NewLine +
					$@"[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\{fileName}\PerfOptions]" +
					Environment.NewLine +
					$"\"CpuPriorityClass\" = dword:0000000{(int)perfOption}";

				string file = Path.GetFileNameWithoutExtension(fileName).Replace(" ", "");
				string path = $@"{filePath}\{file}{perfOption.ToString()}Priority.reg";

				fileManager.CreateFile(path, text);
				return $"{file}{perfOption.ToString()}Priority.reg";
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}
    }
}
