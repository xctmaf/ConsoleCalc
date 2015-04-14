using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Logic.Method.Abstract
{
    abstract public class OneOperandMethod : IMethod
    {
        protected double Operand { get; set; }

        protected OneOperandMethod(double val)
        {
            this.Operand = val;
        }

        abstract public double Process();
    }
}
