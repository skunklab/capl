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
using System.Linq;
using System.Text;
using System.Configuration;
using Capl.Authorization.Matching;
using Capl.Services.Configuration;
using System.Globalization;

namespace Capl.Configuration
{
    [ConfigurationCollection(typeof(UriNamedExtensionElement))]
    public class MatchElementCollection : ConfigurationElementCollection
    {

        private MatchExpressionDictionary matchExpressions;
        private ExtensionCollection extensions;
        private Action<Type, MatchExpressionDictionary> addOpAsType;
        private Action<MatchExpression, MatchExpressionDictionary> addOpAsInstance;

        public MatchElementCollection()
        {
            matchExpressions = new MatchExpressionDictionary();
            addOpAsType = (typeRef, op) =>
            {
                MatchExpression matchExpression = (MatchExpression)Activator.CreateInstance(Type.GetType(typeRef.FullName));
                op.Add(matchExpression.Uri.ToString(), matchExpression);
            };

            addOpAsInstance = (instance, op) =>
            {
                op.Add(instance.Uri.ToString(), instance);
            };

            addOpAsType(typeof(LiteralMatchExpression), matchExpressions);
            addOpAsType(typeof(PatternMatchExpression), matchExpressions);
            addOpAsType(typeof(ComplexTypeMatchExpression), matchExpressions);
            addOpAsType(typeof(UnaryMatchExpression), matchExpressions);

            ExtensionsSection section = (ExtensionsSection)ConfigurationManager.GetSection(ConfigurationConstants.ExtensionsSection);

            if (section != null && section.MatchExtensions != null && section.MatchExtensions.Count > 0)
            {
                extensions = section.MatchExtensions;
            }
        }

        public MatchExpressionDictionary MatchExpressions
        {
            get { return this.matchExpressions; }
            internal set { this.matchExpressions = value; }
        }

        public ExtensionCollection Extensions
        {
            get { return this.extensions; }
            internal set { this.extensions = value; }
        }


        public void Add(ConfigurationElement element)
        {
            BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new UriNamedExtensionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UriNamedExtensionElement)element).Uri;
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            bool extensionFound = false;
            base.BaseAdd(element);
            UriNamedExtensionElement elem = (UriNamedExtensionElement)element;

            if (extensions == null || extensions.Count == 0)
            {
                throw new ConfigurationErrorsException("Extensions not configured for operations.");
            }

            if (string.IsNullOrEmpty(elem.ExtensionName))
            {
                throw new ConfigurationErrorsException("Cannot add operation extension. The extension name attribute is missing from the operation.");
            }

            foreach (ExtensionTypeElement ext in extensions)
            {
                if (ext.Name == elem.ExtensionName)
                {
                    addOpAsInstance((MatchExpression)Activator.CreateInstance(Type.GetType(ext.TypeName)), matchExpressions);
                    extensionFound = true;
                }
            }

            if (!extensionFound)
            {
                throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture, "No extension configured for operation {0}", elem.ExtensionName));
            }
        }

        protected override bool IsElementRemovable(ConfigurationElement element)
        {
            UriNamedExtensionElement elem = (UriNamedExtensionElement)element;
            return matchExpressions.ContainsKey(elem.Uri);
        }
    }
}
