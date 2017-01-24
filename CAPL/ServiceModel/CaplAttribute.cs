/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
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
