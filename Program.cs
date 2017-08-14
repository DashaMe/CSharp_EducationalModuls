using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDashaM
{
    class Program
    {
        static string ch;
        static void Main(string[] args)
        {
            /*int[] massive = new int[3] { 1, 5, 7 };
            Console.WriteLine(massive[2]);

            int[] massive1 = new int[] { 1, 5, 7 };
            Console.WriteLine(massive1[0]);

            int[] massive2 = new []{ 1, 5, 7 };
            Console.WriteLine(massive2[2]);

            int[] massive3 = { 1, 5, 7 };
            Console.WriteLine(massive3[1]);

            int[] massive4 = new int[2];
            massive4[0] = 2;
            massive4[1] = 5;
            Console.WriteLine(massive[1]);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }*/

            string ch="", nch="";

            for (int i = 0; i < 5; i++)
            {
                int[] nums = new int[5];
                Console.WriteLine("Please, enter number " + (i + 1));
                nums[i] = int.Parse(Console.ReadLine());
                if (nums[i]%2==0)
                {
                ch+=nums[i] + " ";
                }
                else
                {
                nch+=nums[i] + " ";
                }
             }

         Console.WriteLine ("Even numbers" + ch);
            Console.WriteLine("Odd numbers" + nch);


        }
    }
}