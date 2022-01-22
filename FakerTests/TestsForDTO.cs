using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FakerLib.Block;
using FakerLib.Test;
using Generators.Plugins.CustomTypes;

namespace FakerTests
{
    [TestClass]
    public class TestsForDTO
    {
        private A ObjectDTO;

        [TestInitialize]
        public void Setup()
        {
            var config = new FakerConfig();
            config.Add<B, string, CityGenerator>(elem => elem.TestCity);

            var faker = new Faker(config);

            ObjectDTO = faker.Create<A>();
        }

        [TestMethod]
        public void TestClassBType()
        {
            Assert.IsNotNull(ObjectDTO.ClassB);
            Assert.IsInstanceOfType(ObjectDTO.ClassB, typeof(B));

            Assert.IsNotNull(ObjectDTO.ClassB.TestBoolean);
            Assert.IsInstanceOfType(ObjectDTO.ClassB.TestBoolean, typeof(bool));

            Assert.IsNotNull(ObjectDTO.ClassB.TestChar);
            Assert.IsInstanceOfType(ObjectDTO.ClassB.TestChar, typeof(char));

            Assert.IsNotNull(ObjectDTO.ClassB.TestCity);
            Assert.IsInstanceOfType(ObjectDTO.ClassB.TestCity, typeof(string));
        }

        [TestMethod]
        public void TestClassCType()
        {
            Assert.IsNotNull(ObjectDTO.ClassC);
            Assert.IsInstanceOfType(ObjectDTO.ClassC, typeof(C));

            Assert.IsNull(ObjectDTO.ClassC.ClassA);

            Assert.IsNotNull(ObjectDTO.ClassC.TestFloat);
            Assert.IsInstanceOfType(ObjectDTO.ClassC.TestFloat, typeof(float));

            Assert.IsNotNull(ObjectDTO.ClassC.TestInt);
            Assert.IsInstanceOfType(ObjectDTO.ClassC.TestInt, typeof(int));
        }

        [TestMethod]
        public void TestListType()
        {
            Assert.IsNotNull(ObjectDTO.TestList);
            Assert.IsInstanceOfType(ObjectDTO.TestList, typeof(List<float>));
        }

        [TestMethod]
        public void TestStringType()
        {
            Assert.IsNotNull(ObjectDTO.TestString);
            Assert.IsInstanceOfType(ObjectDTO.TestString, typeof(string));
            Assert.AreEqual(ObjectDTO.TestString.Length, 62);
        }

        [TestMethod]
        public void TestDateTimeType()
        {
            Assert.IsNotNull(ObjectDTO.TestDateTime);
            Assert.IsInstanceOfType(ObjectDTO.TestDateTime, typeof(DateTime));
        }
    }
}
