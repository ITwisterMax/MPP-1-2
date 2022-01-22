using System;
using System.Linq;
using Generators.Model;

namespace Generators.Plugins.ReferenceTypes
{
    /// <summary>
    ///     String type generator
    /// </summary>
    public class StringGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Allowed symbols collection
        /// </summary>
        public string AllowedSymbols { get; }

        /// <summary>
        ///     Public constructor for StringGenerator
        /// </summary>
        public StringGenerator()
        {
            ItemType = typeof(string);
            AllowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>string</returns>
        public override object Generate()
        {
            // Generate random string from allowed symbols
            return new string(
                Enumerable.Repeat(AllowedSymbols, AllowedSymbols.Length)
                .Select(s => s[Random.Next(s.Length)])
                .ToArray()
            );
        }
    }
}
