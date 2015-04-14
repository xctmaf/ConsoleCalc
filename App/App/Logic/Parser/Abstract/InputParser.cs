using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Logic.Method.Abstract;

namespace App.Logic.Parser.Abstract
{
    abstract public class InputParser : IInputParser
    {
        abstract public IMethod Parse(string input);
    }
}
