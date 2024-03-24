using PerfOptionsUtility.Enums;

namespace PerfOptionsUtility.Interfaces
{
    public interface IPerfOptionsRegistryHelper
    {
        string GenerateRegistryFileForPerfOptions(string filePath, string fileName, PerfOptions perfOption);
    }
}
