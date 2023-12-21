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

        public static List<object> OperationsAndNumbers(string str)
        {            
            List<object> numbers = new List<object>();            
            char[] testOperation = { '-', '*', '/', '+' };
            string[] arrayNumber = str.Split(testOperation, StringSplitOptions.RemoveEmptyEntries);
            foreach (string num in arrayNumber)
            {
                numbers.Add(num);
            }                       
            return numbers;
        }                   
    }
}