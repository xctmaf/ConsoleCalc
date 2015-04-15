using App.Logic;
using App.Logic.Method;
using App.Logic.Method.Abstract;
using App.Logic.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator(new StandartInputParser());
            calculator.AddMethod(Functor.Create<PowMethod>("^", MethodPriority.High));

            for (; ; )
            {
                Console.Write("> ");
                var expression = Console.ReadLine();
                if ("q" == expression) break;
                string result = String.Empty;
                try
                {
                    result = calculator.Calculate(expression).ToString();
                }
                catch (ArgumentNullException ex)
                {
                    result = ex.Message;
                }
                catch (ArgumentException ex)
                {
                    result = ex.Message;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
                Console.WriteLine("> {0}", result);
            }
        }
    }
}
