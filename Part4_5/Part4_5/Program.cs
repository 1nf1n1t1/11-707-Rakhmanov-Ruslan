using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part4_5
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1,k = 1, number=1,sum=0;//count=1 так как 6 совершенное число
            
            while (number <= 1000000)
            {
                k += 2;               
                number += k * k * k;
                for (int i = 1; i <= number; i++)
                {
                    if (number % i == 0) sum += i; 
                   
                }
                if (2 * number == sum) count++;
                sum = 0; 

            }
            

            Console.WriteLine(count);
        }
    }
}
