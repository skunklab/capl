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

namespace Capl.Authorization.Transforms
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Capl.Configuration;
    
    /// <summary>
    /// An abstract action used to perform a type of transform.
    /// </summary>
    public abstract class TransformAction
    {
        /// <summary>
        /// Gets a unique URI that corresponds to the transform action.
        /// </summary>
        public abstract Uri Uri { get; }

        /// <summary>
        /// Creates a transform action.
        /// </summary>
        /// <param name="action">The identifier of the transform action to create.</param>
        /// <param name="transforms">Dictionary of transforms.</param>
        /// <returns>An action used to transform claims.</returns>
        public static TransformAction Create(Uri action, TransformsDictionary transforms)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            TransformAction transformAction = null;

            if (transforms == null)
            {                
                transformAction = CaplConfigurationManager.Transforms[action.ToString()];
            }
            else
            {
                transformAction = transforms[action.ToString()];
            }

            return transformAction;
        }

        /// <summary>
        /// Executes a transform.
        /// </summary>
        /// <param name="claimSet">A set of claims to transform.</param>
        /// <param name="sourceClaim">The source claim used in matching.</param>
        /// <param name="targetClaim">The resultant claim used in the transform.</param>
        /// <returns>Transformed set of claims.</returns>
        public abstract IEnumerable<Claim> Execute(IEnumerable<Claim> claims, IList<Claim> matchedClaims, LiteralClaim targetClaim);
    }
}
