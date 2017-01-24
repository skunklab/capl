/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/



namespace Capl.Services.Configuration
{
    using System.Configuration;

    public class ExtensionTypeElement : ConfigurationElement
    {
        public ExtensionTypeElement()
        {
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string TypeName
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
