using System;
using System.Windows.Forms;

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
            var retval = Create(info);
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
            var retval = Create(info);
            return retval;
        }

        private static AppDomain Create(AppDomainSetup info)
        {
            var retval = AppDomain.CreateDomain($"appDomain{Guid.NewGuid()}", AppDomain.CurrentDomain.Evidence, info);
            retval.UnhandledException += (sender, args) => MessageBox.Show(args.ExceptionObject.ToString());
            return retval;
        }
    }
}