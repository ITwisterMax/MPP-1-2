using System;

namespace FakerLib.Api
{
    /// <summary>
    ///     Interface for Faker class
    /// </summary>
    public interface IFaker
    {
        /// <summary>
        ///     Faker create method
        /// </summary>
        /// 
        /// <typeparam name="T">Generator type</typeparam>
        /// 
        /// <returns>object</returns>
        T Create<T>() where T : class;

        /// <summary>
        ///     Faker initialize method
        /// </summary>
        /// 
        /// <param name="itemType">Generator type</param>
        /// 
        /// <returns>object</returns>
        object Initialize(Type itemType);
    }
}
