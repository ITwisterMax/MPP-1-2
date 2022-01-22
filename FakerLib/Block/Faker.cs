using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakerLib.Api;
using FakerLib.Helper;
using Generators.Model;
using Generators.Plugins.ValueTypes;
using Generators.Plugins.ReferenceTypes;

namespace FakerLib.Block
{
    /// <summary>
    ///     Faker class
    /// </summary>
    public class Faker : IFaker
    {
        /// <summary>
        ///     List with all allowed types
        /// </summary>
        public List<Type> AllTypes { get; }

        /// <summary>
        ///     Dictionary with all base generators
        /// </summary>
        public Dictionary<Type, AbstractGenerator> Generators { get; private set; }

        /// <summary>
        ///     Dictionary with all generic generators
        /// </summary>
        public Dictionary<Type, AbstractGeneratorCollection> GeneratorsCollection { get; private set; }

        /// <summary>
        ///     Dictionary with all custom generators
        /// </summary>
        public Dictionary<MemberInfo, AbstractGenerator> CustomGenerators { get; private set; }

        /// <summary>
        ///     Recursive padding
        /// </summary>
        private readonly Stack<Type> NestedTypes;

        /// <summary>
        ///     Public constructor for Faker
        /// </summary>
        /// 
        /// <param name="fakerConfig">Object with faker config</param>
        public Faker(FakerConfig fakerConfig)
        {
            // Get all allowed types
            AllTypes = typeof(Assembly).Assembly.GetExportedTypes().ToList();

            // Get generator's instances (from plugins)
            var customGenerator = Loader.LoadPlugin(@"D:\Work\СПП\Часть 1\Laba2\Plugins\CustomTypes\CityGenerator\bin\Debug\CityGenerator.dll", typeof(string));
            var dateTimeGenerator = Loader.LoadPlugin(@"D:\Work\СПП\Часть 1\Laba2\Plugins\SystemTypes\DateTimeGenerator\bin\Debug\DateTimeGenerator.dll", typeof(DateTime));

            // Set dictionary with all base generators
            Generators = new Dictionary<Type, AbstractGenerator>
            {
                { typeof(bool), new BooleanGenerator() },
                { typeof(char), new CharGenerator() },
                { typeof(byte), new ByteGenerator() },
                { typeof(sbyte), new SByteGenerator() },
                { typeof(short), new ShortGenerator() },
                { typeof(ushort), new UShortGenerator() },
                { typeof(float), new FloatGenerator() },
                { typeof(double), new DoubleGenerator() },
                { typeof(decimal), new DecimalGenerator() },
                { typeof(int), new IntGenerator() },
                { typeof(uint), new UIntGenerator() },
                { typeof(long), new LongGenerator() },
                { typeof(ulong), new ULongGenerator() },
                { typeof(string), new StringGenerator() },
                { customGenerator.GetType(), customGenerator },
                { typeof(DateTime), dateTimeGenerator }
            };

            // Set dictionary with all generic generators
            GeneratorsCollection = new Dictionary<Type, AbstractGeneratorCollection>
            {
                { typeof(List<>), new ListGenerator(Generators) }
            };

            // Set dictionary with all custom generators
            CustomGenerators = fakerConfig.CustomGenerators;

            // Initialize stack for recursive padding
            NestedTypes = new Stack<Type>();
        }

        /// <summary>
        ///     Create new DTO object
        /// </summary>
        /// 
        /// <typeparam name="T">Generator type</typeparam>
        /// 
        /// <returns>T</returns>
        public T Create<T>() where T : class
        {
            NestedTypes.Push(typeof(T));

            return Initialize(typeof(T)) as T;
        }

        /// <summary>
        ///     Initialize class fields and properties
        /// </summary>
        /// 
        /// <param name="itemType">Generator type</param>
        /// 
        /// <returns>object</returns>
        public object Initialize(Type itemType)
        {
            // Get DTO instance and initialize it
            var result = GetInstance(itemType);

            if (result != null)
            {
                SetFields(ref result);
                SetProperties(ref result);
            }

            return result;
        }

        /// <summary>
        ///     Create DTO instance
        /// </summary>
        /// 
        /// <param name="itemType">Generator type</param>
        /// 
        /// <returns>object</returns>
        private object GetInstance(Type itemType)
        {
            // Get all parameters from constructors
            var constructor = itemType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.FirstOrDefault();
            var parameters = constructor.GetParameters();

            var generatedParams = new List<dynamic>();
            foreach (var parameter in parameters)
            {
                Type objectType = parameter.ParameterType;

                // Select necessary branch and generate value
                if (FindCustomGeneratorByParams(parameter, out AbstractGenerator customGenerator))
                {
                    generatedParams.Add(customGenerator.Generate());
                }
                else if (FindGenerator(objectType, out AbstractGenerator generator))
                {
                    generatedParams.Add(generator.Generate());
                }
                else if (FindGeneratorCollection(objectType, out AbstractGeneratorCollection generatorCollection))
                {
                    Type generatorCollectionType = parameter.ParameterType.GetGenericArguments()[0];
                    generatedParams.Add(generatorCollection.Generate(generatorCollectionType));
                }
                else if (IsNotExistingCustomClass(objectType))
                {
                    NestedTypes.Push(parameter.ParameterType);

                    generatedParams.Add(Initialize(parameter.ParameterType));

                    NestedTypes.Pop();
                }
            }

