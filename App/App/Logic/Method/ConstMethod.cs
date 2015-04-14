using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Logic.Method.Abstract;

namespace App.Logic.Method
{
    public class ConstMethod : OneOperandMethod
    {
        public ConstMethod(double val) : base(val) { }
        public override double Process()
        {
            return this.Operand;
        }
    }
}
