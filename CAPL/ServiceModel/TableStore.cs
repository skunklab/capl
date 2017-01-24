
/*
Claims Authorization Policy Langugage SDK ver. 1.0
 
Copyright (c) Matt Long labskunk@gmail.com
 
All rights reserved.
 
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the ""Software""), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace Capl.ServiceModel
{
    using Capl.Authorization;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
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
            AuthorizationPolicy policy = null;
            TableQuery<PolicyEntity> query = new TableQuery<PolicyEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, policyId));
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

    }
}
