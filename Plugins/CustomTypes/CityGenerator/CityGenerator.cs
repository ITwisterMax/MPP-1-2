using System;
using System.Collections.Generic;
using Generators.Model;

namespace Generators.Plugins.CustomTypes
{
    /// <summary>
    ///     Custom City type generator
    /// </summary>
    public class CityGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Allowed cities collection
        /// </summary>
        public List<string> AllowedCities { get; }

        /// <summary>
        ///     Public constructor for CityGenerator
        /// </summary>
        public CityGenerator()
        {
            ItemType = typeof(string);
            AllowedCities = new List<string>
            {
                "Minsk", "Moscow", "Kiev", "Tokio", "Washington"
            };
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>string</returns>
        public override object Generate()
        {
            // Generate random city from allowed collection
            return AllowedCities[Random.Next(5)].Clone();
        }
    }
}
