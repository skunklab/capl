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
using System.Xml;

namespace Capl.Authorization.Operations
{
    public class BetweenDateTimeOperation : Operation
    {

        public static Uri OperationUri
        {
            get { return new Uri(AuthorizationConstants.OperationUris.BetweenDateTime); }
        }

        public override Uri Uri
        {
            get { return new Uri(AuthorizationConstants.OperationUris.BetweenDateTime); }
        }

        public override bool Execute(string left, string right)
        {
            ///the LHS is ignored and the RHS using a normalized string containing 2 xsd:dateTime values.
            ///the current time should be between the 2 dateTime values

            string[] parts = right.Split(new char[] { ' ' });
            DateTime startDate = XmlConvert.ToDateTime(parts[0], XmlDateTimeSerializationMode.Utc);
            DateTime endDate = XmlConvert.ToDateTime(parts[1], XmlDateTimeSerializationMode.Utc);
            DateTime now = DateTime.Now;

            return (startDate <= now && endDate >= now);
        }
    }
}
