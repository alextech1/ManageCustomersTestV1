using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassProject2.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Tests.ClassProject2.Data
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void Ctor_SetsId ()
        {
            var expectedId = 10;

            //Act
            var target = new Customer(expectedId);

            //Assert
            Assert.AreEqual(expectedId, target.Id);
        }

        [TestMethod]
        public void Ctor_UseNegativeId()
        {
            var expectedId = -10;

            //Act
            var target = new Customer(expectedId);

            //Assert
            Assert.AreEqual(0, target.Id);
        }  

        [TestMethod]
        public void Customer_NameIsPascalCased ()
        {
            var target = new Customer()
            {
                FirstName = "bob",
                LastName = "miller"
            };

            Assert.AreEqual("Bob", target.FirstName);
            Assert.AreEqual("Miller", target.LastName);
        }
        
        [TestMethod]
        public void Customer_GetCustomer ()
        {
            var customers = new Customers();

            var customer = customers.Get(100);

            Assert.IsNotNull(customer);
        }
        //[TestMethod]      
        //public void TestCollection ()
        //{
        //    var intValues = new List<int>();
        //    intValues.Add(10);

        //    foreach (var firstValue in intValues)
        //    {
        //    };

        //    var oldIntValues = new ArrayList();
        //    oldIntValues.Add(new LineItem());
        //    oldIntValues.Add(new LineItem());
        //    oldIntValues.Add(10);
            
        //    foreach (var oldValue in oldIntValues.OfType<LineItem>())
        //    {                
        //    };
        //}
    }
}
