using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2_16
{
    class Program
    {
        static void Main(string[] args)
        {
            int i,number;
            long maxlong,count=0,maxcount=0;
            maxlong = long.Parse(Console.ReadLine());
            for (i = 1; i <= maxlong; i++)
            {
               
                number = i;
                while (number!=0)
                { if (maxcount < count) maxcount = count;
                  if (number % 2 == 0)
                  {
                     count++;
                     number = number / 2;
                  }
                  else
                  {
                      count = 0;
                      number = number / 2;
                  }

                }
               
                count = 0;
               
            }
            Console.WriteLine(maxcount);
            
        }
    }
}
