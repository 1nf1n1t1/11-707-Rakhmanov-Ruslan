using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1_19
{
    class Program
    {
        static void Main(string[] args)

        {

            int countCows=0,replace;
            int numberFirst = int.Parse(Console.ReadLine());
            int numberSecond = int.Parse(Console.ReadLine());
            if (numberFirst > numberSecond)
            {
                replace = numberSecond;
                numberSecond = numberFirst;
                numberFirst = replace;
            }                               
            int numF1= numberFirst / 1000;
            int numF2 = ((numberFirst / 100) - (numF1 * 10));
            int numF3 = ((numberFirst / 10) - (numF1 * 100) - (numF2 * 10));
            int numF4 = (numberFirst - (numF1 * 1000) - (numF2 * 100) - (numF3 * 10));
            int numS1 = numberSecond / 1000;
            int  numS2 = ((numberSecond / 100) - (numS1 * 10));
            int numS3 = ((numberSecond / 10) - (numS1 * 100) - (numS2 * 10));
            int numS4 = (numberSecond - (numS1 * 1000) - (numS2 * 100) - (numS3 * 10));
            if (numF1 == numS2)

                countCows++;

            else if (numF1 == numS3)

                countCows++;

            else if (numF1== numS4)

                countCows++;

            if (numF2 == numS1)

                countCows++;

            else if (numF2 == numS3)

                countCows++;

            else if (numF2 == numS4)

                countCows++;

            if (numF3 == numS1)

                countCows++;

            else if (numF3 == numS2)

                countCows++;

            else if (numF3 == numS4)

                countCows++;

            if (numF4 == numS1)

                countCows++;

            else if (numF4 == numS2)

                countCows++;

            else if (numF4 == numS3)

                countCows++;

            Console.WriteLine(countCows);

        }
    }
}
