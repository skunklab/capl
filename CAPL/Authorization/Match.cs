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

namespace Capl.Authorization
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    public class Match : IXmlSerializable
    {
        public Match()
        {

        }

        public Match(Uri matchExpressionUri, string claimType)
            : this(matchExpressionUri, claimType, true, null)
        {

        }

        public Match(Uri matchExpressionUri, string claimType, bool required)
            : this(matchExpressionUri, claimType, required, null)
        {

        }

        public Match(Uri matchExpressionUri, string claimType, bool required, string value)
        {
            Type = matchExpressionUri;
            ClaimType = claimType;
            Required = required;
            Value = value;
        }
        /// <summary>
        /// Gets or set the type of match expression
        /// </summary>
        public Uri Type { get; set; }

        /// <summary>
        /// Gets or sets a value for a claim type.
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets a value for claim.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether matching is required for evaluation.
        /// </summary>
        public bool Required { get; set; }


        public static Match Load(XmlReader reader)
        {
            Match match = new Match();
            match.ReadXml(reader);
            return match;           
        }
                
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            reader.MoveToRequiredStartElement(AuthorizationConstants.Elements.Match);
            this.ClaimType = reader.GetOptionalAttribute(AuthorizationConstants.Attributes.ClaimType);
            this.Type = new Uri(reader.GetRequiredAttribute(AuthorizationConstants.Attributes.Type));
            string required = reader.GetOptionalAttribute(AuthorizationConstants.Attributes.Required);

            if (string.IsNullOrEmpty(required))
            {
                this.Required = true;
            }
            else
            {
                this.Required = XmlConvert.ToBoolean(required);
            }

            this.Value = reader.GetElementValue(AuthorizationConstants.Elements.Match);

            if (!reader.IsRequiredEndElement(AuthorizationConstants.Elements.Match))
            {
                throw new SerializationException(String.Format("Unexpected element {0}", reader.LocalName));
            }
            
        }

        public void WriteXml(XmlWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WriteStartElement(AuthorizationConstants.Elements.Match, AuthorizationConstants.Namespaces.Xmlns);

            writer.WriteAttributeString(AuthorizationConstants.Attributes.Type, this.Type.ToString());
            writer.WriteAttributeString(AuthorizationConstants.Attributes.ClaimType, this.ClaimType);
            writer.WriteAttributeString(AuthorizationConstants.Attributes.Required, XmlConvert.ToString(this.Required));

            if (!string.IsNullOrEmpty(this.Value))
            {
                writer.WriteString(this.Value);
            }

            writer.WriteEndElement();          
        }
    }
}
