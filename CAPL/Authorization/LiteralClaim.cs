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

    /// <summary>
    /// A definition of a claim.
    /// </summary>
    [Serializable]
    public class LiteralClaim 
    {        
        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralClaim"/> class.
        /// </summary>
        public LiteralClaim()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralClaim"/> class.
        /// </summary>
        /// <param name="claimType">The namespace of the claim.</param>
        /// <param name="claimValue">The value of the claim.</param>
        public LiteralClaim(string claimType, string claimValue)
        {
            this.ClaimType = claimType;
            this.ClaimValue = claimValue;
        }

        /// <summary>
        /// Gets or sets the claim type.
        /// </summary>
        public string ClaimType { get; set; }
        

        /// <summary>
        /// Gets or sets the claim value.
        /// </summary>
        public string ClaimValue { get; set; }
            
    }
}
