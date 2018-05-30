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
            if (File.Exists(name))
            {
                var fileText = File.ReadAllText(name);
                var retval = Deserialize(fileText);
                return retval;
            }

            return null;
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