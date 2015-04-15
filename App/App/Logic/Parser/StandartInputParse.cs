using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App.Logic.Parser.Abstract;
using App.Logic.Method.Abstract;
using App.Logic.Method;
using System.Text.RegularExpressions;

namespace App.Logic.Parser
{
    public class StandartInputParser : InputParser
    {
        private readonly string Braces = "()";
        private readonly string DecPoint = ".,";


        


        public override IMethod Parse(string input)
        {
            input = input.Replace('.', ',').Replace(',', System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0]);
            input = Regex.Replace(input, @"\s", string.Empty);
            if (String.IsNullOrWhiteSpace(input)) 
                throw new ArgumentNullException("Empty input");

            return _Parse(ref input, MethodPriority.Lowest); 
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
                var nextPriority = priority + 1;
                var result = _Parse(ref input, nextPriority);
                var currentElem = GetCurrentElement(input);
                var method = Functors.GetFunctor(currentElem);
                while (method != null && method.Priority == priority)
                {
                    currentElem = PopCurrentElement(ref input);
                    var subMethod = _Parse(ref input, nextPriority);
                    result = method.Process(result, subMethod);
                    method = Functors.GetFunctor(GetCurrentElement(input));
                }
                return result;              
            }
        }

    

        private string GetInputInBraces(ref string input)
        {
            
            if (String.IsNullOrEmpty(input)) 
                throw new ArgumentException("Неверный синтаксис скобочного выражения");
           
            var bracesCount = 1;
            var curPosition = -1;

            foreach (char c in input)
            {
                if (c == ')') 
                    bracesCount--;
                if (c == '(') 
                    bracesCount++;
                ++curPosition;
                if (bracesCount == 0) 
                    break;
                
            }
            if (bracesCount != 0)
                throw new ArgumentException("Неверный синтаксис скобочного выражения");
            //if (input[curPosition] != ')' || curPosition == input.Length)
            //    throw new ArgumentException("Неверный синтаксис скобочного выражения");
            
            var bracesBody = input.Substring(0, curPosition);
            input = input.Remove(0, curPosition + 1);

            return bracesBody;
        }
        private string PopCurrentElement(ref string input)
        {
            string result = GetCurrentElement(input);
            input = input.Remove(0,result.Length);
            return result;
        }

        private string GetCurrentElement(string input)
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
            return Braces.IndexOf(it) != -1 || Functors.Any(x => x.Record.Equals(it.ToString()));
        }




        protected override FunctorFactory GetAvailableFunctors()
        {
            return new FunctorFactory()
                            .Add(Functor.Create<AddMethod>("+", MethodPriority.Lowest))
                            .Add(Functor.Create<SubMethod>("-", MethodPriority.Lowest))
                            .Add(Functor.Create<MulMethod>("*", MethodPriority.MidNormal))
                            .Add(Functor.Create<DivMethod>("/", MethodPriority.MidNormal));
        }

    }
}
