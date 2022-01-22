using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Short type generator
    /// </summary>
    public class ShortGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for ShortGenerator
        /// </summary>
        public ShortGenerator()
        {
            ItemType = typeof(short);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>short</returns>
        public override object Generate()
        {
            // Generate random short value
            return (short)Random.Next(-32768, 65536);
        }
    }
}
