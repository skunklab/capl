using Capl.Authorization;
using Capl.ServiceModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Samples.WebApi.PolicyService.Controllers
{
    public class PolicyController : ApiController
    {
        private ICaplCache webCache;
        //private ICaplStore blobStore;
        //private string blobContainerName = "AZURE_BLOB_CONTAINER_NAME"; //e.g., policies
        //private string blobConnectionString = "AZURE_BLOB_CONNECTION_STRING";  //e.g., 
        
        public PolicyController()
        {
            webCache = new WebCache() { DefaultTTL = TimeSpan.FromMinutes(10) };
            //blobStore = BlobStore.Create(blobContainerName, blobConnectionString);            
        }

        /// <summary>
        /// Get a CAPL policy
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns>CAPL poicy</returns>
        [HttpGet]
        public HttpResponseMessage Get(string policyId)
        {
            try
            {
                string policyUriString = new Uri(policyId).ToString().ToLower(CultureInfo.InvariantCulture);
                AuthorizationPolicy policy = webCache.Get(policyUriString);
                //AuthorizationPolicy policy = blobStore.GetPolicy(policyId);

                if(policy != null)
                {
                    return Request.CreateResponse<AuthorizationPolicy>(HttpStatusCode.OK, policy);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Add a CAPL policy; will overwrite (update) and existing CAPL policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns>c</returns>
        [HttpPost]
        public HttpResponseMessage Post(AuthorizationPolicy policy)
        {
            try
            {
                string policyUriString = policy.PolicyId.ToString().ToLower(CultureInfo.InvariantCulture);
                webCache.Set(policyUriString, policy);
                //blobStore.SetPolicy(policy);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete a CAPL policy
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete(string policyId)
        {
            try
            {
                string policyUriString = new Uri(policyId).ToString().ToLower(CultureInfo.InvariantCulture);
                bool result = webCache.Remove(policyUriString);

                //bool result = blobStore.RemovePolicy(policyId);

                if (result)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
