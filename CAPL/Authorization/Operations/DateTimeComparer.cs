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

namespace Capl.Authorization.Operations
{
    using System;
    using System.Collections;
    using System.Xml;

    /// <summary>
    /// Compares two DateTime types by UTC.
    /// </summary>
    public class DateTimeComparer : IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeComparer"/> class.
        /// </summary>
        public DateTimeComparer()
        {
        }

        #region IComparer Members

        /// <summary>
        /// Compares two DateTime types by UTC.
        /// </summary>
        /// <param name="x">LHS datetime parameter to test.</param>
        /// <param name="y">RHS datatime parameter to test.</param>
        /// <returns>0 for equality; 1 for x greater than y; -1 for x less than y.</returns>
        public int Compare(object x, object y)
        {
            DateTime left = XmlConvert.ToDateTime((string)x, XmlDateTimeSerializationMode.Utc);
            DateTime right = XmlConvert.ToDateTime((string)y, XmlDateTimeSerializationMode.Utc);

            if (left == right)
            { 
                return 0; 
            }

            if (left < right) 
            { 
                return -1; 
            }

            return 1;
        }

        #endregion
    }
}
