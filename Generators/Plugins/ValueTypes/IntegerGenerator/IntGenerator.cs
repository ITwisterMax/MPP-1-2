using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Int type generator
    /// </summary>
    public class IntGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for IntGenerator
        /// </summary>
        public IntGenerator()
        {
            ItemType = typeof(int);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>int</returns>
        public override object Generate()
        {
            // Generate random int value
            var buffer = new byte[4];
            Random.NextBytes(buffer);

            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
