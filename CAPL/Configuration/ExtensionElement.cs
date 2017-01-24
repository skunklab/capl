/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/



namespace Capl.Services.Configuration
{
    using System.Configuration;

    public abstract class ExtensionElement<TExtension> : ConfigurationElement
    {
        public abstract TExtension Create();
        public abstract string Name { get; }
        public virtual void SetProperty(string name, string value)
        {
            base.SetPropertyValue(new ConfigurationProperty(name, typeof(string)), value, true);
        }
    }
}
