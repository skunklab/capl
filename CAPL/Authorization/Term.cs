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
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using System.Security.Claims;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    /// <summary>
    /// An interface used to evaluate a set of claims or a collection of claims and set the truthful evaluation
    ///  for both.
    /// </summary>
    /// <remarks>The abstract LogicalConnectiveCollection implements this interface.</remarks>
    [Serializable]
    public abstract class Term : IXmlSerializable
    {
        /// <summary>
        /// Get or sets the truthful evaluation.
        /// </summary>
        public abstract bool Evaluates { get; set; }
        public abstract Uri TermId { get; set; }
        public abstract bool Evaluate(IEnumerable<Claim> claims);

        public static Term Load(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            Term evalExp = null;

            reader.MoveToStartElement();

            if(reader.IsRequiredStartElement(AuthorizationConstants.Elements.Rule))
            {
                Rule rule = new Rule();
                rule.ReadXml(reader);
                evalExp = rule;
            }

            if(reader.IsRequiredStartElement(AuthorizationConstants.Elements.LogicalAnd))
            {
                LogicalAndCollection logicalAnd = new LogicalAndCollection();
                logicalAnd.ReadXml(reader);
                evalExp = logicalAnd;
            }


            if(reader.IsRequiredStartElement(AuthorizationConstants.Elements.LogicalOr))
            {
                LogicalOrCollection logicalOr = new LogicalOrCollection();
                logicalOr.ReadXml(reader);
                evalExp = logicalOr;
            }

            if (evalExp != null)
            {
                return evalExp;
            }
            else
            {
                throw new SerializationException("Invalid evaluation expression element.");
            }
        }

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        public abstract void ReadXml(XmlReader reader);

        public abstract void WriteXml(XmlWriter writer);
    }
}
