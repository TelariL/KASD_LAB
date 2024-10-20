using System;

using MyStack;

namespace Lab9
{
    class Program
    {
        static public int WeightOfOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "//":
                    return 2;
                case "^":
                    return 3;
                case "sqrt":
                case "ln":
                case "cos":
                case "sin":
                case "tg":
                case "ctg":
                case "abs":
                case "log":
                case "min":
                case "max":
                case "mod":
                case "exp":
                case "trunc":
                    return 4;
                default: return 0;
            }
        }

        static private double InitializationOperation(string operation, params double[] x) => operation switch
        {
            "+" => x[0] + x[1],
            "-" => x[0] - x[1],
            "*" => x[0] * x[1],
            "/" => x[0] / x[1],
            "//" => Math.Floor(x[0] / x[1]),
            "^" => Math.Pow(x[0], x[1]),
            "exp" => Math.Exp(x[0]),
            "sqrt" => Math.Sqrt(x[0]),
            "ln" => Math.Log(x[0]),
            "log" => Math.Log10(x[0]),
            "cos" => Math.Cos(x[0]),
            "sin" => Math.Sin(x[0]),
            "tg" => Math.Tan(x[0]),
            "ctg" => 1 / Math.Tan(x[0]),
            "abs" => Math.Abs(x[0]),
            "min" => x[0] < x[1] ? x[0] : x[1],
            "max" => x[0] > x[1] ? x[0] : x[1],
            "mod" => (int)x[0] % (int)x[1],
            "trunc" => Math.Truncate(x[0]),
            _ => throw new Exception("Unknown operation")
        };
        static private string Variable(string example)
        {
            MyVector<string> vectorOfItem = new MyVector<string>();
            for(int i = 0; i < example.Length; i++)
            {
                string item = "";
                while (i < example.Length && Char.IsLetter(example[i]))
                {
                    item += example[i];
                    i++;
                }
                if (item.Length > 0)
                {
                    switch (item)
                    {
                        case "sqrt":
                        case "ln":
                        case "cos":
                        case "sin":
                        case "tg":
                        case "ctg":
                        case "abs":
                        case "log":
                        case "min":
                        case "max":
                        case "mod":
                        case "exp":
                        case "trunc":
                            break;
                        default:
                            vectorOfItem.Add(item);
                            break;
                    }
                }
            }

            for (int i = 0; i < vectorOfItem.Size(); i++)
            {
                Console.WriteLine($"Input variable {vectorOfItem.Get(i)}: ");
                example = example.Replace(vectorOfItem.Get(i), Console.ReadLine());
            }
            return example;
        }

        static public MyVector<string> ToPostfixForm(string example)
        {
            example = Variable(example);
            MyStack<string> stack = new MyStack<string>();
            MyVector<string> result = new MyVector<string>();

            char[] basicOperations = new char[] { '+', '-', '*', '^', '/' };

            for(int i = 0; i < example.Length; i++)
            {
                string number = "";
                if ((result.IsEmpty() && example[i] == '-') || (i > 0 && example[i] == '-' && example[i - 1] == '('))
                {
                    number += example[i];
                    i++;
                }
                while (i < example.Length && (Char.IsDigit(example[i]) || example[i] == '.'))
                {
                    number += example[i];
                    i++;
                }
                if (number.Length > 0) { result.Add(number); }

                string func = "";
                while (i < example.Length && Char.IsLetter(example[i]))
                {
                    func += example[i];
                    i++;
                }
                if (func.Length > 0 && func == "pi") result.Add("3.14");
                else if (func.Length > 0) { stack.Push(func); }

                if (i < example.Length && basicOperations.Contains(example[i]))
                {
                    if (stack.Empty()) { stack.Push(example[i].ToString()); }
                    else
                    {
                        while (!stack.Empty() && (WeightOfOperation(stack.Peek()) > WeightOfOperation(example[i].ToString())))
                        {
                            string b = stack.Peek();
                            result.Add(b.ToString());
                            stack.Pop();
                        }
                        stack.Push(example[i].ToString());
                    }
                }
                else if (i < example.Length && example[i] == '(') stack.Push(example[i].ToString());
                else if (i < example.Length && example[i] == ')')
                {
                    while (!stack.Empty())
                    {
                        string b = stack.Peek();
                        if (b == "(")
                        {
                            stack.Pop();
                            break;
                        }
                        result.Add(b.ToString());
                        stack.Pop();
                    }
                }
            }
            while (!stack.Empty())
            {
                string b = stack.Peek();
                if (b != "(") result.Add(b.ToString());
                if (b == ")") throw new Exception("Count of brackets");
                stack.Pop();
            }

            return result;
        }

        static public double CalculateExample(string example)
        {
            if (example == null) throw new Exception("Empty example");
            MyVector<string> postfixForm = ToPostfixForm(example);
            MyStack<double> stack = new MyStack<double>();

            char[] basicOperator = new char[] { '+', '-', '*', '^', '/' };

            for (int i = 0; i < postfixForm.Size(); i++)
            {
                string element = postfixForm.Get(i);
                if (Char.IsDigit(element[0]) || (element.Length > 1 && Char.IsDigit(element[1])))
                {
                    element = element.Replace('.', ',');
                    double number = Convert.ToDouble(element);
                    stack.Push(number);
                }
                else if (Char.IsLetter(element[0]))
                {
                    if (element == "max" || element == "min" || element == "mod")
                    {
                        double number1 = stack.Peek();
                        stack.Pop();
                        double number2 = stack.Peek();
                        stack.Pop();
                        stack.Push(InitializationOperation(element, number1, number2));
                    }
                    else
                    {
                        double number = stack.Peek();
                        stack.Pop();
                        stack.Push(InitializationOperation(element, number));
                    }
                }
                else if (basicOperator.Contains(element[0]))
                {
                    if (i < postfixForm.Size() - 1 && element == "/" && postfixForm.Get(i + 1) == "/")
                    {
                        i++;
                        element += postfixForm.Get(i);
                    }
                    double number2 = stack.Peek();
                    stack.Pop();
                    double number1 = stack.Peek();
                    stack.Pop();
                    stack.Push(InitializationOperation(element, number1, number2));
                }
            }
            double answer = stack.Peek();
            stack.Pop();
            return answer;
        }

        static void Main(string[] args)
        {
            string example = Console.ReadLine();
            Console.WriteLine(CalculateExample(example));
        }
    }
}
