/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/

using Microsoft.WindowsAzure.Storage.Table;
using System;

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
