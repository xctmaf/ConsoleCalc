using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Logic.Parser.Abstract;
using App.Logic.Parser;
using App.Logic.Method.Abstract;
using App.Logic.Method;
using System.Reflection;


namespace InputParserTest
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Empty input")]
        public void Parser_WhenParseEmptyString_ExpectException()
        {
            const string input = "";
            IInputParser parser = new StandartInputParse();

            var operation = parser.Parse(input);

            Assert.Fail();
        }


        [TestMethod]
        [TestProperty("input1", " ")]
        [TestProperty("input2", "    ")]
        [TestProperty("input3", "    ")]
        [TestProperty("input4", "     ")]
        [TestProperty("input5", "\n")]
        [TestProperty("input6", "\r\n")]
        [TestProperty("input7", "\r")]
        [ExpectedException(typeof(ArgumentNullException), "Empty input")]
        public void Parser_WhenParseWhiteSpacesString_ExpectException()
        {
            IInputParser parser = new StandartInputParse();

            Type curType = GetType();
            MethodInfo curMethodInfo = curType.GetMethod("Parser_WhenParseWhiteSpacesString_ExpectException");
            Type testPropertyType = typeof(TestPropertyAttribute);
            object[] attributes = curMethodInfo.GetCustomAttributes(testPropertyType, false);
            foreach (var attrib in attributes)
            {
                var operation = parser.Parse(((TestPropertyAttribute)attrib).Value);

                Assert.Fail();
            }
        } 
    }
}
