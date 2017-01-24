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
using System.Web;
using System.Web.Caching;

namespace Capl.ServiceModel
{
    public class WebCache : ICaplCache
    {
        static WebCache()
        {            
            runtime = new HttpRuntime();
        }

        
        public TimeSpan DefaultTTL { get; set; }

        private static HttpRuntime runtime;

        public AuthorizationPolicy Get(string key)
        {
            return HttpRuntime.Cache.Get(key) as AuthorizationPolicy;
        }

        public void Set(string key, AuthorizationPolicy policy)
        {
            HttpRuntime.Cache.Add(key, policy, null, DateTime.Now.Add(DefaultTTL), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public void Set(string key, AuthorizationPolicy policy, TimeSpan ttl)
        {
            HttpRuntime.Cache.Add(key, policy, null, DateTime.Now.Add(ttl), Cache.NoSlidingExpiration, CacheItemPriority.High, null);            
        }

        public void Remove(string key)
        {
            AuthorizationPolicy policy = Get(key);

            if(policy != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }
    }
}
