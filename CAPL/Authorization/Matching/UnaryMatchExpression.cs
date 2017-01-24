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
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Capl.Authorization.Matching
{
    /// <summary>
    /// Creates a canonical claim that binds to the the RHS of the expression.  The canonical claim is a signal that a unary operation
    /// will be required using only the claim value from the identity.
    /// </summary>
    public class UnaryMatchExpression : MatchExpression
    {
        public static Uri MatchUri
        {
            get { return new Uri(AuthorizationConstants.MatchUris.Any); }
        }

        public override Uri Uri
        {
            get { return new Uri(AuthorizationConstants.MatchUris.Any); }
        }

        public override IList<Claim> MatchClaims(IEnumerable<Claim> claims, string claimType, string value)
        {
            Claim claim = new Claim(AuthorizationConstants.MatchUris.Any, "Any");
            return new List<Claim>(new Claim[] { claim });
        }
    }
}
