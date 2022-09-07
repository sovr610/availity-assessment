// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;

namespace LispChecker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            userInput userInput = new userInput();
            lispChecker lispChecker = new lispChecker();

            var lispCode = userInput.input();
            if (!String.IsNullOrEmpty(lispCode))
            {
                bool result = lispChecker.validateLispCode(lispCode);

                if (result)
                {
                    Console.WriteLine("Lisp code checks out and is properly closed and nested");
                }
                else
                {
                    Console.WriteLine("Lisp code does not check out and is not properly closed and nested");
                }
            }
            else
            {
                Console.WriteLine("did not detect any code, please re-enter the lisp code.");
            }
        }
    }
}
