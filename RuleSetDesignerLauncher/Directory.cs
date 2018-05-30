using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RuleSetDesignerLauncher
{
    internal static class Directory
    {
        public static IEnumerable<string> GetFiles(string folder, IEnumerable<string> searchPatterns,
            SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => System.IO.Directory.GetFiles(folder, x, searchOption));
        }
    }
}