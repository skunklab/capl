using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capl.ServiceModel
{
    public class PolicyEntity : TableEntity
    {
        public PolicyEntity()
        {
            if(this.PartitionKey != null)
            {
                this.Id = this.PartitionKey;
            }
            else
            {
                this.Id = Guid.NewGuid().ToString();
            }
        }

        public PolicyEntity(string policyId, string xmlString)
        {
            this.PolicyId = policyId;
            this.PolicyXml = xmlString;
        }

        public String Id
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string PolicyId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }
        public string PolicyXml { get; set; }
    }
}
