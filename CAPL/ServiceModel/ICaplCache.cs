/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/
namespace Capl.ServiceModel
{
    using Capl.Authorization;
    using System;

    public interface ICaplCache
    {
        TimeSpan DefaultTTL { get; set; }
        AuthorizationPolicy Get(string key);
        void Set(string key, AuthorizationPolicy policy, TimeSpan ttl);

        void Set(string key, AuthorizationPolicy policy);
        void Remove(string key);

    }
}
