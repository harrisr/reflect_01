using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflect_01
{
    public class Employee : ConfigurationElement
    {
        [ConfigurationProperty("id", IsKey = true)]
        public string Id { get { return (string)this["id"]; } }

        [ConfigurationProperty("personalInfo")]
        public PersonalInfo PersonalInfo
        {
            get { return (PersonalInfo)this["personalInfo"]; }
        }

        [ConfigurationProperty("homeAddress")]
        public Address HomeAddress { get { return (Address)this["homeAddress"]; } }

        [ConfigurationProperty("officeAddress")]
        public Address OfficeAddress { get { return (Address)this["officeAddress"]; } }
    }

    public class PersonalInfo : ConfigurationElement
    {
        [ConfigurationProperty("ssn", IsKey = true)]
        public string SSN { get { return (string)this["ssn"]; } set { this["ssn"] = value; } }

        [ConfigurationProperty("height")]
        public int Height { get { return (int)this["height"]; } set { this["height"] = value; } }

        [ConfigurationProperty("weight")]
        public int Weight { get { return (int)this["weight"]; } set { this["weight"] = value; } }
    }

    public class Address : ConfigurationElement
    {
        [ConfigurationProperty("pin")]
        public string PinCode { get { return (string)this["pin"]; } }

        [ConfigurationProperty("city")]
        public string City { get { return (string)this["city"]; } }

        [ConfigurationProperty("state")]
        public string State { get { return (string)this["state"]; } }
    }
}
