using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LispChecker
{
    internal class lispChecker
    {
        private int parentheses;

        public lispChecker()
        {
            parentheses = 0;
        }

        /// <summary>
        /// checks if the parentheses are properly nested in the lisp code gathered from user.
        /// </summary>
        /// <param name="lispCode">lisp code from user as a string</param>
        /// <returns>boolean valide lisp code</returns>
        public bool validateLispCode(string lispCode)
        {
            foreach(var character in lispCode)
            {
                if(character == '(')
                {
                    parentheses++;
                }

                if(character == ')')
                {
                    parentheses--;
                }
            }
            
            if(parentheses == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
