using System;

namespace Generators.Model
{
    /// <summary>
    ///     Abstract generator
    /// </summary>
    public abstract class AbstractGenerator
    {
        /// <summary>
        ///     Random value instance
        /// </summary>
        protected static readonly Random Random;

        /// <summary>
        ///     Generator type
        /// </summary>
        public Type ItemType { get; protected set; }

        /// <summary>
        ///     Public constructor for AbstractGenerator
        /// </summary>
        static AbstractGenerator()
        {
            Random = new Random();
        }

        /// <summary>
        ///     Abstract method Generate
        /// </summary>
        public abstract object Generate();
    }
}
