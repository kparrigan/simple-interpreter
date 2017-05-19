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
                Console.Write("si> ");
                var text = Console.ReadLine();

                try
                {
                    if (!string.IsNullOrEmpty(text))
                    {
                        var lexer = new Lexer(text);
                        var parser = new Parser(lexer);
                        var interpreter = new Interpreter(parser);
                        var result = interpreter.Interpret();

                        Console.WriteLine(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
