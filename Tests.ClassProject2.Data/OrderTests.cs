using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassProject2.Data;

namespace Tests.ClassProject2.Data
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void Ctor_SetsId ()
        {
            var expectedId = 10;

            //Act
            var target = new Order(expectedId);

            //Assert
            Assert.AreEqual(expectedId, target.Id);
        }

        [TestMethod]
        public void Ctor_UseNegativeId()
        {
            var expectedId = -10;

            //Act
            var target = new Order(expectedId);

            //Assert
            Assert.AreEqual(0, target.Id);
        }

        [TestMethod]
        public void Ctor_HasEmptyLineItems()
        {
            var target = new Order();

            Assert.AreEqual(0, target.LineItems.Count);
        }
    }
}
