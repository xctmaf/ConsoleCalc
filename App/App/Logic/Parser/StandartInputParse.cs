using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App.Logic.Parser.Abstract;
using App.Logic.Method.Abstract;
using App.Logic.Method;

namespace App.Logic.Parser
{
    public class StandartInputParse : InputParser
    {
        public override IMethod Parse(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) 
                throw new ArgumentNullException("Empty input");

            


            return new ConstMethod(74);//! 
        }
    }
}
