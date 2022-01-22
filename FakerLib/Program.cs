using System;
using FakerLib.Block;
using FakerLib.Test;
using Generators.Plugins.CustomTypes;

namespace FakerLib
{
    /// <summary>
    ///     Main class
    /// </summary>
    class Program
    {
        /// <summary>
        ///     Entry Point
        /// </summary>
        static void Main()
        {
            // Get custom faker config
            var config = new FakerConfig();
            config.Add<B, string, CityGenerator>(elem => elem.TestCity);

            // Get faker with custom config
            var faker = new Faker(config);

            // Create new DTO object
            var objectDTO = faker.Create<A>();

            Console.ReadLine();
        }
    }
}
