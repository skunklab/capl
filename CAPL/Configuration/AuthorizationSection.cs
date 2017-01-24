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

    public class AuthorizationSection : ConfigurationSection
    {
        public AuthorizationSection()
        {
        }

        [ConfigurationProperty("policyCache", IsRequired =false)]
        public PolicyCacheElement Cache
        {
            get { return (PolicyCacheElement)base["policyCache"]; }
            set { base["policyCache"] = value; }
        }

        [ConfigurationProperty("policyStore", IsRequired =false)]
        public PolicyStoreElement Store
        {
            get { return (PolicyStoreElement)base["policyStore"]; }
            set { base["policyStore"] = value; }
        }

        [ConfigurationProperty("matchExpressions", IsRequired = false)]
        public MatchElementCollection MatchExtensions
        {
            get { return (MatchElementCollection)base["matchExpressions"]; }
            set { base["matchExpressions"] = value; }
        }

        [ConfigurationProperty("operations", IsRequired = false)]
        public OperationElementCollection OperationExtensions
        {
            get { return (OperationElementCollection)base["operations"]; }
            set { base["operations"] = value; }
        }

        [ConfigurationProperty("transforms", IsRequired = false)]
        public TransformElementCollection TransformExtensions
        {
            get { return (TransformElementCollection)base["transforms"]; }
            set { base["transforms"] = value; }
        }

           
    }
}
