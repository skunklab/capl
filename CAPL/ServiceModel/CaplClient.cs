using Capl.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capl.ServiceModel
{
    public sealed class CaplClient
    {
        public CaplClient()
        {
        }

        public CaplClient(ICaplCache cache, ICaplStore store)
        {
            Cache = cache;
            Store = store;
        }        

        public ICaplCache Cache { get; set; }
        public ICaplStore Store { get; set; }

        public AuthorizationPolicy GetPolicy(string key, TimeSpan ttl)
        {
            AuthorizationPolicy policy = Cache.Get(key);

            if (policy == null)
            {
                policy = Store.GetPolicy(key);
                if (policy != null)
                {
                    Cache.Set(key, policy, ttl);
                }
                else
                {
                    throw new InvalidOperationException("Key not found.");
                }
            }

            return policy;
        }
        public AuthorizationPolicy GetPolicy(string key)
        {
            AuthorizationPolicy policy = Cache.Get(key);

            if(policy == null)
            {
                policy = Store.GetPolicy(key);
                if(policy != null)
                {
                    Cache.Set(key, policy);
                }
                else
                {
                    throw new InvalidOperationException("Key not found.");
                }
            }

            return policy;
        }
           
    }
}
