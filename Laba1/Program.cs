﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LabCalculator
{
    public abstract class Token
    {
        
    }

    public class Number : Token
    {
        public float Value { get; }

        public Number(float value)
        {
            Value = value;
        }
        public override string ToString() 
        {
            return "" + Value;
        }
        
    }

    public class Operation : Token
    {
        public char Operator { get; }

        public Operation(char op)
        {
            Operator = op;
        }
        public override string ToString()
        {
            return "" + Operator;
        }
    }

    public class Parenthesis : Token
    {
        public char ParenthesisType { get; }

        public Parenthesis(char type)
        {
            ParenthesisType = type;
        }
        public override string ToString()
        {
            return "" + ParenthesisType;
        }
    }

    public class Calculator
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                string input = Console.ReadLine().Replace(" ", "");
                List<Token> parsedInput = Parse(input);
                List<Token> rpn = ToRPN(parsedInput);
                float result = CalculateWithRPN(rpn);
                Console.WriteLine(string.Join(" ", parsedInput));
                Console.WriteLine(string.Join(" ", rpn));
                Console.WriteLine(result);
            }
        }

        public static List<Token> Parse(string input)
        {
            List<Token> tokenList = new List<Token>();
            string number = "";
            foreach (var element in input + ' ')
            {
                if (char.IsDigit(element) || element == '.')
                {
                    number += element;
                    continue;
                }
                else if (!string.IsNullOrEmpty(number))
                {
                    tokenList.Add(new Number(float.Parse(number)));
                    number = "";
                }

                if (element == '*' || element == '/' || element == '+' || element == '-')
                {
                    tokenList.Add(new Operation(element));
                    continue;
                }

                if (element == '(' || element == ')')
                {
                    tokenList.Add(new Parenthesis(element));
                }
            }
            return tokenList;
        }

        public static List<Token> ToRPN(List<Token> input)
        {
            List<Token> result = new List<Token>();
            Stack<Token> stack = new Stack<Token>();

            foreach (var token in input)
            {
                if (token is Number)
                {
                    result.Add(token);
                }
                else if (token is Operation)
                {
                    while (stack.Count > 0 && stack.Peek() is Operation &&
                           GetPriority((stack.Peek() as Operation).Operator) >= GetPriority((token as Operation).Operator))
                    {
                        result.Add(stack.Pop());
                    }
                    stack.Push(token);
                }
                else if (token is Parenthesis)
                {
                    if ((token as Parenthesis).ParenthesisType == '(')
                    {
                        stack.Push(token);
                    }
                    else if ((token as Parenthesis).ParenthesisType == ')')
                    {
                        while (stack.Count > 0 && !(stack.Peek() is Parenthesis) && ((stack.Peek() as Parenthesis).ParenthesisType == '('))
                        {
                            result.Add(stack.Pop());
                        }

                        if (stack.Count == 0 || !(stack.Peek() is Parenthesis) || ((stack.Peek() as Parenthesis).ParenthesisType != '('))
                        {
                            throw new Exception("Invalid expression: mismatched parentheses");
                        }

                        stack.Pop();
                    }
                }
            }

            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }

        public static float CalculateWithRPN(List<Token> rpn)
        {
            Stack<float> stack = new Stack<float>();

            foreach (var token in rpn)
            {
                if (token is Number)
                {
                    stack.Push((token as Number).Value);
                }
                else if (token is Operation)
                {
                    float num2 = stack.Pop();
                    float num1 = stack.Pop();
                    char op = (token as Operation).Operator;
                    float intermediateResult = Calculate(op, num1, num2);
                    stack.Push(intermediateResult);
                }
            }

            return stack.Peek();
        }

        public static float Calculate(char op, float num1, float num2)
        {
            switch (op)
            {
                case '+': return num1 + num2;
                case '-': return num1 - num2;
                case '*': return num1 * num2;
                case '/': return num1 / num2;
                default: throw new Exception("Unknown operation");
            }
        }

        public static int GetPriority(char op)
        {
            switch (op)
            {
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                default: return 0;
            }
        }
    }
}