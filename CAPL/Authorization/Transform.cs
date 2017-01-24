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
    using System.Collections.Generic;
    using Capl.Authorization.Matching;

    /// <summary>
    /// Interface used to transform claims.
    /// </summary>
    [Serializable]
    public abstract class Transform 
    {
        /// <summary>
        /// An optional URI that can identify the transform.
        /// </summary>
        public abstract Uri TransformID { get; set; }

        /// <summary>
        /// A required type the identifies the transform.
        /// </summary>
        public abstract Uri Type { get; set; }

        /// <summary>
        /// An optional evaluation expression that determines whether the transform should be processed.
        /// </summary>
        /// <remarks>If the evaluation expression is omitted, then transform will be processed; otherwise the transform will only be processed if the 
        /// evaluation expression evaluates TRUE.</remarks>
        public abstract Term Expression { get; set; }

        /// <summary>
        /// A required match expression that matches the input set of claims to determine which claims should be processed.
        /// </summary>
        public abstract Match MatchExpression { get; set; }

        /// <summary>
        /// An optional target claim that applies to any transform that adds or replaces claims for the transform.
        /// </summary>
        /// ><remarks>The target claims is used for input into the transform, which is used by the normative Add and Replace transforms
        /// to Add the target claim or replace an existing claim with the target claim.</remarks>
        public abstract LiteralClaim TargetClaim { get; set; }

        /// <summary>
        /// Transforms a set of claims.
        /// </summary>
        /// <param name="claimSet">Set of claims to transform.</param>
        /// <returns>Transformed set of claims.</returns>
        public abstract IEnumerable<Claim> TransformClaims(IEnumerable<Claim> claims);

        public abstract void ReadXml(XmlReader reader);

        public abstract void WriteXml(XmlWriter writer);

        
    }
}
