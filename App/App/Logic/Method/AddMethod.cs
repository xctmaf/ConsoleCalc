﻿using App.Logic.Method.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Logic.Method
{
    public class AddMethod : TwoOperandMethod
    {
        public AddMethod(IMethod lValue, IMethod rValue) : base(lValue, rValue){ }


        public override double Process()
        {
            return LValue.Process() + RValue.Process();
        }       
    }
}
