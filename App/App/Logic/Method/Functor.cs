using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Logic.Method.Abstract;

namespace App.Logic.Method
{
    public sealed class Functor
    {
        private Type _functorType;
        public string Record { get; private set; }
        public MethodPriority Priority { get; private set; }

        private Functor(Type functorType, string record, MethodPriority priority)
        {
            _functorType = functorType;
            Record = record;
            Priority = priority;
        }

        public static Functor Create<T>(string record, MethodPriority priority) where T : TwoOperandMethod
        {
            return new Functor(typeof(T), record, priority);
        }

        public IMethod Process(IMethod lValue, IMethod rValue)
        {
            return (IMethod)(_functorType.GetConstructor(new Type[] { lValue.GetType(), rValue.GetType() })
                                         .Invoke(new object[] { lValue, rValue }));
        }
    }
}
