using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_8
{
    class Program
    {
        static void Main(string[] args)
        {
            long a1, a2, a3, t=0, n,i;
            n = long.Parse(Console.ReadLine());
            a1 = long.Parse(Console.ReadLine());
            a2 = long.Parse(Console.ReadLine());
            
            for (i = 1; i <= n - 2; i++)
            {
                a3 = long.Parse(Console.ReadLine());
                if ((a1 > a2) && (a2 < a3)) t++;
                if ((a1 < a2) && (a2 > a3)) t++;
                a1 = a2;
                a2 = a3;

            }
            if (t == n - 2)  Console.WriteLine("Пилообразная");
                else Console.WriteLine("Не пилообразная");
        }

        
       
    }
}
