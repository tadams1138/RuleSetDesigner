using System;
using System.Windows.Forms;
using RuleSetDesigner;

namespace RuleSetDesignerLauncher
{
    internal class RuleSetDialogFactory
    {
        public static Form Get(AppDomain loaderDomain, TypeInfo typeInfo, string fileName)
        {
            var factory = CreateInternalFactory(loaderDomain, typeInfo, fileName);
            loaderDomain.DoCallBack(factory.Get);
            return factory.Dialog;
        }

        private static InternalFactory CreateInternalFactory(AppDomain appDomain, TypeInfo typeInfo, string fileName)
        {
            var type = typeof(InternalFactory);
            // ReSharper disable once AssignNullToNotNullAttribute
            var retval = (InternalFactory)appDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            retval.TypeInfo = typeInfo;
            retval.FileName = fileName;
            return retval;
        }

        private class InternalFactory : MarshalByRefObject
        {
            public TypeInfo TypeInfo;
            public string FileName;
            public RuleSetDialog Dialog;

            public void Get()
            {
                AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => Assembly.Load(args.Name);
                var ruleSet = RuleSetGateway.GetRuleSet(FileName);
                var activityType = GetActivityType(TypeInfo);
                Dialog = new RuleSetDialog(activityType, null, ruleSet);
                Dialog.Save += DialogOnSave;
            }

            private static System.Type GetActivityType(TypeInfo typeInfo)
            {
                var assembly = System.Reflection.Assembly.LoadFrom(typeInfo.FilePath);
                var retval = assembly.GetType(typeInfo.FullName);
                return retval;
            }

            private void DialogOnSave(object sender, EventArgs e)
            {
                try
                {
                    SaveRuleSet();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            private void SaveRuleSet()
            {
                RuleSetGateway.SaveRuleSet(Dialog.RuleSet, FileName);
            }
        }
    }
}
