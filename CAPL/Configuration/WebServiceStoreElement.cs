using Capl.ServiceModel;
using Capl.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Capl.Configuration
{
    public class WebServiceStoreElement : ExtensionElement<ICaplStore>
    {
        public override ICaplStore Create()
        {
            return new WebServiceStore(this.ServiceUrl);
        }

        public override string Name
        {
            get { return "webServiceStore"; }
        }

        [ConfigurationProperty("serviceUrl")]
        public string ServiceUrl
        {
            get { return (string)base["serviceUrl"]; }
            set { base["serviceUrl"] = value; }
        }


        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            base.SetProperty(name, value);
            return true;
        }
    }
}
