using Capl.ServiceModel;
using Capl.Services.Configuration;
using System;
using System.Configuration;

namespace Capl.Configuration
{
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
            get { return (TimeSpan)base["ttl"]; }
            set { base["ttl"] = value; }
        }

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            base.SetProperty(name, value);
            return true;
        }
    }
}
