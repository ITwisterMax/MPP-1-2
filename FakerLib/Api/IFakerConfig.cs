using System;
using System.Linq.Expressions;
using Generators.Model;

namespace FakerLib.Api
{
    /// <summary>
    ///     Interface for FakerConfig class
    /// </summary>
    interface IFakerConfig
    {
        /// <summary>
        ///     Add new config for Faker
        /// </summary>
        /// 
        /// <typeparam name="DTObjectType">DTO type</typeparam>
        /// <typeparam name="MemberType">Member base type</typeparam>
        /// <typeparam name="GeneratorType">Generator type</typeparam>
        ///
        /// <param name="expression">Config expression</param>
        void Add<DTObjectType, MemberType, GeneratorType>(Expression<Func<DTObjectType, MemberType>> expression)
            where DTObjectType : class
            where GeneratorType : AbstractGenerator;
    }
}
