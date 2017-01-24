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

using Capl.Authorization;
using System;

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
