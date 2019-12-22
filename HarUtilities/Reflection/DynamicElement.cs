using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarUtilities.Reflection
{
    public class DynamicElement : ConfigurationElement
    {
        [ConfigurationProperty("value1", IsKey = true)]
        public string Value1 { get { return (string)this["value1"]; } set { this["value1"] = value; } }

        [ConfigurationProperty("value2")]
        public string Value2 { get { return (string)this["value2"]; } set { this["value2"] = value; } }

        [ConfigurationProperty("value3")]
        public string Value3 { get { return (string)this["value3"]; } set { this["value3"] = value; } }
    }
}
