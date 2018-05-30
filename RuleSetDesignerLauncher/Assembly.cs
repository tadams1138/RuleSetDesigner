using System.IO;
using System.Reflection;

namespace RuleSetDesignerLauncher
{
    internal static class Assembly
    {
        public static System.Reflection.Assembly TryReflectionOnlyLoadFrom(string file)
        {
            try
            {
                return System.Reflection.Assembly.ReflectionOnlyLoadFrom(file);
            }
            catch
            {
                return null;
            }
        }

        public static System.Reflection.Assembly Load(string assemblyName)
        {
            var assemblyFileName = GetLibraryName(assemblyName);
            if (File.Exists(assemblyFileName))
            {
                return System.Reflection.Assembly.LoadFrom(assemblyFileName);
            }

            return System.Reflection.Assembly.Load(assemblyName);
        }

        public static System.Reflection.Assembly ReflectionOnlyLoad(string assemblyName)
        {
            var assemblyFileName = GetLibraryName(assemblyName);
            if (File.Exists(assemblyFileName))
            {
                return System.Reflection.Assembly.ReflectionOnlyLoadFrom(assemblyFileName);
            }

            return System.Reflection.Assembly.ReflectionOnlyLoad(assemblyName);
        }

        private static string GetLibraryName(string assemblyName)
        {
            var name = new AssemblyName(assemblyName);
            var retval = name.Name + ".dll";
            return retval;
        }
    }
}