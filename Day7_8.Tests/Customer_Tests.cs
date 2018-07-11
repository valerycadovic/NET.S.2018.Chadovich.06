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

        [TestCase("", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100")]
        [TestCase("G", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100")]
        [TestCase("NR", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("nr", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("nR", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("RP", ExpectedResult = "1,000,000.00, +1 (425) 555-0100")]
        [TestCase("R", ExpectedResult = "1,000,000.00")]
        [TestCase("P", ExpectedResult = "+1 (425) 555-0100")]
        [TestCase("p", ExpectedResult = "+1 (425) 555-0100")]
        public string Can_Format_By_Format_String(string format) =>
            customer.ToString(format, CultureInfo.InvariantCulture);
    }
}
