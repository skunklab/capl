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

namespace Capl.Authorization.Matching
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Security.Claims;

    /// <summary>
    /// Matches the string literal of a claim type and optional regular expression of the claim value.
    /// </summary>
    public class PatternMatchExpression : MatchExpression
    {
        public static Uri MatchUri
        {
            get { return new Uri(AuthorizationConstants.MatchUris.Pattern); }
        }
        public override Uri Uri
        {
            get { return new Uri(AuthorizationConstants.MatchUris.Pattern); }
        }

        public override IList<Claim> MatchClaims(IEnumerable<Claim> claims, string claimType, string pattern)
        {
            if (claims == null)
            {
                throw new ArgumentNullException("claims");
            }

            Regex regex = new Regex(pattern);

            ClaimsIdentity ci = new ClaimsIdentity(claims);
            IEnumerable<Claim> claimSet = ci.FindAll(delegate(Claim claim)
            {
                return (claimType == claim.Type);
            });


            if (pattern == null)
            {
                return new List<Claim>(claimSet);
            }

            List<Claim> claimList = new List<Claim>();
            IEnumerator<Claim> en = claimSet.GetEnumerator();

            while (en.MoveNext())
            {
                if (regex.IsMatch(en.Current.Value))
                {
                    claimList.Add(en.Current);
                }
            }

            return claimList;
        }
    }
}
