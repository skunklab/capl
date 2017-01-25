/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/

using Capl.Authorization;

namespace Capl.ServiceModel
{
    public interface ICaplStore
    {
        AuthorizationPolicy GetPolicy(string policyId);

        void SetPolicy(AuthorizationPolicy policy);

        bool RemovePolicy(string policyId);


    }
}
