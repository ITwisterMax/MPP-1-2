using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     SByte type generator
    /// </summary>
    public class SByteGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for SByteGenerator
        /// </summary>
        public SByteGenerator()
        {
            ItemType = typeof(sbyte);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>sbyte</returns>
        public override object Generate()
        {
            // Generate random sbyte value
            return (sbyte)Random.Next(-128, 256);
        }
    }
}
