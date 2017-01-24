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

    public class WebCacheElement : ExtensionElement<ICaplCache>
    {
        public override ICaplCache Create()
        {
            WebCache cache = new WebCache();
            cache.DefaultTTL = TTL;
            return cache;
        }

        public override string Name
        {
            get { return "webCache"; }
        }

        [ConfigurationProperty("ttl", IsRequired = true)]
        [TimeSpanValidator()]
        public TimeSpan TTL
        {
            get { return TimeSpan.Parse((string)base["ttl"]); }
            set { base["ttl"] = value; }
        }

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            base.SetProperty(name, value);
            return true;
        }
    }
}
