using System.IO;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace RuleSetDesignerLauncher
{
    public class RuleSetGateway
    {
        private static readonly WorkflowMarkupSerializer WorkflowMarkupSerializer = new WorkflowMarkupSerializer();

        public static RuleSet GetRuleSet(string name)
        {
            var fileText = GetFileText(name);
            if (string.IsNullOrWhiteSpace(fileText))
            {
                return null;
            }

            var retval = Deserialize(fileText);
            return retval;
        }

        public static void SaveRuleSet(RuleSet ruleSet, string name)
        {
            var contents = Serialize(ruleSet);
            File.WriteAllText(name, contents);
        }

        private static RuleSet Deserialize(string value)
        {
            using (var stringReader = new StringReader(value))
            using (var xmlTextReader = new XmlTextReader(stringReader))
            {
                var retval = (RuleSet)WorkflowMarkupSerializer.Deserialize(xmlTextReader);
                return retval;
            }
        }

        private static string GetFileText(string name)
        {
            if (File.Exists(name))
            {
                var retval = File.ReadAllText(name);
                return retval;
            }

            return null;
        }

        private static string Serialize(RuleSet ruleSet)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = new XmlTextWriter(stringWriter))
            {
                WorkflowMarkupSerializer.Serialize(xmlTextWriter, ruleSet);
                var retval = stringWriter.ToString();
                return retval;
            }
        }
    }
}