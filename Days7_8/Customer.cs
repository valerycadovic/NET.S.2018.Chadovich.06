namespace Days7_8
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Representation of a customer.
    /// </summary>
    /// <seealso cref="System.IFormattable" />
    public class Customer : IFormattable
    {
        /// <summary>
        /// The available format chars
        /// </summary>
        private const string AvailableFormatChars = "NRP";

        /// <summary>
        /// The name of a customer
        /// </summary>
        private string name;

        /// <summary>
        /// The phone of a customer
        /// </summary>
        private string phone;

        /// <summary>
        /// The revenue of a customer
        /// </summary>
        private decimal revenue;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="revenue">The revenue.</param>
        public Customer(string name, string phone, decimal revenue)
        {
            this.Name = name;
            this.ContactPhone = phone;
            this.Revenue = revenue;
        }
        
        /// <summary>
        /// Gets the name of a customer.
        /// </summary>
        /// <value>
        /// The name of a customer
        /// </value>
        public string Name
        {
            get => this.name;

            private set
            {
                Regex regex = new Regex(@"^([A-Z][a-z]*\s)*[A-Z][a-z]*$");

                if (!regex.IsMatch(value))
                {
                    throw new FormatException($"{nameof(value)} is invalid");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets the contact phone.
        /// </summary>
        /// <value>
        /// The contact phone.
        /// </value>
        public string ContactPhone
        {
            get => this.phone;

            private set
            {
                Regex regex = new Regex(@"^\+\d+\s(\(\d+\))\s\d{3}-\d{4}$");

                if (!regex.IsMatch(value))
                {
                    throw new FormatException($"{nameof(value)} is invalid");
                }

                this.phone = value;
            }
        }

        /// <summary>
        /// Gets the revenue.
        /// </summary>
        /// <value>
        /// The revenue.
        /// </value>
        public decimal Revenue
        {
            get => Math.Round(this.revenue, 2);

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)}: Customer cannot have negative revenue");
                }

                this.revenue = value;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ToString("G");
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrEmpty(format) || format.Equals("G", StringComparison.InvariantCultureIgnoreCase))
            {
                string result = string.Empty;
                result += string.Format(formatProvider, "{0}", this.Name) + ", ";
                result += this.FormDecimal(this.Revenue) + ", ";
                result += string.Format(formatProvider, "{0}", this.ContactPhone);
                return result;
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            this.ValidateFormat(format);

            return this.FormString(format, formatProvider);
        }

        /// <summary>
        /// Validates the format.
        /// </summary>
        /// <param name="format">The format.</param>
        private void ValidateFormat(string format)
        {
            if (format.Length > AvailableFormatChars.Length)
            {
                throw new FormatException($"{nameof(format)} is invalid. See the documentation");
            }

            string upperFormat = format.ToUpperInvariant();
            string reminder = AvailableFormatChars;

            foreach (var item in upperFormat)
            {
                if (reminder.IndexOf(item) == -1)
                {
                    throw new FormatException($"{nameof(format)} is invalid. See the documentation");
                }

                reminder = reminder.Remove(reminder.IndexOf(item), 1);
            }
        }

        /// <summary>
        /// Forms the string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>Formatted string</returns>
        private string FormString(string format, IFormatProvider formatProvider)
        {
            string result = string.Empty;
            string ignoredCaseFormat = format.ToUpperInvariant();

            for (int i = 0; i < ignoredCaseFormat.Length; i++)
            {
                switch (ignoredCaseFormat[i])
                {
                    case 'N':
                        result += string.Format(formatProvider, "{0}", this.Name);
                        break;
                    case 'P':
                        result += string.Format(formatProvider, "{0}", this.ContactPhone);
                        break;
                    case 'R':
                        result += this.FormDecimal(this.Revenue);
                        break;
                }

                if (i != format.Length - 1)
                {
                    result += ", ";
                }
            }

            return result;
        }

        /// <summary>
        /// Forms the decimal.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>The string.</returns>
        private string FormDecimal(decimal d)
        {
            string result = string.Empty;

            NumberFormatInfo f = new NumberFormatInfo();
            f.NumberGroupSeparator = ",";
            result += this.Revenue.ToString("N2", f);

            return result;
        }
    }
}
