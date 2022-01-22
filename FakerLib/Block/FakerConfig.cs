using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FakerLib.Api;
using Generators.Model;

namespace FakerLib.Block
{
    /// <summary>
    ///     FakerConfig class
    /// </summary>
    public class FakerConfig : IFakerConfig
    {
        /// <summary>
        ///     Dictionary with custom congig for generators
        /// </summary>
        public Dictionary<MemberInfo, AbstractGenerator> CustomGenerators { get; private set; }

        /// <summary>
        ///     Public constructor for FakerConfig
        /// </summary>
        public FakerConfig()
        {
            CustomGenerators = new Dictionary<MemberInfo, AbstractGenerator>();
        }

        /// <summary>
        ///     Add new config for Faker
        /// </summary>
        /// 
        /// <typeparam name="DTObjectType">DTO type</typeparam>
        /// <typeparam name="MemberType">Member base type</typeparam>
        /// <typeparam name="GeneratorType">Generator type</typeparam>
        ///
        /// <param name="expression">Config expression</param>
        public void Add<DTObjectType, MemberType, GeneratorType>(Expression<Func<DTObjectType, MemberType>> expression)
            where DTObjectType : class
            where GeneratorType : AbstractGenerator
        {
            // Get class field or property
            Expression eBody = expression.Body;
            if (eBody.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Invalid config expression!");
            }

            // Get generator with current type
            AbstractGenerator generator = (AbstractGenerator)Activator.CreateInstance(typeof(GeneratorType));
            if (generator.ItemType != typeof(MemberType))
            {
                throw new ArgumentException("Invalid generator type!");
            }

            // Add new config expression in dictionary
            CustomGenerators.Add(((MemberExpression)eBody).Member, generator);
        }
    }
}
