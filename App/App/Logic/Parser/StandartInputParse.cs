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
        private readonly string Braces = "()";
        private readonly string DecPoint = ".,";
        public override IMethod Parse(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) 
                throw new ArgumentNullException("Empty input");

            return _Parse(ref input, MethodPriority.Low); 
        }

        private IMethod _Parse(ref string input, MethodPriority priority)
        {
            if (priority == MethodPriority.Top)
            {
                var currentElem = PopCurrentElement(ref input);
                if (currentElem == "(")
                {
                    var subInput = GetInputInBraces(ref input);
                    return Parse(subInput);
                }
                double num;
                if (double.TryParse(currentElem, out num))
                    return new ConstMethod(num);
                else
                    throw new ArgumentException(String.Concat(currentElem, " <-- недопустимый синтаксис"));

            }
            else
            {
                throw new NotImplementedException(); //!
            }
        }

        private string GetInputInBraces(ref string input)
        {
            throw new NotImplementedException();
        }

        private string PopCurrentElement(ref string input)
        {
            if (input.Length == 0)
                return string.Empty;
            
            StringBuilder currentElement = new StringBuilder();
            var index = 0;
          
            while ( index < input.Length && Char.IsWhiteSpace(input[index])) 
                ++index;
           
            if (IsBracesOrOperation(input[index]))
            {
                currentElement.Append(input[index]);
            }
            else 
                if (Char.IsDigit(input[index]))
                {
                    currentElement = GetDigit(input, index);
      
                }
                else
                {
                    if (DecPoint.IndexOf(input[index]) != -1)
                        throw new ArgumentException("Разделитель числа стоит не на месте");
                    throw new ArgumentException(string.Concat("Непонятный ввод:", input[index]));
                }
            return currentElement.ToString();

        }

        private StringBuilder GetDigit(string input, int index)
        {
            StringBuilder currentElement = new StringBuilder();
            while (Char.IsDigit(input[index]) || (DecPoint.IndexOf(input[index]) != -1))
            {
                currentElement.Append(input[index]);
                index++;
                if (index >= input.Length) break;
            }
            return currentElement;
        }

        private bool IsBracesOrOperation(char it)
        {
            return Braces.IndexOf(it) != -1; //! надо еще проверить что мы не знак операции, не только скобочка
        }


     
    }
}
