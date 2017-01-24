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
    public class PolicyCacheElement : ConfigurationElement //NamedExtensionElement
    {
        public PolicyCacheElement()
        {
            
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
        {
            ExtensionsSection section = (ExtensionsSection)ConfigurationManager.GetSection(ConfigurationConstants.ExtensionsSection);
            ExtensionTypeElement elem = section.CachingExtension;
            if (elem.Name == elementName)
            {
                ExtensionElement<ICaplCache> elemType = (ExtensionElement<ICaplCache>)Activator.CreateInstance(Type.GetType(elem.TypeName));
                cache = elemType.Create();
                return true;
            }
            else
            {
                return false;
            }
        }

        private ICaplCache cache;
        public ICaplCache GetCache()
        {
            return cache;
        }



        

        



        

        
    }
}
