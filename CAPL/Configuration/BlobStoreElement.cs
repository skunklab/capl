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
    public class BlobStoreElement : ExtensionElement<ICaplStore>
    {
        public override ICaplStore Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            return BlobStore.Create(ContainerName, connectionString);
        }

        public override string Name
        {
            get { return "blobStore"; }
        }

        [ConfigurationProperty("connectionStringName")]
        public string ConnectionStringName
        {
            get { return (string)base["connectionStringName"]; }
            set { base["connectionStringName"] = value; }
        }

        [ConfigurationProperty("containerName")]
        public string ContainerName
        {
            get { return (string)base["containerName"]; }
            set { base["containerName"] = value; }
        }


        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            base.SetProperty(name, value);
            return true;
        }

    }
}
