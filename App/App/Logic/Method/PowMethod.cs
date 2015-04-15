using App.Logic.Method.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Logic.Method
{
    public class PowMethod: TwoOperandMethod
    {
        public PowMethod(IMethod lValue, IMethod rValue) : base(lValue, rValue){ }


        public override double Process()
        {
            double lValueResult = LValue.Process();
            double rValueResult = RValue.Process();
            if (rValueResult == 0 && lValueResult == 0)
                throw new ArgumentException("Невозможно возвести 0 в 0 степень");
            return Math.Pow(lValueResult,rValueResult);
        }       
    }
}
