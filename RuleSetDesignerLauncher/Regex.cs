using System.Text.RegularExpressions;

namespace RuleSetDesignerLauncher
{
    internal static class Regex
    {
        public static System.Text.RegularExpressions.Regex TryCreate(string pattern, RegexOptions regexOptions)
        {
            try
            {
                return new System.Text.RegularExpressions.Regex(pattern, regexOptions);
            }
            catch
            {
                return null;
            }
        }
    }
}