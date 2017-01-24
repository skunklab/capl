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
    using Capl.Authorization.Operations;
    using Capl.Configuration;

    /// <summary>
    /// An abstract operation that performs an authorization function.
    /// </summary>
    public abstract class Operation
    {
        /// <summary>
        /// Gets the URI that identifies the operation.
        /// </summary>
        public abstract Uri Uri { get; }

        /// <summary>
        /// Creates an AuthorizationOperation used to compare values.
        /// </summary>
        /// <param name="operationUri">Uri if the operation.</param>
        /// <param name="operations">A dictionary of operations.  The value may be null.</param>
        /// <returns>An AuthorizationOperation to compare values.</returns>
        public static Operation Create(Uri operationUri, OperationsDictionary operations)
        {
            if (operationUri == null)
            {
                throw new ArgumentNullException("operationUri");
            }

            Operation operation = null;

            if (operations == null)
            {
                operation = CaplConfigurationManager.Operations[operationUri.ToString()];
            }
            else
            {
                operation = operations[operationUri.ToString()];
            }

            return operation;
        }

        /// <summary>
        /// Executes the comparsion.
        /// </summary>
        /// <param name="left">LHS of the expression argument.</param>
        /// <param name="right">RHS of the expression argument.</param>
        /// <returns>True, if the compare is ture; otherwise false.</returns>
        public abstract bool Execute(string left, string right);
    }
}
