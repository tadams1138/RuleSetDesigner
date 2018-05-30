namespace RuleSetDesignerLauncher
{
    internal static class Assembly
    {
        public static System.Reflection.Assembly TryLoadFile(string file)
        {
            try
            {
                return System.Reflection.Assembly.LoadFile(file);
            }
            catch
            {
                return null;
            }
        }
    }
}