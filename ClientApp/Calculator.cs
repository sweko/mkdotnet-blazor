using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp
{
    public class Calculator
    {
        public int Add (int first, int second)
        {
            return first + second;
        }

        public int Subtract (int first, int second)
        {
            return first - second;
        }

        public int Multiply (int first, int second)
        {
            return first * second;
        }

        public int Divide (int first, int second)
        {
            if (second == 0)
            {
                throw new DivideByZeroException("The result is not defined if the divisor is zero");
            }
            return first / second;
        }
    }
}
