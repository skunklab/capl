using Capl.Authorization;
using Capl.Configuration;
using Capl.ServiceModel;
using System;
using System.Security;
using System.Security.Claims;
using System.Threading;

namespace Samples.CodeAccessPolicy
{
    public class CaplCodeAccessPermission : IPermission
    {
        public CaplCodeAccessPermission(CaplCodeAccessAttribute attribute)
        {
            policyId = attribute.PolicyId;
            client = new CaplClient(CaplConfigurationManager.Cache, CaplConfigurationManager.Store);
        }

        private string policyId;
        private CaplClient client;

        public IPermission Copy()
        {
            return this;
        }

        public void Demand()
        {
            ClaimsPrincipal principal = Thread.CurrentPrincipal as ClaimsPrincipal;
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            AuthorizationPolicy policy = client.GetPolicy(policyId);
            if(!policy.Evaluate(identity))
            {
                throw new UnauthorizedAccessException("Not authorized.");
            }
        }

        public void FromXml(SecurityElement e)
        {
            
        }

        public IPermission Intersect(IPermission target)
        {
            return null;
        }

        public bool IsSubsetOf(IPermission target)
        {
            return false;
        }

        public SecurityElement ToXml()
        {
            return null;
        }

        public IPermission Union(IPermission target)
        {
            return null;
        }
    }
}
