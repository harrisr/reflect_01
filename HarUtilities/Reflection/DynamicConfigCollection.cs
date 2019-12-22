using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarUtilities.Reflection
{
    public class DynamicConfigCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DynamicElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DynamicElement)element).Value1;
        }

        protected override string ElementName
        {
            get { return "DynamicElement"; }
        }

        protected override bool IsElementName(string elementName)
        {
            return !String.IsNullOrEmpty(elementName) && elementName.Equals("DynamicElement");
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        public DynamicElement this[int index]
        {
            get { return (DynamicElement)base.BaseGet(index); }
        }

        public new DynamicElement this[string key]
        {
            get { return (DynamicElement)base.BaseGet(key); }
        }
    }

}






namespace reflect_01
{

}

