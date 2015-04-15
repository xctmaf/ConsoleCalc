using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Logic.Parser.Abstract;
using App.Logic.Method;
using App.Logic.Method.Abstract;


namespace App.Logic
{
    public class Calculator
    {
        private readonly IInputParser _parser;
        public FunctorFactory Functors { get { return _parser.Functors; } }

        public Calculator(IInputParser parser)
        {
            _parser = parser;
        }

        public void AddMethod(Functor newMethod)
        {
            _parser.Functors.Add(newMethod);
        }

        public double Calculate(string input)
        {
            var method = _parser.Parse(input);
            return method.Process();
        }
    }
}
