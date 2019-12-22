using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarUtilities.Reflection
{
    public class DynamicConfigSection : ConfigurationSection
    {
        // If you replace "DynamicConfigCollection" with "" then you do not need "DynamicConfigCollection" 
        // element as a wrapper node over DynamicElement nodes in config file.
        [ConfigurationProperty("DynamicConfigCollection", IsDefaultCollection = true, IsKey = false, IsRequired = true)]
        public DynamicConfigCollection Members
        {
            get { return (DynamicConfigCollection)base["DynamicConfigCollection"]; }

            set { base["DynamicConfigCollection"] = value; }
        }
    }
}
