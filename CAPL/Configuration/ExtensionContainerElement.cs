/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/



namespace Capl.Services.Configuration
{
    using System.Configuration;

    public abstract class ExtensionContainerElement<TExtension> : ConfigurationElement
    {
        public abstract TExtension GetExtension();
    }
}
