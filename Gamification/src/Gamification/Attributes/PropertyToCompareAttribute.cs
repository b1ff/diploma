using System;

namespace Gamification.Core.Attributes
{
    public class PropertyToCompareAttribute : Attribute
    {
        public PropertyToCompareAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
