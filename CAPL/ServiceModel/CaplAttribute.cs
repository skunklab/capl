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

namespace Capl.ServiceModel
{
    using Capl.Authorization;
    using Configuration;
    using System;
    using System.Security.Claims;
    using System.Threading;

    [AttributeUsage(AttributeTargets.All)]
    public class CaplAttribute : Attribute
    {
        public CaplAttribute(string policyId)
        {
            this.policyId = policyId;

            client = new CaplClient(CaplConfigurationManager.Cache, CaplConfigurationManager.Store);            
        }

        public CaplAttribute(string policyId, TimeSpan ttl)
        {
            client = new CaplClient(CaplConfigurationManager.Cache, CaplConfigurationManager.Store);
            this.policyId = policyId;
            this.ttl = ttl;
        }

        private string policyId;
        private TimeSpan? ttl;
        private CaplClient client;

        public virtual bool IsAuthorized()
        {
            AuthorizationPolicy policy = ttl.HasValue ? client.GetPolicy(policyId, ttl.Value) : client.GetPolicy(policyId);
            
            if (policy == null)
            {
                throw new UnauthorizedException("CAPL policy not found.");
            }

            ClaimsPrincipal prin = Thread.CurrentPrincipal as ClaimsPrincipal;

            return policy.Evaluate(prin.Identity as ClaimsIdentity);
        }
    }
}
