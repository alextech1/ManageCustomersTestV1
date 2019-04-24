using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassProject2.Data;
using System.Linq;

namespace Tests.ClassProject2.Data
{
    [TestClass]
    public class CustomersTests
    {
        [TestMethod]
        public void GetCustomers_Returns3()
        {
            var target = new Customers();

            var actual = target.GetAll();

            Assert.AreEqual(3, actual.Count());
        }
    }
}
