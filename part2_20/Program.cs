using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace part2_20
{
    class Program
    {
        static void Main(string[] args)
        {
            string k = Console.ReadLine();
            int number =int.Parse(k);
            Console.WriteLine(Estimation(number));
        }
        static string Estimation(int number)
        {
            string text=null;
            while (number > 0)
            {
               
                if (number % 2==0)
                {
                    text = text + "0";
                }
                number = number /2;
            }

            return text;
        }

    }
}
