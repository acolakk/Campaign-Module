using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;

namespace Campaign_Module_Test
{
    
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var input = "create_product P1 100 1000";
            var expectedOutput = "Product created; code P1, price 100, stock 1000";
            // Act

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        [TestCase("45.5", "45", 90.5)]
        public void TestRunMethod(string a, string b, double expected)
        {
            var sut = new Program(new List<string> { a, b });

            var result = sut.Run();

            Assert.That(result, Is.EqualTo(expected).Within(0.001));
        }


    }
}
