using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace lab1
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите выражение:");
            string str = Console.ReadLine();
            Number(str);
            Operation(str);
        }
        public static void Number(string str)
        {
            List<int> numbers = new List<int>();
            string[] arrayNumber = str.Split('-', '*', '/', '+');

            foreach (string num in arrayNumber)
            {
                numbers.Add(int.Parse(num));
            }

            Console.WriteLine(string.Join(" ", numbers));
        }

        public static void Operation(string str)
        {
            List<char> chars = new List<char>();
            char[] testOperation = { '-', '*', '/', '+' };
            for (int i = 0; i < str.Length; i++)
            {
                if (testOperation.Contains(str[i]))
                {
                    chars.Add(str[i]);
                }
            }
            Console.WriteLine(string.Join(" ", chars));
        }
    }
}