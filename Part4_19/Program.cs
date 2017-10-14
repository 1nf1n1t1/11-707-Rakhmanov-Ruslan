using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part4_19
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1,k=1,sum=0;
            int number = int.Parse(Console.ReadLine());
            while (sum != number)
            {
                
                k= i;
                sum = 0;
                for (; sum < number; )
                {
                    sum += k;
                    k++;
                }
                i++;
            }
                Console.WriteLine(k-i+1);
                
            
        }
        
    }
}
