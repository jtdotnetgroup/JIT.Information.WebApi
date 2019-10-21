using System;

namespace CommonTools
{
    public class SelectAttribute:Attribute
    {
        public SelectAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}