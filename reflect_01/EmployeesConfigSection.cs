using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflect_01
{
    public class EmployeesConfigSection : ConfigurationSection
    {
        //If you replace "employeeCollection" with "" then you do not need "employeeCollection" 
        //element as a wrapper node over employee nodes in config file.
        [ConfigurationProperty("employeeCollection", IsDefaultCollection = true,
        IsKey = false, IsRequired = true)]
        public EmployeeCollection Members
        {
            get
            {
                return base["employeeCollection"] as EmployeeCollection;
            }

            set
            {
                base["employeeCollection"] = value;
            }
        }
    }
}
