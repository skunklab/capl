
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Constants used for Access Control
    /// </summary>
    public static class AuthorizationConstants
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class Namespaces
        {
            public const string Xmlns = "http://schemas.authz.org/capl";
            public const string AuthorizationOperations = "http://schemas.authz.org/capl/operation";
            public const string TransformOperations = "http://schemas.authz.org/capl/transform";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class Elements
        {
            public const string AuthorizationPolicy = "AuthorizationPolicy";
            public const string Rule = "Rule";
            public const string LogicalAnd = "LogicalAnd";
            public const string LogicalOr = "LogicalOr";
            public const string Operation = "Operation";
            public const string SourceClaim = "SourceClaim";
            public const string TargetClaim = "TargetClaim";
            public const string Transforms = "Transforms";
            public const string Transform = "Transform";
            public const string Match = "Match";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class Attributes
        {
            public const string PolicyId = "PolicyID";
            public const string Evaluates = "Evaluates";
            public const string ClaimType = "ClaimType";
            public const string Required = "Required";
            public const string Type = "Type";
            public const string Issuer = "Issuer";
            public const string TermId = "TermID";
            public const string Delegation = "Delegation";
            public const string TransformId = "TransformID";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class OperationUris
        {            
            public const string Equal = "http://schemas.authz.org/capl/operation#Equal";
            public const string NotEqual = "http://schemas.authz.org/capl/operation#NotEqual";
            public const string Exists = "http://schemas.authz.org/capl/operation#Exists";
            public const string Contains = "http://schemas.authz.org/capl/operation#Contains";
            public const string EqualDateTime = "http://schemas.authz.org/capl/operation#EqualDateTime";
            public const string EqualNumeric = "http://schemas.authz.org/capl/operation#EqualNumeric";
            public const string GreaterThan = "http://schemas.authz.org/capl/operation#GreaterThan";
            public const string GreaterThanDateTime = "http://schemas.authz.org/capl/operation#GreaterThanDateTime";            
            public const string GreaterThanOrEqual = "http://schemas.authz.org/capl/operation#GreaterThanOrEqual";
            public const string GreaterThanOrEqualDateTime = "http://schemas.authz.org/capl/operation#GreaterThanOrEqualDateTime";
            public const string LessThan = "http://schemas.authz.org/capl/operation#LessThan";
            public const string LessThanDateTime = "http://schemas.authz.org/capl/operation#LessThanDateTime";
            public const string LessThanOrEqual = "http://schemas.authz.org/capl/operation#LessThanOrEqual";
            public const string LessThanOrEqualDateTime = "http://schemas.authz.org/capl/operation#LessThanOrEqualDateTime";
            public const string BetweenDateTime = "http://schemas.authz.org/capl/operation#BetweenDateTime";
            
        }        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class TransformUris
        {
            public const string Add = "http://schemas.authz.org/capl/transform#Add";
            public const string Remove = "http://schemas.authz.org/capl/transform#Remove";
            public const string Replace = "http://schemas.authz.org/capl/transform#Replace";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class MatchUris
        {
            public const string Literal = "http://schemas.authz.org/capl/match#Literal";
            public const string Pattern = "http://schemas.authz.org/capl/match#Pattern";
            public const string ComplexType = "http://schemas.authz.org/capl/match#ComplexType";
            public const string Any = "http://schemas.authz.org/capl/match#Any";
        }
    }
}
