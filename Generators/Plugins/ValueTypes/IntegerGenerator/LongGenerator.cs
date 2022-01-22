using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Long type generator
    /// </summary>
    public class LongGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for LongGenerator
        /// </summary>
        public LongGenerator()
        {
            ItemType = typeof(long);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>long</returns>
        public override object Generate()
        {
            // Generate random long value
            var buffer = new byte[8];
            Random.NextBytes(buffer);

            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
