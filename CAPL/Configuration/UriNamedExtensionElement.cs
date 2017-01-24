/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/



namespace Capl.Configuration
{
    using System.Configuration;

    public class UriNamedExtensionElement : NamedExtensionElement
    {
        public UriNamedExtensionElement()
        {
        }

        [ConfigurationProperty("uri", IsRequired = true, IsKey = true)]
        public string Uri
        {
            get { return (string)base["uri"]; }
            set { base["uri"] = value; }
        }
    }
}
