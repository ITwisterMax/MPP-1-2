using System;
using System.Reflection;
using Generators.Model;

namespace FakerLib.Helper
{
    /// <summary>
    ///     Plugin loader class
    /// </summary>
    public static class Loader
    {
        /// <summary>
        ///     Load plugin from the specified path
        /// </summary>
        /// 
        /// <param name="path">File path</param>
        /// <param name="generatorType">Generator type</param>
        /// 
        /// <returns>AbstractGenerator|null</returns>
        public static AbstractGenerator LoadPlugin(string path, Type itemType)
        {
            try
            {
                // Get all types from dll
                var types = Assembly.LoadFrom(path).GetTypes();

                // Find necessary type and create its instance
                foreach (var type in types)
                {
                    var instance = (AbstractGenerator)Activator.CreateInstance(type);

                    if (instance.ItemType == itemType)
                    {
                        return instance;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

            
        }
    }
}
