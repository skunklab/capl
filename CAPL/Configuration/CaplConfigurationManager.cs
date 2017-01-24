/*
Claims Authorization Policy Langugage SDK ver. 1.0
 
Copyright (c) Matt Long labskunk@gmail.com
 
All rights reserved.
 
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the ""Software""), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System.Configuration;
using Capl.Authorization.Operations;
using Capl.Authorization.Transforms;
using Capl.Authorization.Matching;
using System.Diagnostics;
using Capl.ServiceModel;
using System;

namespace Capl.Configuration
{
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
