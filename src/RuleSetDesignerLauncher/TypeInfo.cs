using System;

namespace RuleSetDesignerLauncher
{
    [Serializable]
    public class TypeInfo
    {
        public string FullName { get; set; }
        public string FilePath { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
