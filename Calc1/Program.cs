using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInterpreter.Core;

namespace Calc1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("calc> ");
                var text = Console.ReadLine();

                if (!string.IsNullOrEmpty(text))
                {
                    var interpreter = new Interpreter(text);
                    var result = interpreter.Expression();
                    Console.WriteLine(result);
                }
            }
        }
    }
}
