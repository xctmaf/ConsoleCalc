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
        public void Parser_Parse_WhenParseEmptyString_ExpectException()
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
        public void Parser_Parse_WhenParseWhiteSpacesString_ExpectException()
        {
            IInputParser parser = new StandartInputParse();

            Type curType = GetType();
            MethodInfo curMethodInfo = curType.GetMethod("Parser_Parse_WhenParseWhiteSpacesString_ExpectException");
            Type testPropertyType = typeof(TestPropertyAttribute);
            object[] attributes = curMethodInfo.GetCustomAttributes(testPropertyType, false);
            foreach (var attrib in attributes)
            {
                var operation = parser.Parse(((TestPropertyAttribute)attrib).Value);

                Assert.Fail();
            }
        }



        [TestMethod]
        public void Parser_PopCurrentElement_WhenPopBraces_ExpectBraces()
        {

            IInputParser target = new StandartInputParse();
            PrivateObject obj = new PrivateObject(target);
            
            var retVal = obj.Invoke("PopCurrentElement",new object[]{"(2+3)"});
            
            Assert.AreEqual("(",retVal);
        }

        [TestMethod]
        public void Parser_PopCurrentElement_WhenPopDigit_ExpectDigit()
        {

            IInputParser target = new StandartInputParse();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("PopCurrentElement", new object[] { "46+2314-2+(32-2)" });

            Assert.AreEqual("46", retVal);
        }

        [TestMethod]
        public void Parser_PopCurrentElement_WhenPopDigitWithPoint_ExpectDigit()
        {

            IInputParser target = new StandartInputParse();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("PopCurrentElement", new object[] { "46.23+2314-2+(32-2)" });

            Assert.AreEqual("46.23", retVal);
        }
        [TestMethod]
        public void Parser_PopCurrentElement_WhenPopDigitWithComma_ExpectDigit()
        {

            IInputParser target = new StandartInputParse();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("PopCurrentElement", new object[] { "46,23+2314-2+(32-2)" });

            Assert.AreEqual("46,23", retVal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Непонятный ввод:$")]
        public void Parser_PopCurrentElement_WhenWrongInput_ExpectException()
        {

            IInputParser target = new StandartInputParse();
            PrivateObject obj = new PrivateObject(target);

            var retVal  = obj.Invoke("PopCurrentElement", new object[] { "$" });

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Разделитель числа стоит не на месте")]
        public void Parser_PopCurrentElement_WhenWrongDecPoint_ExpectException()
        {

            IInputParser target = new StandartInputParse();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("PopCurrentElement", new object[] { ".23" });

            Assert.Fail();
        }
    }
}
