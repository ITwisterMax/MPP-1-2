using System;
using System.Collections.Generic;

namespace Generators.Model
{
    /// <summary>
    ///     Abstract generator collection
    /// </summary>
    public abstract class AbstractGeneratorCollection
    {
        /// <summary>
        ///     Random value instance
        /// </summary>
        public static readonly Random Random;

        /// <summary>
        ///     Generator collection type
        /// </summary>
        public Type ItemType { get; protected set; }

        /// <summary>
        ///     Dictionary with generators
        /// </summary>
        public Dictionary<Type, AbstractGenerator> Generators { get; private set; }

        /// <summary>
        ///     Public constructor for AbstractGeneratorCollection
        /// </summary>
        static AbstractGeneratorCollection()
        {
            Random = new Random();
        }

        /// <summary>
        ///     Protected constructor for AbstractGeneratorCollection
        /// </summary>
        /// 
        /// <param name="generators">Dictionary with generators</param>
        protected AbstractGeneratorCollection(Dictionary<Type, AbstractGenerator> generators)
        {
            Generators = generators;
        }

        /// <summary>
        ///     Abstract method Generate
        /// </summary>
        /// 
        /// <param name="type">Generator collection type</param>
        public abstract object Generate(Type itemType);
    }
}
