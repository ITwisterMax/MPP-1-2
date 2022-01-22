using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Decimal type generator
    /// </summary>
    public class DecimalGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for DecimalGenerator
        /// </summary>
        public DecimalGenerator()
        {
            ItemType = typeof(decimal);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>decimal</returns>
        public override object Generate()
        {
            // Generate random decimal value
            bool sign = Random.Next(2) == 1;

            return new decimal(
                Random.Next() - -2147483648,
                Random.Next() - -2147483648,
                Random.Next() - -2147483648,
                sign,
                (byte)Random.Next(29)
            );
        }
    }
}
