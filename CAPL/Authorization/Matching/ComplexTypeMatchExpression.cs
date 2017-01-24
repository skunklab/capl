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
    using System.IO;
    using System.Text;
    using System.Xml.XPath;
    using System.Security.Claims;
    using System.Web;

    /// <summary>
    ///  Matches the string literal of a claim type and optional XSLT expression to match the claim value.
    /// </summary>
    /// <remarks>
    /// Assumes the claim value is encoded as Xml.
    /// </remarks>
    public class ComplexTypeMatchExpression : MatchExpression
    {

        public static Uri MatchUri
        {
            get { return new Uri(AuthorizationConstants.MatchUris.ComplexType); }
        }
        public override Uri Uri
        {
            get { return new Uri(AuthorizationConstants.MatchUris.ComplexType); }
        }

        public override IList<Claim> MatchClaims(IEnumerable<Claim> claims, string claimType, string xpath)
        {
            if (claims == null)
            {
                throw new ArgumentNullException("claims");
            }
            
            ClaimsIdentity ci = new ClaimsIdentity(claims);
            IEnumerable<Claim> claimSet = ci.FindAll(delegate(Claim claim)
            {
                if (claim.Type == claimType)
                {
                    string claimValue = HttpUtility.HtmlDecode(claim.Value);
                    using (Stream stream = new MemoryStream(UTF8Encoding.UTF32.GetBytes(claimValue)))
                    {
                        try
                        {
                            XPathDocument doc = new XPathDocument(stream);
                            XPathExpression expression = XPathExpression.Compile(xpath);

                            XPathNavigator nav = doc.CreateNavigator();
                            XPathNodeIterator iterator = nav.Select(expression);

                            while (iterator.MoveNext())
                            {
                                if (!string.IsNullOrEmpty(iterator.Current.Value))
                                {
                                    return true;
                                }
                            }
                        }
                        catch //if the claim type is not valid xml catch the exception so true cannot be returned and method will not fail
                        {
                        }
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            });

            return new List<Claim>(claimSet);
        }
    }
}
