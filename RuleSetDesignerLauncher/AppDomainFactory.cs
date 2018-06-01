using System;

namespace RuleSetDesignerLauncher
{
    internal static class AppDomainFactory
    {
        public static AppDomain Create()
        {
            var info = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                ShadowCopyFiles = null
            };
            var retval = AppDomain.CreateDomain($"appDomain{Guid.NewGuid()}", AppDomain.CurrentDomain.Evidence, info);
            return retval;
        }

        public static AppDomain CreateWithShadowCopy()
        {
            var info = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                ShadowCopyFiles = "true",
                ShadowCopyDirectories = System.IO.Directory.GetCurrentDirectory()
            };
            var retval = AppDomain.CreateDomain($"appDomain{Guid.NewGuid()}", AppDomain.CurrentDomain.Evidence, info);
            return retval;
        }
    }
}