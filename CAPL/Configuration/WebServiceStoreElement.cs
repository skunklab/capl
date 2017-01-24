/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/



namespace Capl.Configuration
{
    using Capl.ServiceModel;
    using Capl.Services.Configuration;
    using System.Configuration;

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
