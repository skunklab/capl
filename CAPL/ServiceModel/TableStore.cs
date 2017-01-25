/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/
namespace Capl.ServiceModel
{
    using Capl.Authorization;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    public class TableStore : ICaplStore
    {
        public static TableStore Create(string tableName, string connectionString)
        {
            if(instance == null)
            {
                instance = new TableStore(tableName, connectionString);   
            }

            return instance;
        }

        private static TableStore instance;

        protected TableStore(string tableName, string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            client = new CloudTableClient(storageAccount.TableStorageUri, storageAccount.Credentials);
            table = client.GetTableReference(tableName);

            Task task = table.CreateIfNotExistsAsync();
            Task.WhenAll(task);
        }

        public string TableName { get; set; }
        public string ConnectionString { get; set; }

        private CloudTableClient client;
        private CloudTable table;

        public AuthorizationPolicy GetPolicy(string policyId)
        {
            Uri policyUri = new Uri(policyId);
            string policyUriString = policyUri.ToString().ToLower(CultureInfo.InvariantCulture);

            AuthorizationPolicy policy = null;
            TableQuery<PolicyEntity> query = new TableQuery<PolicyEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, policyUriString));
            TableQuerySegment<PolicyEntity> segment = table.ExecuteQuerySegmented<PolicyEntity>(query, new TableContinuationToken());

            if (segment == null || segment.Results.Count == 0)
            {
                return null;
            }
            else
            {
                PolicyEntity entity = segment.First();
                using(MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(entity.PolicyXml)))
                {
                    stream.Position = 0;
                    using(XmlReader reader = XmlReader.Create(stream))
                    {
                        policy = AuthorizationPolicy.Load(reader);
                        reader.Close();
                    }

                    stream.Close();
                }

                return policy;
            }           
        }

        public void SetPolicy(AuthorizationPolicy policy)
        {           

            XmlWriterSettings settings = new XmlWriterSettings() { OmitXmlDeclaration = true};
            StringBuilder builder = new StringBuilder();
            using(XmlWriter writer = XmlWriter.Create(builder, settings))
            {
                policy.WriteXml(writer);
                writer.Flush();
                writer.Close();
            }

            PolicyEntity entity = new PolicyEntity() { PolicyId = policy.PolicyId.ToString().ToLower(CultureInfo.InvariantCulture), PolicyXml = builder.ToString() };
            TableOperation operation = TableOperation.InsertOrReplace(entity);
            
            Task task = Task.Factory.StartNew(async () =>
                {
                    await table.ExecuteAsync(operation);
                });

            Task.WhenAll(task);
        }

        public bool RemovePolicy(string policyId)
        {
            Uri policyUri = new Uri(policyId);
            string policyUriString = policyUri.ToString().ToLower(CultureInfo.InvariantCulture);

            TableQuery<PolicyEntity> query = new TableQuery<PolicyEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, policyId));
            TableQuerySegment<PolicyEntity> segment = table.ExecuteQuerySegmented<PolicyEntity>(query, new TableContinuationToken());

            if (segment == null || segment.Results.Count == 0)
            {
                return false;
            }
            else
            {                
                PolicyEntity entity = segment.First();
                TableResult result = table.Execute(TableOperation.Delete(entity));
                return (result.HttpStatusCode == 200 || result.HttpStatusCode == 202 || result.HttpStatusCode == 204);
            }
        }

    }
}
