using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

namespace CTDL_GT
{
    class Program
    {
        static void Main(string[] args)
        {
           
            // Khai báo đường dẫn và đọc dữ liệu từ file
            var filePath = Directory.GetParent("CTDL_GT").Parent.Parent.Parent.Parent.FullName;
            var filePath_read = filePath + @"\data\BIEUTHUC.txt";
            List<string> lines = new List<string>();
            List<string> box = new List<string>();
            lines = File.ReadAllLines(filePath_read).ToList();
            string s = "";
            for (int i =1; i < lines.Count;i++)
            {
                s = lines[i].Replace(" ","");
                s = change_Infix(s);
                Queue Posfix = new Queue();
                Posfix = infixToPosfix(s);
                box.Add(Calculator(Posfix).ToString());
            }

            // ghi kết quả xuống file
            
            var filePath_write = filePath + @"\data\KETQUA.txt";
            File.WriteAllLines(filePath_write, box);
        }


        // Hàm chuyển đổi toán hạng âm và mang giai thừa thành toán tử hai ngôi
        public static String change_Infix(string s)
        {
           string a = "";
           if (s[0] == '-')
            {
                a += '0';
            }
           for (int i = 0; i <s.Length;i++)
            {
                if (i != 0 && s[i - 1] == '(' && s[i] == '-')
                    a += 0;
                a += s[i];
                if (s[i] == '!')
                {
                    a += '0';
                }
            }
            return a;
        }


        // Hàm chuyển đổi Infix sang Posfix
        public static Queue infixToPosfix (String a)
        {
            Queue posfix = new Queue();
            Stack stack = new Stack();
            string token,x;
            string newQueue = "";
            for (int i = 0; i<a.Length;i++)
            {
                token = a[i].ToString();
                if (token.All(Char.IsDigit) || token =="." )
                {
                    newQueue += token;
                }
                else
                {
                    if (IsOperator (token))
                    {
                        if (newQueue != "")
                        {
                            if (IsNumber(newQueue))
                            {
                                posfix.Enqueue(newQueue);
                                newQueue = "";
                            }
                            else
                            {
                                throw new Exception($"{newQueue} + đây ko phải là số. Syntax Error");
                            }
                        }

                        if (token == "(")
                        {
                            stack.Push(token);
                        }
                        else if (token == ")")
                        {
                            x = stack.Pop();
                            while (x != "(")
                            {
                                posfix.Enqueue(x);
                                x = stack.Pop();
                            }
                        }
                        else
                        {
                            while (!stack.isEmty() && (precedence(token) <= precedence(stack.Gethead())))
                            {
                                x = stack.Pop();
                                posfix.Enqueue(x);
                            }
                            stack.Push(token);
                        }
                    }else
                    {
                        throw new Exception("Chuỗi nhập vào không hợp lệ");
                    }
                    
                }
            }

            if (newQueue != "" && IsNumber(newQueue))
            {
                posfix.Enqueue(newQueue);
            }
          

            while(!stack.isEmty())
            {
                x = stack.Pop();
                posfix.Enqueue(x);
            }

            return posfix;

        }
        

        // Hàm trả về độ ưu tiên của toán tử
        public static int precedence(string x)
        {
            if (x == "(")
                return 0;
            if (x == "+" || x == "-")
                return 1;
            if (x == "*" || x == "/")
                return 2;
            if (x == "^")
                return 3;
            return 4;
        }

        // Kiểm tra 1 chuỗi string đây có phải là số.
        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private static bool IsOperator(string str)
        {
            return Regex.Match(str, @"\+|\-|\*|\/|\^|\!|\(|\)").Success;
        }

        //hàm tính toán
        public static double Calculator(Queue profix)
        {
            Stack stack = new Stack();
            double result = 0;
            string x, opr1, opr2;
            while (!profix.isEmty())
            {
                x = profix.Dequeue();
                if (x.All(Char.IsDigit))
                {
                    stack.Push(x);
                }
                else
                {
                    opr1 = stack.Pop();
                    opr2 = stack.Pop();
                    result = cal(x, opr1, opr2);
                    stack.Push(result.ToString());
                }
            }
            return result;
        }

        public static double cal(string x, string opr1, string opr2)
        {
            double result = 0;
            double a = double.Parse(opr1);
            double b = double.Parse(opr2);
            switch (x)
            {
                case "+":
                    return b + a;
                case "-":
                    return b - a;
                case "*":
                    return b * a;
                case "/":
                    return b / a;
                case "^":
                    return Math.Pow(b, a);
                case "!":
                    if (b % 1 != 0)
                    {
                        throw new Exception("Không có chức năng tính thập phân cho số có giai thừa");
                    }
                    return Giaithua(b,a);
            }
            return result;
        }

        public static double Giaithua(double b , double a)
        {
            double result = 1;
            while (b > a )
            {
                result *= b;
                b--;
            }
            return result;

        }
    }
}
