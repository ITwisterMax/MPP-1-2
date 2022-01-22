using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     ULong type generator
    /// </summary>
    public class ULongGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for ULongGenerator
        /// </summary>
        public ULongGenerator()
        {
            ItemType = typeof(ulong);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>ulong</returns>
        public override object Generate()
        {
            // Generate random ulong value
            var buffer = new byte[8];
            Random.NextBytes(buffer);

            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}
