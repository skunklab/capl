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


using Capl.ServiceModel;
using Capl.Services.Configuration;
using System;
using System.Configuration;
using System.Xml;

namespace Capl.Configuration
{
    public class PolicyCacheElement : ConfigurationElement 
    {
        public PolicyCacheElement()
        {
            
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
        {
            ExtensionsSection section = (ExtensionsSection)ConfigurationManager.GetSection(ConfigurationConstants.ExtensionsSection);
            ExtensionTypeElement elem = section.CachingExtension;
            if (elem.Name == elementName)
            {
                ExtensionElement<ICaplCache> elemType = (ExtensionElement<ICaplCache>)Activator.CreateInstance(Type.GetType(elem.TypeName));

                for (int i = 0; i < reader.AttributeCount; i++)
                {
                    reader.MoveToAttribute(i);
                    string name = reader.LocalName;
                    string value = reader.Value;
                    elemType.SetProperty(name, value);

                }

                cache = elemType.Create();
                return true;
            }
            else
            {
                return false;
            }
        }

        private ICaplCache cache;
        public ICaplCache GetCache()
        {
            return cache;
        }



        

        



        

        
    }
}
