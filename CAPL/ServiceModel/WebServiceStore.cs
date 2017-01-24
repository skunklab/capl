

namespace Capl.ServiceModel
{
    using Capl.Authorization;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Xml;
    public class WebServiceStore : ICaplStore
    {
        public WebServiceStore()
        {
        }

        public WebServiceStore(string serviceUrl)
        {
            this.ServiceUrl = serviceUrl;
        }

        public X509Certificate2 Certificate { get; set; }

        public string SecurityToken { get; set; }        

        public string ServiceUrl { get; set; }

        public AuthorizationPolicy GetPolicy(string policyId)
        {
            HttpWebRequest request = null;

            try
            {
                string url = null;
                Uri uri = new Uri(ServiceUrl);
                if (uri.Query == null)
                {
                    url = String.Format("{0}?policyid={1}", this.ServiceUrl.ToString(), policyId);
                }
                else
                {
                    url = String.Format("{0}&policyid={1}", this.ServiceUrl.ToString(), policyId);
                }

                request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.ContentType = "application/xml";
                request.Accept = "application/xml";
                request.Method = "GET";

                if (!string.IsNullOrEmpty(this.SecurityToken))
                {
                    request.Headers.Add("Authorization", String.Format("Bearer {0}", this.SecurityToken));
                }

                if (this.Certificate != null)
                {
                    request.ClientCertificates.Add(this.Certificate);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }

            try
            {
                AuthorizationPolicy policy = null;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Trace.TraceInformation("Rest request is success.");

                        using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            policy = AuthorizationPolicy.Load(reader);
                            reader.Close();
                            response.Close();
                        }
                    }
                    else
                    {
                        Trace.TraceInformation("Rest request return an expected status code.");
                    }
                }

                return policy;
            }
            catch (WebException we)
            {
                Trace.TraceError(we.Message);
                throw;
            }
        }

        public void SetPolicy(AuthorizationPolicy policy)
        {
            HttpWebRequest request = null;

            StringBuilder builder = new StringBuilder();
            XmlWriterSettings writerSettings = new XmlWriterSettings() { OmitXmlDeclaration = true };

            using (XmlWriter writer = XmlWriter.Create(builder, writerSettings))
            {
                policy.WriteXml(writer);
                writer.Flush();
                writer.Close();
            }

            byte[] message = Encoding.UTF8.GetBytes(builder.ToString());

            try
            {
                request = HttpWebRequest.Create(this.ServiceUrl) as HttpWebRequest;
                request.ContentType = "application/xml";
                request.Accept = "application/xml";
                request.Method = "POST";

                if (!string.IsNullOrEmpty(this.SecurityToken))
                {
                    request.Headers.Add("Authorization", String.Format("Bearer {0}", this.SecurityToken));
                }
                
                if(Certificate != null)
                {
                    request.ClientCertificates.Add(this.Certificate);
                }

                request.ContentLength = message.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(message, 0, message.Length);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.Accepted)
                    {
                        Trace.TraceInformation("Rest request is success.");
                    }
                    else
                    {
                        Trace.TraceInformation("Rest request return an expected status code.");
                    }
                }
            }
            catch (WebException we)
            {
                Trace.TraceError(we.Message);
                throw;
            }
        }
    }
}
