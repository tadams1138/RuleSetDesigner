using System.IO;

namespace RuleSetDesignerLauncher
{
    internal static class TestHelper
    {
        public static string GetFullPath(string fileName)
        {
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var retval = Path.Combine(currentDirectory, fileName);
            return retval;
        }
    }
}