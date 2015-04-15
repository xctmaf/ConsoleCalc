using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Logic.Method.Abstract
{
    public abstract class TwoOperandMethod : IMethod
    {
        protected IMethod LValue { get; set; }
        protected IMethod RValue { get; set; }


        protected TwoOperandMethod(IMethod lValue, IMethod rValue)
        {
            LValue = lValue;
            RValue = rValue;
        }
        abstract public double Process();
    }
}
