using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     UShort type generator
    /// </summary>
    public class UShortGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for UShortGenerator
        /// </summary>
        public UShortGenerator()
        {
            ItemType = typeof(ushort);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>ushort</returns>
        public override object Generate()
        {
            // Generate random ushort value
            return (ushort)Random.Next(65536);
        }
    }
}
