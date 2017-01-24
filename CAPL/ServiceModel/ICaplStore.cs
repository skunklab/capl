using Capl.Authorization;

namespace Capl.ServiceModel
{
    public interface ICaplStore
    {
        AuthorizationPolicy GetPolicy(string policyId);

        void SetPolicy(AuthorizationPolicy policy);


    }
}
