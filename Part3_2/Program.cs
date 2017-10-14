using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ввеите последовательность чисел");
            int n = int.Parse(Console.ReadLine());
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            while (a != b)
            {
                if (a > b) a = a - b;
                else b = b - a;

            }
            for (int i = 1; i <= n - 2; i++)
            {
                b = int.Parse(Console.ReadLine());
                while (a != b)
                {
                    if (a > b) a = a - b;
                    else b = b - a;

                }
            }
            Console.WriteLine(a);

        }
    }
}
