using System;
using System.Collections;
using System.Collections.Generic;
using Generators.Model;

namespace Generators.Plugins.ReferenceTypes
{
    /// <summary>
    ///     List type generator
    /// </summary>
    public class ListGenerator : AbstractGeneratorCollection
    {
        /// <summary>
        ///     Public constructor for ListGenerator
        /// </summary>
        /// 
        /// <param name="generators">Dictionary with generators</param>
        public ListGenerator(Dictionary<Type, AbstractGenerator> generators) : base(generators)
        {
            ItemType = typeof(List<>);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <param name="type">Generator collection type</param>
        /// 
        /// <returns>List<itemType></returns>
        public override object Generate(Type itemType)
        {
            try
            {
                // Create List instance with current type
                IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

                // Initialize base types generators
                var n = Random.Next(10, 50);
                for (int i = 0; i < n; i++)
                {
                    list.Add(Generators[itemType].Generate());
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
    }
}
