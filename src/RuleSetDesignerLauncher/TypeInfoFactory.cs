using System;
using System.Collections.Generic;
using System.Linq;

namespace RuleSetDesignerLauncher
{
    internal class TypeInfoFactory
    {
        public static TypeInfo[] Get(IEnumerable<string> filePaths)
        {
            var loaderDomain = AppDomainFactory.Create();
            try
            {
                var factory = CreateInternalFactory(loaderDomain, filePaths);
                loaderDomain.DoCallBack(factory.Get);
                var retval = factory.Result;
                return retval;
            }
            finally
            {
                AppDomain.Unload(loaderDomain);
            }
        }

        private static InternalFactory CreateInternalFactory(AppDomain appDomain, IEnumerable<string> filePaths)
        {
            var type = typeof(InternalFactory);
            // ReSharper disable once AssignNullToNotNullAttribute
            var retval = (InternalFactory) appDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            retval.FilePaths = filePaths;
            return retval;
        }

        private class InternalFactory : MarshalByRefObject
        {
            public IEnumerable<string> FilePaths;
            public TypeInfo[] Result;

            public void Get()
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (sender, args) => Assembly.ReflectionOnlyLoad(args.Name);
                Result = FilePaths.Select(x => new {assembly = TryReflectionOnlyLoadFrom(x), fileName = x})
                    .Where(x => x.assembly != null).ToList()
                    .SelectMany(x => TryGetTypes(x.assembly).Select(t => TryGetTypeInfo(t, x.fileName)))
                    .Where(x => x != null).ToArray();
            }

            private static TypeInfo TryGetTypeInfo(System.Type t, string fileName)
            {
                try
                {
                    return new TypeInfo {FullName = t.FullName, FilePath = fileName};
                }
                catch
                {
                    return null;
                }
            }

            private static IEnumerable<System.Type> TryGetTypes(System.Reflection.Assembly assembly)
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch
                {
                    return new System.Type[] { };
                }
            }

            private static System.Reflection.Assembly TryReflectionOnlyLoadFrom(string filePath)
            {
                try
                {
                    return System.Reflection.Assembly.ReflectionOnlyLoadFrom(filePath);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
