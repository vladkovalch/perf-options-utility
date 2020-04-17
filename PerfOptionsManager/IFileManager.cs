namespace PerfOptionsManager
{
	public interface IFileManager
	{
		bool CreateFile(string filePath);
		bool CreateFile(string filePath, string text);
		string ReadFile(string filePath);
		bool WriteFile(string filePath, string text);
	}
}
