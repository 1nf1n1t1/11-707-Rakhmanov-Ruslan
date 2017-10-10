using System;

namespace Part1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var startPosition = Console.ReadLine();
            var finishPosition = Console.ReadLine();
            int stNum = int.Parse(startPosition[1].ToString());
            int fshNum = int.Parse(finishPosition[1].ToString());

            if (startPosition[0] == finishPosition[0])
            {
                if (Math.Abs(stNum - fshNum) == 1 && (stNum != 1 || stNum != 8)) Console.WriteLine("Yes");
                else if (Math.Abs(stNum - fshNum) == 2 && (stNum == 2 || stNum == 7)) Console.WriteLine("Yes");
                else Console.WriteLine("No");
            }
            else Console.WriteLine("No");
        }
    }
}
