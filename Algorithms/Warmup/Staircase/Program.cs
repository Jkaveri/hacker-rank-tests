using System;

namespace Staircase
{
    class Program
    {
        // Complete the staircase function below.
        static void staircase(int n)
        {
            var i = 0;
            while (i < n)
            {
                var j = n;
                while (j > i + 1)
                {
                    Console.Write(" ");
                    j--;
                }
                while (j-- > 0)
                {
                    Console.Write("#");
                }

                Console.WriteLine("");
                i++;
            }
        }

        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            staircase(n);
            Console.ReadLine();
        }
    }
}
