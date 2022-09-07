using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LispChecker
{
    internal class userInput
    {
        private string lispCode;

        public string input()
        {

            Console.WriteLine("Please paste your lisp code in the console window");
            Console.Write(":>");
            lispCode = Console.ReadLine();
            Console.WriteLine("code entered: " + lispCode);
            return lispCode;
        }
    }
}
