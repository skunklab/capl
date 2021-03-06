﻿/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/



namespace Capl.Configuration
{
    using Capl.Authorization.Transforms;
    using Capl.Services.Configuration;
    using System;
    using System.Configuration;
    using System.Globalization;

    [ConfigurationCollection(typeof(UriNamedExtensionElement))]
    public class TransformElementCollection : ConfigurationElementCollection
    {
        private TransformsDictionary transforms;
        private ExtensionCollection extensions;
        private Action<Type, TransformsDictionary> addTranAsType;
        private Action<TransformAction, TransformsDictionary> addTranAsInstance;

        public TransformElementCollection()
        {
            transforms = new TransformsDictionary();
            addTranAsType = (typeRef, op) =>
            {
                TransformAction operation = (TransformAction)Activator.CreateInstance(Type.GetType(typeRef.FullName));
                op.Add(operation.Uri.ToString(), operation);
            };

            addTranAsInstance = (instance, op) =>
            {
                op.Add(instance.Uri.ToString(), instance);
            };

            addTranAsType(typeof(AddTransformAction), transforms);
            addTranAsType(typeof(RemoveTransformAction), transforms);
            addTranAsType(typeof(ReplaceTransformAction), transforms);

            ExtensionsSection section = (ExtensionsSection)ConfigurationManager.GetSection(ConfigurationConstants.ExtensionsSection);
            
            if (section != null && section.TransformExtensions != null)
            {
                extensions = section.TransformExtensions;
            }
        }

        public TransformsDictionary Transforms
        {
            get { return transforms; }
        }

        public void Add(UriNamedExtensionElement element)
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

            if (extensions == null)
            {
                throw new ConfigurationErrorsException("Extensions not configured for transforms.");
            }

            if (string.IsNullOrEmpty(elem.ExtensionName))
            {
                throw new ConfigurationErrorsException("Cannot add transform extension. The extension name attribute missing from transform.");

            }

            foreach (ExtensionTypeElement ext in extensions)
            {
                if (ext.Name == elem.ExtensionName)
                {
                    addTranAsInstance((TransformAction)Activator.CreateInstance(Type.GetType(ext.TypeName)), transforms);
                    extensionFound = true;
                }
            }

            if (!extensionFound)
            {
                throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture, "No extension configured for transform {0}", elem.ExtensionName));
            }
        }

        protected override bool IsElementRemovable(ConfigurationElement element)
        {
            UriNamedExtensionElement elem = (UriNamedExtensionElement)element;
            return transforms.ContainsKey(elem.Uri);
        }

    }
}
