using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
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
            str = str.Replace(" ", string.Empty);
            GetAnsewer(OperationsAndNumbers(str));
            
        }

        public static ArrayList OperationsAndNumbers(string str)
        {
            List<char> operations = new List<char>();
            List<double> numbers = new List<double>();            
            char[] testOperation = { '-', '*', '/', '+' };
            string[] arrayNumber = str.Split(testOperation, StringSplitOptions.RemoveEmptyEntries);
            foreach (string num in arrayNumber)
            {
                numbers.Add(double.Parse(num));
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (testOperation.Contains(str[i]))
                {
                    operations.Add(str[i]);
                }
            }
            Console.WriteLine($"список чисел: {string.Join(" ", numbers)}");
            Console.WriteLine($"список операций: {string.Join(" ", operations)}");
            ArrayList lists = new ArrayList() { operations, numbers };
            return lists;
        }
        public static void GetAnsewer(ArrayList lists)
        {
            List<char> operations = (List<char>)lists[0];
            List<double> numbers = (List<double>)lists[1];
            
            while (numbers.Count != 1)
            {
                for (int i =0; i < operations.Count; i++)
                {
                    double number;
                    if (operations[i] == '*' || operations[i] == '/')
                    {
                        if (operations[i] == '*')
                        {
                            number = numbers[i] * numbers[i + 1];
                        }

                        else
                        {
                            number = numbers[i] / numbers[i + 1];
                        }
                        numbers.RemoveAt(i+1);
                        numbers.RemoveAt(i);
                        operations.RemoveAt(i);
                        numbers.Insert(i, number);
                    }
                    else if (!(operations.Contains('*')|| operations.Contains('/')))
                    {
                        if (operations[i] == '+')
                        {
                            number = numbers[i] + numbers[i + 1];
                        }
                        else
                        {
                            number = numbers[i] - numbers[i + 1];
                        }
                        numbers.RemoveAt(i + 1);
                        numbers.RemoveAt(i);
                        numbers.Insert(i, number);
                        operations.RemoveAt(i);
                        
                    }
                }
                
               
            }
            Console.WriteLine($"ответ: {string.Join(" ", numbers)}");

        }
        
    }
}