using App.Logic.Method.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Logic.Method
{
    public class DivMethod : TwoOperandMethod
    {
        public DivMethod(IMethod lValue, IMethod rValue) : base(lValue, rValue) { }


        public override double Process()
        {
            var rValueResult = RValue.Process();

            if (rValueResult == 0)
                throw new DivideByZeroException("Деление на ноль запрещено");

            return LValue.Process() / rValueResult;
        }
    }
}
