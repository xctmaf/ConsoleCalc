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
            IInputParser parser = new StandartInputParser();

            var operation = parser.Parse(input);

            Assert.Fail();
        }


        [TestMethod]
        public void Parser_Parse_WhenInputDigit_ExpectDigit()
        {
            const string input = "45";
            IInputParser parser = new StandartInputParser();

            var operation = parser.Parse(input);

            Assert.AreEqual(45,((IMethod)operation).Process());
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
            IInputParser parser = new StandartInputParser();

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

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);
            
            var retVal = obj.Invoke("PopCurrentElement",new object[]{"(2+3)"});
            
            Assert.AreEqual("(",retVal);
        }

        [TestMethod]
        public void Parser_PopCurrentElement_WhenPopDigit_ExpectDigit()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("PopCurrentElement", new object[] { "46+2314-2+(32-2)" });

            Assert.AreEqual("46", retVal);
        }

        [TestMethod]
        public void Parser_PopCurrentElement_WhenPopDigitWithPoint_ExpectDigit()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("PopCurrentElement", new object[] { "46.23+2314-2+(32-2)" });

            Assert.AreEqual("46.23", retVal);
        }
        [TestMethod]
        public void Parser_PopCurrentElement_WhenPopDigitWithComma_ExpectDigit()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("PopCurrentElement", new object[] { "46,23+2314-2+(32-2)" });

            Assert.AreEqual("46,23", retVal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Непонятный ввод:$")]
        public void Parser_PopCurrentElement_WhenWrongInput_ExpectException()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal  = obj.Invoke("PopCurrentElement", new object[] { "$" });

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Разделитель числа стоит не на месте")]
        public void Parser_PopCurrentElement_WhenWrongDecPoint_ExpectException()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("PopCurrentElement", new object[] { ".23" });

            Assert.Fail();
        }



        [TestMethod]
        public void Parser_GetInputInBraces_WhenNormalInput_ExpectRightAnswer()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("GetInputInBraces", new object[] { "74+22-1)" });

            Assert.AreEqual("74+22-1", retVal);
        }

        [TestMethod]
        public void Parser_GetInputInBraces_WhenNormalInputWithSubBraces_ExpectRightAnswer()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("GetInputInBraces", new object[] { "74+22*(34-2+(22-1)*5/(7-1+(4-2)))-1)" });

            Assert.AreEqual("74+22*(34-2+(22-1)*5/(7-1+(4-2)))-1", retVal);
        }

        [TestMethod]
        public void Parser_GetInputInBraces_WhenNormalInputWithSubBracesAndSymbolsAfterClosingBraces_ExpectRightAnswer()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("GetInputInBraces", new object[] { "79+22*(34-2+(22-1)*5/(7-1+(4-2)))-1)-84+22-2+(2-1)" });

            Assert.AreEqual("79+22*(34-2+(22-1)*5/(7-1+(4-2)))-1", retVal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Неверный синтаксис скобочного выражения")]
        public void Parser_GetInputInBraces_WhenInputEmptyString_ExpectException()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("GetInputInBraces", new object[] { "" });

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Неверный синтаксис скобочного выражения")]
        public void Parser_GetInputInBraces_WhenInputIncorectBraces_ExpectException()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("GetInputInBraces", new object[] { "(" });

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Неверный синтаксис скобочного выражения")]
        public void Parser_GetInputInBraces_WhenInputWithoutClosingBraces_ExpectException()
        {

            IInputParser target = new StandartInputParser();
            PrivateObject obj = new PrivateObject(target);

            var retVal = obj.Invoke("GetInputInBraces", new object[] { "3-2(" });

            Assert.Fail();
        }

        [TestMethod]
        public void Parser_Parse_WhenInputAdd_ExpectRightResult()
        {
            const string input = "79+1.5";
            const string input2 = "1+2+3+4+5,1";
            IInputParser parser = new StandartInputParser();

            var method = parser.Parse(input);
            Assert.AreEqual(80.5, method.Process());

            var method2 = parser.Parse(input2);
            Assert.AreEqual(15.1, method2.Process());
        }

        [TestMethod]
        public void Parser_Parse_WhenInputSub_ExpectRightResult()
        {
            const string input = "79-1.5";
            const string input2 = "0-2,5-2,5";
            IInputParser parser = new StandartInputParser();

            var method = parser.Parse(input);
            Assert.AreEqual(77.5, method.Process());

            var method2 = parser.Parse(input2);
            Assert.AreEqual(-5, method2.Process());
        }

        [TestMethod]
        public void Parser_Parse_WhenInputMul_ExpectRightResult()
        {
            const string input = "3*2.5";
            const string input2 = "4*1,5";
            IInputParser parser = new StandartInputParser();

            var method = parser.Parse(input);
            Assert.AreEqual(7.5, method.Process());

            var method2 = parser.Parse(input2);
            Assert.AreEqual(6, method2.Process());
        }

        [TestMethod]
        public void Parser_Parse_WhenInputDiv_ExpectRightResult()
        {
            const string input = "7.5/3";
            const string input2 = "6/1,5";
            IInputParser parser = new StandartInputParser();

            var method = parser.Parse(input);
            Assert.AreEqual(2.5, method.Process());

            var method2 = parser.Parse(input2);
            Assert.AreEqual(4, method2.Process());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Деление на ноль запрещено")]
        public void Parser_Parse_WhenInputDivWithDivByZero_ExpectException()
        {
            const string input = "7.5/0";
            IInputParser parser = new StandartInputParser();

            var method = parser.Parse(input);
            var result = method.Process();
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Деление на ноль запрещено")]
        public void Parser_Parse_WhenComplexInputDivWithDivByZero_ExpectException()
        {
            const string input = "7.5/(3-3)";
            IInputParser parser = new StandartInputParser();

            var method = parser.Parse(input);
            var result = method.Process();
            Assert.Fail();
        }

        [TestMethod]
        public void Parser_Parse_WhenComplexInput_ExpectRightResult()
        {
            const string input = "2+2*2-(3+3)*3/2";
            const string input2 = "1+(3+(2+2)-3)-1";
            IInputParser parser = new StandartInputParser();

            var method = parser.Parse(input);
            Assert.AreEqual(-3, method.Process());

            var method2 = parser.Parse(input2);
            Assert.AreEqual(4, method2.Process());
        }
    }
}
