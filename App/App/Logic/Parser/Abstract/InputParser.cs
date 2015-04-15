using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Logic.Method.Abstract;
using App.Logic.Method;

namespace App.Logic.Parser.Abstract
{
    
    abstract public class InputParser : IInputParser
    {
        
        abstract public IMethod Parse(string input);
        protected abstract FunctorFactory GetAvailibleFunctors();


        protected InputParser()
        {
            Functors = GetAvailibleFunctors();
        }


        public FunctorFactory Functors { get; set; }
    }
}
