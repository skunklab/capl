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
using System.Configuration;
using Capl.Services.Configuration;

namespace Capl.Configuration
{
    public class ExtensionsSection : ConfigurationSection
    {
        public ExtensionsSection()
        {
        }

        [ConfigurationProperty("matchExpressions", IsRequired = false)]
        public ExtensionCollection MatchExtensions
        {
            get { return (ExtensionCollection)base["matchExpressions"]; }
            set { base["matchExpressions"] = value; }
        }

        [ConfigurationProperty("operations", IsRequired = false)]
        public ExtensionCollection OperationExtensions
        {
            get { return (ExtensionCollection)base["operations"]; }
            set { base["operations"] = value; }
        }

        [ConfigurationProperty("transforms", IsRequired = false)]
        public ExtensionCollection TransformExtensions
        {
            get { return (ExtensionCollection)base["transforms"]; }
            set { base["transforms"] = value; }
        }

        [ConfigurationProperty("caching", IsRequired = false)]
        public ExtensionTypeElement CachingExtension
        {
            get { return (ExtensionTypeElement)base["caching"]; }
            set { base["caching"] = value; }
        }

        [ConfigurationProperty("store", IsRequired = false)]
        public ExtensionTypeElement StoreExtension
        {
            get { return (ExtensionTypeElement)base["store"]; }
            set { base["store"] = value; }
        }
    }
}
