using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Byte type generator
    /// </summary>
    public class ByteGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for ByteGenerator
        /// </summary>
        public ByteGenerator()
        {
            ItemType = typeof(byte);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>byte</returns>
        public override object Generate()
        {
            // Generate random byte value
            return (byte)Random.Next(256);
        }
    }
}
