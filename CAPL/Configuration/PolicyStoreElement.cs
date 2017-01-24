using Capl.ServiceModel;
using Capl.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Capl.Configuration
{
    public class PolicyStoreElement : ConfigurationElement //NamedExtensionElement
    {
        public PolicyStoreElement()
        {
        }

        private ICaplStore store;

        public ICaplStore GetStore()
        {
            return store;
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
        {
            ExtensionsSection section = (ExtensionsSection)ConfigurationManager.GetSection(ConfigurationConstants.ExtensionsSection);
            ExtensionTypeElement elem = section.StoreExtension;
            if (elem.Name == elementName)
            {
                ExtensionElement<ICaplStore> elemType = (ExtensionElement<ICaplStore>)Activator.CreateInstance(Type.GetType(elem.TypeName));
                store = elemType.Create();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
