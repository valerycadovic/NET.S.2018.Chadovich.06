using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Days7_8;
using NUnit.Framework;

namespace Day7_8.Tests
{
    [TestFixture]
    public class Customer_Tests
    {
        private readonly Customer customer =
            new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
        
        [TestCase("NR", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("nr", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("nR", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("RP", ExpectedResult = "1,000,000.00, +1 (425) 555-0100")]
        [TestCase("NRP", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100")]
        [TestCase("R", ExpectedResult = "1,000,000.00")]
        [TestCase("P", ExpectedResult = "+1 (425) 555-0100")]
        [TestCase("p", ExpectedResult = "+1 (425) 555-0100")]
        public string Can_Format_By_Format_String(string format) =>
            customer.ToString(format, CultureInfo.InvariantCulture);

        [TestCase("")]
        [TestCase(null)]
        [TestCase("G")]
        public void Can_Format_By_Format_String_Default(string format)
        {
            Assert.AreEqual(
                $"Jeffrey Richter, {1000000.ToString("N", CultureInfo.CurrentCulture)}, +1 (425) 555-0100",
                customer.ToString(format)
                );
        }
        [TestCase("jeffrey richter", 1000000, "+1 (425) 555-0100")]
        [TestCase("Valery Chadovich", 45, "555-0100")]
        public void Ctor_Throws_FormatException_When_Params_Invalid(string name, decimal revenue, string phone) =>
            Assert.Throws<FormatException>(() => new Customer(name, phone, revenue));
        
        [TestCase("Valery Chadovich", -45, "+375 (25) 957-7240")]
        public void Ctor_Throws_When_ArgumentOutOfRangeException_When_Revenue_Negative(string name, decimal revenue, string phone) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new Customer(name, phone, revenue));
    }
}
