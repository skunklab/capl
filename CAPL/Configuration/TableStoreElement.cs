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

    public class TableStoreElement : ExtensionElement<ICaplStore>
    {
        public override ICaplStore Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            return TableStore.Create(ContainerName, connectionString);
        }

        public override string Name
        {
            get { return "tableStore"; }
        }

        [ConfigurationProperty("connectionStringName")]
        public string ConnectionStringName
        {
            get { return (string)base["connectionStringName"]; }
            set { base["connectionStringName"] = value; }
        }

        [ConfigurationProperty("tableName")]
        public string ContainerName
        {
            get { return (string)base["tableName"]; }
            set { base["tableName"] = value; }
        }

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            base.SetProperty(name, value);
            return true;
        }
    }
}
