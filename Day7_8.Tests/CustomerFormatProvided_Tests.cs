using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Days7_8;

namespace Day7_8.Tests
{
    [TestFixture]
    public class CustomerFormatProvided_Tests
    {
        private readonly Customer customer =
            new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);

        [TestCase("nr", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("REV", ExpectedResult = "0010-555 )524( 1+ ,00.000,000,1 ,rethciR yerffeJ")]
        [TestCase("rev", ExpectedResult = "0010-555 )524( 1+ ,00.000,000,1 ,rethciR yerffeJ")]
        public string Can_Format_By_Format_String(string format)
        {
            return new CustomerFormatProvider().Format(format, customer, CultureInfo.InvariantCulture);
        }

        [Test]
        public void Format_Throws_ArgumentNullException_When_arg_Is_Null() =>
            Assert.Throws<ArgumentNullException>(
                () => new CustomerFormatProvider().Format("REV", null, CultureInfo.InvariantCulture));

        [TestCase("erer")]
        public void Format_Throws_FormatException(string format) =>
            Assert.Throws<FormatException>(
            () => new CustomerFormatProvider().Format(format, customer, CultureInfo.InvariantCulture));

        [Test]
        public void Format_Throws_ArgumenrException_When_arg_Is_Not_Customer() =>
            Assert.Throws<ArgumentException>(
                () => new CustomerFormatProvider().Format("REV", 25, CultureInfo.InvariantCulture));
    }
}
