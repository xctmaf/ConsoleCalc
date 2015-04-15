using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Logic.Method.Abstract;
using App.Logic.Method;

namespace App.Logic.Parser.Abstract
{
    public interface IInputParser
    {
        IMethod Parse(string input);

        FunctorFactory Functors { get; set; }
    }
}
