using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace CSharpReflection
{
    class CSharpReflectionTest
    {
        /*
         * OBJETIVO: 
         * Aprender como setear una var de inst. de un objeto dinamicamente 
         * mediante TDD.
         */

        [Test]
        public void CanCreateAnInstanceDinmaically_Test0001()
        {
            var objectType = typeof(User);

            var instance = CreateInstanceDynamically(objectType);

            Assert.That(instance != null);
            Assert.That(String.IsNullOrEmpty(instance.Name));
        }

        [Test]
        public void CanReadAnInstanceVariableDinmaically_Test0002()
        {
            var objectType = typeof(User);
            var instance = CreateInstanceDynamically(objectType);

            var properties = objectType.GetProperties();
            var property =  properties.First();
            var propertyName = property.Name;
            var propertyValue = property.GetValue(instance);
            
            Debug.Assert(propertyName != null);
            Assert.That(propertyName.Equals("Name"));
            Assert.That(String.IsNullOrEmpty(propertyValue));
        }

        [Test]
        public void CanReadASpecificInstanceVariableDinmaically_Test0003()
        {
            var objectType = typeof(User);
            var instanceVariableToRead = "Name";
            var instance = CreateInstanceDynamically(objectType);

            dynamic propertyValue = ReadInstanceVariable(objectType, instanceVariableToRead, instance);

            Assert.That(String.IsNullOrEmpty(propertyValue));
        }

        [Test]
        public void CanChangeAnInstanceVariableValueDinamically_Test0004()
        {
            var objectType = typeof(User);
            var nameOfInstanceVariable = "Name";
            var newValue = "Pepe";
            var instance = CreateInstanceDynamically(objectType);

            SetValueDynamically(instance, nameOfInstanceVariable, newValue);

            Assert.That(instance.Name.Equals(newValue));
        }



        private dynamic CreateInstanceDynamically(Type objectType)
        {
            var assembly = objectType.Assembly;
            var assemblyString = objectType.FullName;
            return assembly.CreateInstance(assemblyString);
        }

        private dynamic ReadInstanceVariable(Type objectType, string instanceVariableToRead, dynamic instance)
        {
            return RetrieveProperty(objectType, instanceVariableToRead).GetValue(instance);
        }

        private void SetValueDynamically(dynamic instance, string nameOfInstanceVariable, string newValue)
        {
            instance.GetType().GetProperty(nameOfInstanceVariable).SetValue(instance, newValue, null);
        }

        private PropertyInfo RetrieveProperty(Type objectType, string instanceVariableToRead)
        {
            return objectType.GetProperties().First(p => p.Name.Equals(instanceVariableToRead));
        }
    }
}
