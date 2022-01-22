using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     UInt type generator
    /// </summary>
    public class UIntGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for UIntGenerator
        /// </summary>
        public UIntGenerator()
        {
            ItemType = typeof(uint);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>uint</returns>
        public override object Generate()
        {
            // Generate random uint value
            var buffer = new byte[4];
            Random.NextBytes(buffer);

            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
