using System;
using NUnit.Framework;

namespace CSharpReflection
{
    class UserTest
    {
        [Test]
        public void WhenInstanciateAnUserThenItsNameIsEmpty_Test0001()
        {
            var user = new User();

            Assert.That(String.IsNullOrEmpty(user.Name));
        }

        [Test]
        public void AnUserNameCanBeModifiedExternally_Test0001()
        {
            var user = new User();
            var newName = "Pepe";

            user.Name = newName;

            Assert.That(user.Name.Equals(newName));
        }
    }
}