using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Float type generator
    /// </summary>
    public class FloatGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for FloatGenerator
        /// </summary>
        public FloatGenerator()
        {
            ItemType = typeof(float);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>float</returns>
        public override object Generate()
        {
            // Generate random float value
            var buffer = new byte[4];
            Random.NextBytes(buffer);

            return BitConverter.ToSingle(buffer, 0);
        }
    }
}
