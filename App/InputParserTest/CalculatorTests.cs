using App.Logic;
using App.Logic.Method;
using App.Logic.Method.Abstract;
using App.Logic.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputParserTest
{

    [TestClass]
    public class CalculatorTests
    {

        private class FakeTwoOperandMethodAdd : TwoOperandMethod
        {
            public FakeTwoOperandMethodAdd(IMethod lValue, IMethod rValue) : base(lValue, rValue) { }
            public override double Process()
            {
                return RValue.Process() + LValue.Process();
            }
        }

        private class FakeTwoOperandMethodSub : TwoOperandMethod
        {
            public FakeTwoOperandMethodSub(IMethod lValue, IMethod rValue) : base(lValue, rValue) { }
            public override double Process()
            {
                return RValue.Process() - LValue.Process();
            }
        }

        private class FakeTwoOperandMethodMul : TwoOperandMethod
        {
            public FakeTwoOperandMethodMul(IMethod lValue, IMethod rValue) : base(lValue, rValue) { }
            public override double Process()
            {
                return RValue.Process() * LValue.Process();
            }
        }

        [TestMethod]
        public void Calculator_AddFunctors_WhenAddNewFunctors_ExpectIncFunctorCount()
        {
            var calculator = new Calculator(new StandartInputParser());

            var oldFunctorsCount = calculator.Functors.Count();
            calculator.AddMethod(Functor.Create<FakeTwoOperandMethodMul>("**", MethodPriority.Lowest));
            calculator.AddMethod(Functor.Create<FakeTwoOperandMethodAdd>("++", MethodPriority.Lowest));
            calculator.AddMethod(Functor.Create<FakeTwoOperandMethodSub>("--", MethodPriority.Lowest));

            Assert.IsTrue(calculator.Functors.Count() - oldFunctorsCount == 3);
        }

        [TestMethod]
        public void Calculator_AddFunctors_WhenAddSameFunctor_ExpectOnlyOneNewFunctor()
        {
            var calculator = new Calculator(new StandartInputParser());

            var oldFunctorsCount = calculator.Functors.Count();
            calculator.AddMethod(Functor.Create<AddMethod>("@", MethodPriority.Lowest));
            calculator.AddMethod(Functor.Create<AddMethod>("@", MethodPriority.Lowest));

            Assert.IsTrue(calculator.Functors.Count() - oldFunctorsCount == 1);
        }

        [TestMethod]
        public void Calculator_AddFunctors_WhenAddSameFunctorWithNewPriority_ExpectUpdatePriority()
        {
            var calculator = new Calculator(new StandartInputParser());

            var oldFunctorsCount = calculator.Functors.Count();
            calculator.AddMethod(Functor.Create<AddMethod>("@", MethodPriority.Lowest));
            calculator.AddMethod(Functor.Create<AddMethod>("@", MethodPriority.MidNormal));
            
            Assert.IsTrue(calculator.Functors.GetFunctor("@").Priority == MethodPriority.MidNormal);
        }

       
        [TestMethod]
        public void Calculator_AddFunctors_WhenAddTwoOperandFunctors_ExpectNewFunctor()
        {
            var calculator = new Calculator(new StandartInputParser());

            var oldFunctorsCount = calculator.Functors.Count();
            calculator.AddMethod(Functor.Create<PowMethod>("^", MethodPriority.Highest));

            Assert.IsTrue(calculator.Functors.Count() - oldFunctorsCount == 1);

            var result = calculator.Calculate("3^2*2");

            Assert.AreEqual(18, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Невозможно возвести 0 в 0 степень")]
        public void Calculator_AddFunctors_WhenAddTwoOperandFunctorAndWrongUse_ExpectException()
        {
            var calculator = new Calculator(new StandartInputParser());

            var oldFunctorsCount = calculator.Functors.Count();
            calculator.AddMethod(Functor.Create<PowMethod>("^", MethodPriority.Highest));

            Assert.IsTrue(calculator.Functors.Count() - oldFunctorsCount == 1);

            var result = calculator.Calculate("0^0");

            Assert.AreEqual(18, result);
        }

       
    }
}
