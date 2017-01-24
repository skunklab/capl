/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/


namespace Capl.Configuration
{
    using Capl.Authorization.Matching;
    using Capl.Authorization.Operations;
    using Capl.Authorization.Transforms;
    using Capl.ServiceModel;
    using System.Configuration;
    using System.Diagnostics;

    public class CaplConfigurationManager
    {
        static CaplConfigurationManager()
        {
            
            #region Authorization Section
            AuthorizationSection authzSection = (AuthorizationSection)ConfigurationManager.GetSection(ConfigurationConstants.AuthorizationSection);

            if (authzSection != null)
            {
                if (authzSection.MatchExtensions != null)
                {
                    matchExpressions = authzSection.MatchExtensions.MatchExpressions;
                }
                else
                {
                    Trace.TraceInformation("Match expressions are not configured for extensions.  Default values with be used.");
                    matchExpressions = new MatchElementCollection().MatchExpressions;
                }
                
                if (authzSection.OperationExtensions != null)
                {
                    operations = authzSection.OperationExtensions.Operations;
                }
                else
                {
                    Trace.TraceInformation("Authorization operations are not configured for extensions.  Default values with be used.");
                    operations = new OperationElementCollection().Operations;
                }

                if (authzSection.TransformExtensions != null)
                {                    
                    transforms = authzSection.TransformExtensions.Transforms;
                }
                else
                {
                    Trace.TraceInformation("Authorization transforms are not configured for extensions.  Default values with be used.");
                    transforms = new TransformElementCollection().Transforms;
                }

                if(authzSection.Cache != null)
                {
                    Cache = authzSection.Cache.GetCache();
                }

                if(authzSection.Store != null)
                {
                    Store = authzSection.Store.GetStore();
                }

                
            }
            else
            {
                Trace.TraceInformation("Authorization configuration section is not configured.");
                matchExpressions = new MatchElementCollection().MatchExpressions;
                operations = new OperationElementCollection().Operations;
                transforms = new TransformElementCollection().Transforms;
            }

            #endregion

            #region Extensions Section
            //AuthorizationSection authzSection = (AuthorizationSection)ConfigurationManager.GetSection(ConfigurationConstants.);

            #endregion
        }

        private static OperationsDictionary operations;
        private static TransformsDictionary transforms;
        private static MatchExpressionDictionary matchExpressions;
        private static ICaplCache cache;
        private static ICaplStore store;

        public static OperationsDictionary Operations
        {
            get { return operations; }
            internal set { operations = value; }
        }

        public static TransformsDictionary Transforms
        {
            get { return transforms; }
            internal set { transforms = value; }
        }

        public static MatchExpressionDictionary MatchExpressions
        {
            get { return matchExpressions; }
            internal set { matchExpressions = value; }
        }

        public static ICaplCache Cache
        {
            get { return cache; }
            internal set { cache = value; }
        }
        
        public static ICaplStore Store
        {
            get { return store; }
            internal set { store = value; }
        }
    }
}
