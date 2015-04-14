using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Logic.Method.Abstract;

namespace App.Logic.Parser.Abstract
{
    public interface IInputParser
    {
        IMethod Parse(string input);
    }
}
