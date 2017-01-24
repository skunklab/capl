/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/


namespace Capl.Configuration
{
    using System.Configuration;
    using Capl.Services.Configuration;

    public class ExtensionsSection : ConfigurationSection
    {
        public ExtensionsSection()
        {
        }

        [ConfigurationProperty("matchExpressions", IsRequired = false)]
        public ExtensionCollection MatchExtensions
        {
            get { return (ExtensionCollection)base["matchExpressions"]; }
            set { base["matchExpressions"] = value; }
        }

        [ConfigurationProperty("operations", IsRequired = false)]
        public ExtensionCollection OperationExtensions
        {
            get { return (ExtensionCollection)base["operations"]; }
            set { base["operations"] = value; }
        }

        [ConfigurationProperty("transforms", IsRequired = false)]
        public ExtensionCollection TransformExtensions
        {
            get { return (ExtensionCollection)base["transforms"]; }
            set { base["transforms"] = value; }
        }

        [ConfigurationProperty("caching", IsRequired = false)]
        public ExtensionTypeElement CachingExtension
        {
            get { return (ExtensionTypeElement)base["caching"]; }
            set { base["caching"] = value; }
        }

        [ConfigurationProperty("store", IsRequired = false)]
        public ExtensionTypeElement StoreExtension
        {
            get { return (ExtensionTypeElement)base["store"]; }
            set { base["store"] = value; }
        }
    }
}
