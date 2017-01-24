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

    public class RedisCacheElement : ExtensionElement<ICaplCache>
    {
        public override ICaplCache Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            return new RedisCache(DatabaseNo, connectionString, TTL);
        }

        public override string Name
        {
            get { return "RedisCache"; }
        }

        [ConfigurationProperty("databaseNo", IsRequired =true)]
        public int DatabaseNo
        {
            get { return (int)base["databaseNo"]; }
            set { base["databaseNo"] = value; }
        }

        [ConfigurationProperty("connectionStringName", IsRequired =true)]
        public string ConnectionStringName
        {
            get { return (string)base["connectionStringName"]; }
            set { base["connectionStringName"] = value; }
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
