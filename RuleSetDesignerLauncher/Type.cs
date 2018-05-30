namespace RuleSetDesignerLauncher
{
    internal static class Type
    {
        public static System.Type GetTypeFromReflectionOnlyType(System.Type reflectionOnlyType)
        {
            var assembly = System.Reflection.Assembly.Load(reflectionOnlyType.Assembly.FullName);
            // ReSharper disable once AssignNullToNotNullAttribute
            var retval = assembly.GetType(reflectionOnlyType.FullName);
            return retval;
        }
    }
}