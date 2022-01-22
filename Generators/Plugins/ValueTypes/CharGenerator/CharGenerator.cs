using System;
using Generators.Model;

namespace Generators.Plugins.ValueTypes
{
    /// <summary>
    ///     Char type generator
    /// </summary>
    public class CharGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Allowed symbols collection
        /// </summary>
        public string AllowedSymbols { get; }

        /// <summary>
        ///     Public constructor for CharGenerator
        /// </summary>
        public CharGenerator()
        {
            ItemType = typeof(char);
            AllowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>char</returns>
        public override object Generate()
        {
            // Generate random symbol from allowed collection
            return AllowedSymbols[Random.Next(AllowedSymbols.Length)];
        }
    }
}
