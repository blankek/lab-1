using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Lesson1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите выражение: ");
            string str = Console.ReadLine();
            str = str.Replace(" ", string.Empty);
            var prn = Rpn(str);
            Console.Write("ОПЗ: ");
            Console.WriteLine(string.Join(" ", prn));
            Console.Write("Значение: ");
            Console.WriteLine(string.Join(" ", Result(prn)));
        }

        public static List<object> Rpn(string str)
        {
            Dictionary<char, int> priority = new Dictionary<char, int>
            {
                {'+', 1},
                {'-', 1},
                {'*', 2},
                {'/', 2},
                {'(', 0},
                {')', 3},
            };
            List<object> rpn = new List<object>();
            Stack<char> stack = new Stack<char>();
            string number = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                if (priority.ContainsKey(str[i]))
                {
                    if (number != string.Empty)
                    {
                        rpn.Add(number);
                        number = string.Empty;
                    }

                    if (str[i] == ')')
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                        {
                            rpn.Add(stack.Pop());
                        }
                        if (stack.Count > 0 && stack.Peek() == '(')
                        {
                            stack.Pop();
                        }
                    }
                    else if (stack.Count == 0 || str[i] == '(' || priority[str[i]] > priority[stack.Peek()])
                    {
                        stack.Push(str[i]);
                    }
                    else if (priority[str[i]] <= priority[stack.Peek()])
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                        {
                            rpn.Add(stack.Pop());
                        }
                        if (stack.Count > 0 && stack.Peek() == '(')
                        {
                            stack.Pop();
                        }
                        stack.Push(str[i]);
                    }
                }
                else
                {
                    number += str[i];
                }
            }
            rpn.Add(number);
            while (stack.Count > 0)
            {
                rpn.Add(stack.Pop());
            }
            return rpn;
        }

        public static Stack<double> Result(List<object> expression)
        {
            Stack<double> stack = new Stack<double>();
            for (var i = 0; i < expression.Count; i++)
            {
                if (expression[i] is string)
                {
                    double num = Convert.ToDouble(expression[i]);
                    stack.Push(num);
                }
                else
                {
                    var second = stack.Pop();
                    var first = stack.Pop();
                    stack.Push(Calculate((char)expression[i], first, second));
                }
            }
            return stack;
        }

        public static double Calculate(char op, double first, double second)
        {
            switch (op)
            {
                case '*': return first * second;
                case '/': return first / second;
                case '+': return first + second;
                case '-': return first - second;
                default: return double.NaN;
            }
        }
    }
}