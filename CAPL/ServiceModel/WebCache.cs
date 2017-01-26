/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/

using Capl.Authorization;
using System;
using System.Globalization;
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
            if(HttpRuntime.Cache.Get(key) != null)
            {
                HttpRuntime.Cache.Remove(key);
            }

            HttpRuntime.Cache.Add(key, policy, null, DateTime.Now.Add(DefaultTTL), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public void Set(string key, AuthorizationPolicy policy, TimeSpan ttl)
        {
            HttpRuntime.Cache.Add(key, policy, null, DateTime.Now.Add(ttl), Cache.NoSlidingExpiration, CacheItemPriority.High, null);            
        }

        public bool Remove(string key)
        {
            AuthorizationPolicy policy = Get(key);

            if(policy != null)
            {
                HttpRuntime.Cache.Remove(key);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
