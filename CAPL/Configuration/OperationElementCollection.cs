﻿/*
Claims Authorization Policy Langugage SDK ver. 1.0 
Copyright (c) Matt Long labskunk@gmail.com 
All rights reserved. 
MIT License
*/



namespace Capl.Configuration
{
    using Capl.Authorization.Operations;
    using Capl.Services.Configuration;
    using System;
    using System.Configuration;
    using System.Globalization;

    [ConfigurationCollection(typeof(UriNamedExtensionElement))]
    public class OperationElementCollection : ConfigurationElementCollection
    {
        private OperationsDictionary operations;
        private ExtensionCollection extensions;
        private Action<Type, OperationsDictionary> addOpAsType;
        private Action<Operation, OperationsDictionary> addOpAsInstance;

        public OperationElementCollection()
        {
            operations = new OperationsDictionary();
            addOpAsType = (typeRef, op) =>
            {
                Operation operation = (Operation)Activator.CreateInstance(Type.GetType(typeRef.FullName));
                op.Add(operation.Uri.ToString(), operation);
            };

            addOpAsInstance = (instance, op) =>
            {
                op.Add(instance.Uri.ToString(), instance);
            };

            addOpAsType(typeof(EqualDateTimeOperation), operations);
            addOpAsType(typeof(EqualNumericOperation), operations);
            addOpAsType(typeof(EqualOperation), operations);
            addOpAsType(typeof(NotEqualOperation), operations);
            addOpAsType(typeof(ExistsOperation), operations);
            addOpAsType(typeof(GreaterThanDateTimeOperation), operations);
            addOpAsType(typeof(GreaterThanOperation), operations);
            addOpAsType(typeof(GreaterThanOrEqualDateTimeOperation), operations);
            addOpAsType(typeof(GreaterThanOrEqualOperation), operations);
            addOpAsType(typeof(LessThanDateTimeOperation), operations);
            addOpAsType(typeof(LessThanOperation), operations);
            addOpAsType(typeof(LessThanOrEqualDateTimeOperation), operations);
            addOpAsType(typeof(LessThanOrEqualOperation), operations);

            ExtensionsSection section = (ExtensionsSection)ConfigurationManager.GetSection(ConfigurationConstants.ExtensionsSection);
            
            if (section != null && section.OperationExtensions != null && section.OperationExtensions.Count > 0)
            {
                extensions = section.OperationExtensions;
            }
        }

        public OperationsDictionary Operations
        {
            get { return this.operations; }
            internal set { this.operations = value; }
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
                    addOpAsInstance((Operation)Activator.CreateInstance(Type.GetType(ext.TypeName)), operations);
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
            return operations.ContainsKey(elem.Uri);
        }
    }
}