            // Create DTO instance
            try
            {
                return constructor.Invoke(generatedParams.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        /// <summary>
        ///     Set class properties
        /// </summary>
        /// 
        /// <param name="instance">DTO instance</param>
        private void SetProperties(ref dynamic instance)
        {
            // Get list with properties
            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                Type typeObj = property.PropertyType;

                // Skip private and readonly properties
                if (!(property?.CanWrite ?? false) || (property?.SetMethod.IsPrivate ?? false))
                {
                    continue;
                }

                // Select necessary branch and set value
                if (FindCustomGenerator(property, out AbstractGenerator customGenerator))
                {
                    property.SetValue(instance, customGenerator.Generate());
                }
                else if (FindGenerator(typeObj, out AbstractGenerator generator))
                {
                    property.SetValue(instance, generator.Generate());
                }
                else if (FindGeneratorCollection(typeObj, out AbstractGeneratorCollection generatorCollection))
                {
                    Type generatorCollectionType = property.PropertyType.GetGenericArguments()[0];
                    property.SetValue(instance, generatorCollection.Generate(generatorCollectionType));
                }
                else if (IsNotExistingCustomClass(typeObj))
                {
                    NestedTypes.Push(property.PropertyType);

                    property.SetValue(instance, Initialize(property.PropertyType));

                    NestedTypes.Pop();
                }
            }
        }

        /// <summary>
        ///     Set class fields    
        /// </summary>
        /// 
        /// <param name="instance">DTO instance</param>
        private void SetFields(ref dynamic instance)
        {
            // Get list with fields
            var fields = instance.GetType().GetFields();
            foreach (var fieldInfo in fields)
            {
                Type objectType = fieldInfo.FieldType;

                // Skip private fields
                if (!fieldInfo.IsPublic)
                {
                    continue;
                }

                // Select necessary branch and set value
                if (FindCustomGenerator(fieldInfo, out AbstractGenerator customGenerator))
                {
                    fieldInfo.SetValue(instance, customGenerator.Generate());
                }
                else if (FindGenerator(objectType, out AbstractGenerator generator))
                {
                    fieldInfo.SetValue(instance, generator.Generate());
                }
                else if (FindGeneratorCollection(objectType, out AbstractGeneratorCollection generatorCollection))
                {
                    Type generatorCollectionType = fieldInfo.FieldType.GetGenericArguments()[0];
                    fieldInfo.SetValue(instance, generatorCollection.Generate(generatorCollectionType));
                }
                else if (IsNotExistingCustomClass(objectType))
                {
                    NestedTypes.Push(fieldInfo.FieldType);

                    fieldInfo.SetValue(instance, Initialize(fieldInfo.FieldType));
                    
                    NestedTypes.Pop();
                }
            }
        }

        /// <summary>
        ///     Find custom generator in dictionary by parameters
        /// </summary>
        /// <param name="parametersInfo">Parameters information</param>
        /// <param name="generator">Generator</param>
        /// <returns></returns>
        private bool FindCustomGeneratorByParams(ParameterInfo parametersInfo, out AbstractGenerator generator)
        {
            generator = null;

            foreach (KeyValuePair<MemberInfo, AbstractGenerator> item in CustomGenerators)
            {
                string convertedType;

                // Choose kind of parameter
                var member = item.Key;
                switch (member.MemberType)
                {
                    case MemberTypes.Event:
                        convertedType = ((EventInfo)member).EventHandlerType.ToString();
                        break;
                    case MemberTypes.Field:
                        convertedType = ((FieldInfo)member).FieldType.ToString();
                        break;
                    case MemberTypes.Method:
                        convertedType = ((MethodInfo)member).MemberType.ToString();
                        break;
                    case MemberTypes.Property:
                        convertedType = ((PropertyInfo)member).PropertyType.ToString();
                        break;
                    default:
                        return false;
                }

                // Check type and set generator
                if ((item.Key.Name.ToLower() == parametersInfo.Name.ToLower()) &&
                    parametersInfo.ParameterType.ToString() == convertedType &&
                    parametersInfo.Member.DeclaringType.ToString() == item.Key.DeclaringType.ToString())
                {
                    generator = item.Value;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Find base generator in dictionary
        /// </summary>
        /// 
        /// <param name="itemType">Generator type</param>
        /// <param name="generator">Generator</param>
        /// 
        /// <returns>bool</returns>
        private bool FindGenerator(Type itemType, out AbstractGenerator generator)
        {
            if (Generators.TryGetValue(itemType, out generator))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Find generic generator in dictionary
        /// </summary>
        /// 
        /// <param name="itemType"></param>
        /// <param name="generatorCollection">Generator collection</param>
        /// 
        /// <returns>bool</returns>
        private bool FindGeneratorCollection(Type itemType, out AbstractGeneratorCollection generatorCollection)
        {
            generatorCollection = null;

            if (itemType.IsGenericType)
            {
                if (GeneratorsCollection.TryGetValue(itemType.GetGenericTypeDefinition(), out generatorCollection))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Find custom generator in dictionary
        /// </summary>
        /// 
        /// <param name="memberInfo">Information about expression</param>
        /// <param name="generator">Generator</param>
        /// 
        /// <returns>bool</returns>
        private bool FindCustomGenerator(MemberInfo memberInfo, out AbstractGenerator generator)
        {
            generator = null;

            foreach (KeyValuePair<MemberInfo, AbstractGenerator> item in CustomGenerators)
            {
                if ((item.Key.Name.ToLower() == memberInfo.Name.ToLower()) && memberInfo.Equals(item.Key))
                {
                    generator = item.Value;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Check case when class doesn't exist    
        /// </summary>
        /// <param name="itemType">Generator type</param>
        /// 
        /// <returns>bool</returns>
        private bool IsNotExistingCustomClass(Type itemType)
        {
            if (itemType.IsClass && !itemType.IsArray && !AllTypes.Contains(itemType) && !NestedTypes.Contains(itemType))
            {
                return true;
            }

            return false;
        }
    }
}
