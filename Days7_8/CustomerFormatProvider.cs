namespace Days7_8
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Format provider for <see cref="Customer"/>
    /// </summary>
    /// <seealso cref="System.IFormatProvider" />
    /// <seealso cref="System.ICustomFormatter" />
    public class CustomerFormatProvider : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// The default format
        /// </summary>
        private const string DefaultFormat = "G";

        /// <summary>
        /// The parent
        /// </summary>
        private readonly IFormatProvider parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerFormatProvider"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public CustomerFormatProvider(IFormatProvider parent)
        {
            this.parent = parent;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Days7_8.CustomerFormatProvider" /> class.
        /// </summary>
        public CustomerFormatProvider() : this(CultureInfo.CurrentCulture) { }

        /// <inheritdoc />
        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns>
        /// An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, <see langword="null" />.
        /// </returns>
        public object GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">
        /// A format string containing formatting specifications.
        /// This parameter may be REV - reversed NRP of <see cref="Customer"/> or same as <see cref="Customer"/> format parameter
        /// </param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>
        /// The string representation of the value of <paramref name="arg" />, formatted as specified by <paramref name="format" /> and <paramref name="formatProvider" />.
        /// </returns>
        /// <exception cref="System.FormatException">Throws when the format is invalid</exception>
        /// <exception cref="System.ArgumentNullException">Thows when arg is null</exception>
        /// <exception cref="System.ArgumentException">Throws when arg is not <see cref="Customer"/>/></exception>
        public string Format(string format, object arg, IFormatProvider formatProvider = null)
        {
            if (arg is null)
            {
                throw new ArgumentNullException($"{nameof(arg)} is null");
            }

            if (!(arg is Customer customer))
            {
                throw new ArgumentException($"{nameof(arg)} must have a type of Customer");
            }

            if (formatProvider is null)
            {
                formatProvider = parent;
            }

            if (!format.Equals("REV", StringComparison.OrdinalIgnoreCase))
            {
                return customer.ToString(format, formatProvider);
            }
            
            return Reverse(customer.ToString("NRP", formatProvider));
        }

        /// <summary>
        /// Reverses the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Reversed string</returns>
        private string Reverse(string str)
        {
            char[] result = str.ToCharArray();
            Array.Reverse(result);
            return new string(result);
        }
    }
}
