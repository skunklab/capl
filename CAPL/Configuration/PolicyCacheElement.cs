/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/




namespace Capl.Configuration
{
    using Capl.ServiceModel;
    using Capl.Services.Configuration;
    using System;
    using System.Configuration;
    using System.Xml;

    public class PolicyCacheElement : ConfigurationElement 
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

                for (int i = 0; i < reader.AttributeCount; i++)
                {
                    reader.MoveToAttribute(i);
                    string name = reader.LocalName;
                    string value = reader.Value;
                    elemType.SetProperty(name, value);

                }

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
