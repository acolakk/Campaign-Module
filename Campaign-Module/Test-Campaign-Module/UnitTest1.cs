using NUnit.Framework;
using System;

namespace Test_Campaign_Module
{
    public class Tests
    {



        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {

            //Act
            string[] args = Environment.GetCommandLineArgs();

            //Assert
            Assert.AreEqual("Your", args[0]);
            Assert.AreEqual("Fake", args[0]);
            Assert.AreEqual("Args", args[0]);
            Assert.Pass();
        }
    }
}