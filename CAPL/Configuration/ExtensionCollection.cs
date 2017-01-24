/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/



namespace Capl.Services.Configuration
{
    using System.Configuration;

    [ConfigurationCollection(typeof(ExtensionTypeElement))]
    public class ExtensionCollection : ConfigurationElementCollection
    {
        public ExtensionCollection()
        {
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExtensionTypeElement)element).Name;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExtensionTypeElement();
        }

        public void Add(ExtensionTypeElement element)
        {
            BaseAdd(element);
        }
    }
}
