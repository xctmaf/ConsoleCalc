using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Logic.Method;
using App.Logic.Method.Abstract;
using System.Collections;
using System.Collections.Generic;

namespace InputParserTest
{
    [TestClass]
    public class FunctorsTests
    {
        [TestMethod]
        public void FunctorFactory_Clear_WhenCalling_ExceptNormalBehaviour_CuzTestCover()
        {
            FunctorFactory funcFactory = new FunctorFactory();
            funcFactory.Add(Functor.Create<AddMethod>("+",MethodPriority.Lowest));

            funcFactory.Clear();
            
            Assert.AreEqual(0, funcFactory.Count());
        }

        [TestMethod]
        public void FunctorFactory_GetEnumerator_WhenCalling_ExceptNormalBehaviour_CuzTestCover()
        {
            FunctorFactory funcFactory = new FunctorFactory();
            funcFactory.Add(Functor.Create<AddMethod>("+", MethodPriority.Lowest));

            var result = funcFactory.GetEnumerator();
    
            var count = 0;
            while(result.MoveNext())
                count++;

            Assert.AreEqual(funcFactory.Count(), count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Ошибка определения доступных операций")]
        public void FunctorFactory_Add_WhenAddNullFunctor_ExceptException()
        {
            FunctorFactory funcFactory = new FunctorFactory();
            funcFactory.Add(null);

            Assert.Fail();
        }
    }
}
