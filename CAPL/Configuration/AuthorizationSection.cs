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
using Capl.Services.Configuration;

namespace Capl.Configuration
{
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
