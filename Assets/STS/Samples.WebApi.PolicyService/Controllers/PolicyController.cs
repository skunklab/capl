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
        private ICaplStore blobStore;
        private string blobContainerName = "AZURE_BLOB_CONTAINER";
        private string blobConnectionString = "AZURE_BLOB_CONNECTION_STRING";


        public PolicyController()
        {
            blobStore = BlobStore.Create(blobContainerName, blobConnectionString);            
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
                AuthorizationPolicy policy = blobStore.GetPolicy(policyId.ToLower(CultureInfo.InvariantCulture));

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
        /// Add a CAPL policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns>c</returns>
        [HttpPost]
        public HttpResponseMessage Post(AuthorizationPolicy policy)
        {
            try
            {
                blobStore.SetPolicy(policy);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update a CAPL policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage Put(AuthorizationPolicy policy)
        {
            try
            {
                AuthorizationPolicy storedPolicy = blobStore.GetPolicy(policy.PolicyId.ToString().ToLower(CultureInfo.InvariantCulture));
                if(storedPolicy == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else
                {
                    blobStore.SetPolicy(policy);
                }
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
                AuthorizationPolicy storedPolicy = blobStore.GetPolicy(policy.PolicyId.ToString().ToLower(CultureInfo.InvariantCulture));
                if (storedPolicy == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else
                {
                    
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
