using System.Collections.Generic;
using System.Linq;

namespace RuleSetDesignerLauncher
{
    internal static class Directory
    {
        public static IEnumerable<string> GetFiles(IEnumerable<string> searchPatterns)
        {
            var folder = System.IO.Directory.GetCurrentDirectory();
            return searchPatterns.SelectMany(x => System.IO.Directory.GetFiles(folder, x));
        }
    }
}