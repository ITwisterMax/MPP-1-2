using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Boolean type generator
    /// </summary>
    public class BooleanGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for BooleanGenerator
        /// </summary>
        public BooleanGenerator()
        {
            ItemType = typeof(bool);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>bool</returns>
        public override object Generate()
        {
            // Generate random boolean value
            return Random.Next(2) == 0;
        }
    }
}
