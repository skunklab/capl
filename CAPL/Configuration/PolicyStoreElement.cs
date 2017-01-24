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

    public class PolicyStoreElement : ConfigurationElement 
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

                for (int i = 0; i < reader.AttributeCount; i++)
                {
                    reader.MoveToAttribute(i);
                    string name = reader.LocalName;
                    string value = reader.Value;
                    elemType.SetProperty(name, value);

                }

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
