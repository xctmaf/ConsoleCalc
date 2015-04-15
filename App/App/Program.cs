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

            while (true)
            {
                Console.Write("input@calc  $  ");
                var input = Console.ReadLine();
                if (input == "quit")
                    break;

                try
                {
                    Console.WriteLine("answer@calc $  {0}", calculator.Calculate(input).ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error!@calc $  {0}", ex.Message);
                }

            }
           
        }
    }
}
