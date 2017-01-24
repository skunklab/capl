/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/


namespace Capl.Configuration
{
    using System.Configuration;
    public class NamedExtensionElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string ExtensionName
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }        
    }
}
