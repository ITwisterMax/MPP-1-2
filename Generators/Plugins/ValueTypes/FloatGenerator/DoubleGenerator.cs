using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Double type generator
    /// </summary>
    public class DoubleGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for DoubleGenerator
        /// </summary>
        public DoubleGenerator()
        {
            ItemType = typeof(double);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>double</returns>
        public override object Generate()
        {
            // Generate random double value
            var buffer = new byte[8];
            Random.NextBytes(buffer);

            return BitConverter.ToDouble(buffer, 0);
        }
    }
}
