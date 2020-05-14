using System;
using System.Collections;

namespace AnalaizerClass
{
    public class AnalyzerClass
    {
        // here will be analyzing stuff

        /// <summary>
        /// позиція виразу, на якій знайдена синтаксична помилка (у
        /// випадку відловлення на рівні виконання - не визначається)
        /// </summary>
        private static int erposition = 0;

        /// <summary>
        /// Вхідний вираз
        /// </summary>
        public static string expression = "";

        /// <summary>
        /// Показує, чи є необхідність у виведенні повідомлень про помилки.
        /// У разі консольного запуску програми це значення - false.
        /// </summary>
        public static bool ShowMessage = false;
        /// <summary>
        /// Останнє повідомлення про помилку.
        /// Поле і властивість для нього
        /// </summary>
        public static string lastError = "";
        /// <summary>
        /// Перевірка коректності структури в дужках вхідного виразу
        /// </summary>
        /// <returns>true - якщо все нормально, false- якщо є
        /// помилка</returns>
        /// метод біжить по вхідному виразу, символ за символом аналізуючи
        /// його, і рахуючи кількість дужок. У разі виникнення
        /// помилки повертає false, а в erposition записує позицію, на
        /// якій виникла помилка.
        public static bool CheckCurrency()
        {
            if (expression.Length > 65536)
            {
                //Дуже довгий вираз. Максмальная довжина — 65536 символів.
                ShowMessage = true;
                lastError = "Error 07";
                return false;
            }
            int p = 0, k = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression.Substring(i, 1) == "(")
                {
                    p++;
                    k = i;
                    if (p > 4)
                    {
                        break;
                    }
                }
                else if (expression.Substring(i, 1) == ")")
                {
                    if (p == 0)
                    {
                        ShowMessage = true;
                        lastError = "Error 01 at " + (i + 1).ToString();
                        erposition = i;
                        return false;
                    }
                    p--;
                }
            }
            if (p != 0)
            {
                //Неправильна структура в дужках, помилка на <i> символі.
                ShowMessage = true;
                lastError = "Error 01 at " + (k + 1).ToString();
                erposition = k;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Форматує вхідний вираз, виставляючи між операторами
        /// пропуски і видаляючи зайві, а також знаходить нерозпізнані оператори, стежить за кінцем рядка
        /// а також знаходить помилки в кінці рядка
        /// </summary>
        /// <returns>кінцевий рядок або повідомлення про помилку, що починаються з спец. символу &</returns>
        public static string Format()
        {
            string newExp = "";

            if (expression.Substring(0, 1) == "(" || expression.Substring(0, 1) == "-")
            {
                newExp = newExp + expression.Substring(0, 1);
            }
            else if (Char.IsNumber(expression, 0))
            {
                newExp = newExp + expression.Substring(0, 1);
            }
            else if (expression.Substring(0, 1) != " ")
            {
                //Невірна синтаксична конструкція вхідного виразу.
                ShowMessage = true;
                lastError = "Error 03";
                return "Error 03";
            }
            for (int i = 1; i < expression.Length; i++)
            {

                if (expression.Substring(i, 1) != " ")
                {
                    if (Char.IsNumber(expression, i))
                    {
                        //Console.WriteLine("i=" + i.ToString());
                        //Console.WriteLine("newExp[max-1]=" + newExp.Substring(newExp.Length - 1, 1));
                        //Console.WriteLine("newExp[max-3]=" + newExp.Substring(newExp.Length - 3, 1));
                        //Console.WriteLine(!Char.IsNumber(newExp, newExp.Length - 1));
                        //Console.WriteLine(newExp.Substring(newExp.Length - 1, 1) != "-");
                        //Console.WriteLine(!(newExp.Substring(newExp.Length - 1, 1) == "-" && (i == 1 || (i > 1 && newExp.Substring(newExp.Length - 3, 1) == "("))));
                        if (newExp.Substring(newExp.Length - 1, 1) == ")")
                        {
                            // Неправильна структура в дужках, помилка на<i> символі.
                            ShowMessage = true;
                            lastError = "Error 01 at " + (i + 1).ToString();
                            return "Error 01 at " + (i + 1).ToString();
                        }
                        if (!Char.IsNumber(newExp, newExp.Length - 1))
                        {

                            //Console.WriteLine("newExp[max-2]=" + newExp.Substring(newExp.Length - 1, 1));
                            if (newExp.Substring(newExp.Length - 1, 1) != "-")
                            {
                                newExp = newExp + " ";
                            }
                            else if (!(newExp.Substring(newExp.Length - 1, 1) == "-" && (i == 1 || (i > 1 && newExp.Substring(newExp.Length - 3, 1) == "("))))
                            {
                                newExp = newExp + " ";
                            }
                        }

                        newExp = newExp + expression.Substring(i, 1);
                    }
                    else if (expression.Substring(i, 1) == "+" || expression.Substring(i, 1) == "-" || expression.Substring(i, 1) == "/" || expression.Substring(i, 1) == "*" || expression.Substring(i, 1) == "(" || expression.Substring(i, 1) == ")" || expression.Substring(i, 1) == "%")
                    {
                        if (i > 0 && !Char.IsNumber(newExp, newExp.Length - 1) && newExp.Substring(newExp.Length - 1, 1) != ")" && expression.Substring(i, 1) != "(" && expression.Substring(i, 1) != "-")
                        {
                            //Два підряд оператори на <i> символі.
                            ShowMessage = true;
                            lastError = "Error 04 at " + (i + 1).ToString();
                            erposition = i;
                            return "Error 04 at " + (i + 1).ToString();
                        }
                        if (i > 0 && expression.Substring(i, 1) == "(" && Char.IsNumber(newExp, newExp.Length - 1))
                        {
                            //Неправильна структура в дужках, помилка на <i> символі.
                            ShowMessage = true;
                            lastError = "Error 01 at " + (i + 1).ToString();
                            return "Error 01 at " + (i + 1).ToString();
                        }
                        newExp = newExp + " " + expression.Substring(i, 1);
                    }
                    else
                    {
                        //Невідомий оператор на <i> символі.
                        ShowMessage = true;
                        lastError = "Error 02 at " + (i + 1).ToString();
                        erposition = i;
                        return "Error 02 at " + (i + 1).ToString();
                    }
                }
            }
            if (!Char.IsNumber(expression, expression.Length - 1) && expression.Substring(expression.Length - 1, 1) != " " && expression.Substring(expression.Length - 1, 1) != ")")
            {
                //Незавершений вираз
                ShowMessage = true;
                lastError = "Error 05";
                return "Error 05";
            }
            if (newExp.Substring(0, 1) == "-" && newExp.Substring(2, 1) == "(")
            {
                newExp = "0 " + newExp;
            }
            expression = newExp;
            return newExp;
        }

        /// <summary>
        /// Формує масив, в якому розташовуються оператори і символи
        /// представлені в зворотному польському записі(без дужок)
        /// На цьому ж етапі відшукується решта всіх помилок (див.
        /// код). По суті - це компіляція.
        /// </summary>
        /// <returns>массив зворотнього польського запису</returns>
        public static ArrayList CreateStack(ArrayList stack, string[] expMas)
        {
            ArrayList temp = new ArrayList();
            for (int i = 0; i < expMas.Length; i++)
            {
                if (expMas[i] == "(")
                {
                    int p = 0;
                    for (int j = i + 1; j < expMas.Length; j++)
                    {
                        if (expMas[j] == "(")
                        {
                            p++;
                        }
                        if (expMas[j] == ")")
                        {
                            if (p == 0)
                            {
                                string[] newMas = new string[j - i - 1];
                                for (int k = 0; k < j - i - 1; k++)
                                {
                                    newMas[k] = expMas[i + 1 + k];
                                }
                                stack = CreateStack(stack, newMas);
                                i = j;
                                break;
                            }
                            p--;
                        }
                    }
                    if (temp.Count != 0 && (temp[temp.Count - 1].Equals("*") || temp[temp.Count - 1].Equals("/") || temp[temp.Count - 1].Equals("%")))/*temp[temp.Count - 1] == "*" || temp[temp.Count - 1] == "/" || temp[temp.Count - 1] == "%")*/
                    {
                        stack.Add(temp[temp.Count - 1]);
                        temp.RemoveAt(temp.Count - 1);
                    }
                    if (temp.Count != 0 && (i < expMas.Length - 1 && (expMas[i + 1] == "+" || expMas[i + 1] == "-")))
                    {

                        stack.Add(temp[temp.Count - 1]);
                        temp.RemoveAt(temp.Count - 1);
                    }
                }
                int number;
                if (Int32.TryParse(expMas[i], out number))
                {
                    stack.Add(expMas[i]);
                    if (temp.Count != 0 && (temp[temp.Count - 1].Equals("*") || temp[temp.Count - 1].Equals("/") || temp[temp.Count - 1].Equals("%")))/*temp[temp.Count - 1] == "*" || temp[temp.Count - 1] == "/" || temp[temp.Count - 1] == "%")*/
                    {
                        stack.Add(temp[temp.Count - 1]);
                        temp.RemoveAt(temp.Count - 1);
                    }
                    if (temp.Count != 0 && (i < expMas.Length - 1 && (expMas[i + 1] == "+" || expMas[i + 1] == "-")))
                    {

                        stack.Add(temp[temp.Count - 1]);
                        temp.RemoveAt(temp.Count - 1);
                    }
                }
                else if (expMas[i] == "+" || expMas[i] == "-" || expMas[i] == "*" || expMas[i] == "/" || expMas[i] == "%")
                {
                    temp.Add(expMas[i]);
                }
            }
            while (temp.Count != 0)
            {
                stack.Add(temp[temp.Count - 1]);
                temp.RemoveAt(temp.Count - 1);
            }
            return stack;
        }

        /// <summary>
        /// Обчислення зворотнього польського запису
        /// </summary>
        ///<returns>результат обчислень,або повідомлення про помилку</returns>
        public static string RunEstimate(ArrayList stack)
        {
            ArrayList temp = new ArrayList();
            for (int i = 0; i < stack.Count; i++)
            {
                int number;
                if (Int32.TryParse(stack[i].ToString(), out number))
                {
                    if (Convert.ToInt64(stack[i]) < -2147483648 || Convert.ToInt64(stack[i]) > 2147483647)
                    {
                        //Дуже мале, або дуже велике значення числа для int. Числа повинні бути в межах від -2147483648 до 2147483647
                        ShowMessage = true;
                        lastError = "Error 06";
                        return "Error 06";
                    }
                    temp.Add(stack[i]);
                }
                else
                {
                    double left = Convert.ToDouble(temp[temp.Count - 2]);
                    double right = Convert.ToDouble(temp[temp.Count - 1]);
                    temp.RemoveAt(temp.Count - 1);
                    temp.RemoveAt(temp.Count - 1);
                    switch (stack[i])
                    {
                        case "+":
                            temp.Add(CalcClass.Add(left, right).ToString());
                            break;
                        case "-":
                            temp.Add(CalcClass.Sub(left, right).ToString());
                            break;
                        case "*":
                            temp.Add(CalcClass.Mult(left, right).ToString());
                            break;
                        case "/":
                            if (right == 0)
                            {
                                ShowMessage = true;
                                lastError = "Error 09";
                                return "Error 09";
                            }
                            temp.Add(CalcClass.Div(left, right).ToString());
                            break;
                        case "%":
                            if (right == 0)
                            {
                                ShowMessage = true;
                                lastError = "Error 09";
                                return "Error 09";
                            }
                            temp.Add(CalcClass.Mod(left, right).ToString());
                            break;

                    }
                }

            }
            expression = temp[0].ToString();
            return temp[0].ToString();
        }

        /// <summary>
        /// Метод, який організовує обчислення. По черзі запускає
        /// CheckCorrency, Format, CreateStack і RunEstimate
        /// </summary>
        /// <returns></returns>
        public static string Estimate(string exp)
        {
            ShowMessage = false;
            lastError = "";
            erposition = 0;
            expression = exp;
            if (!CheckCurrency())
            {
                return lastError;
            }
            //Console.WriteLine(Char.IsNumber(Format(), 0));
            Format();
            if (ShowMessage)
            {
                return lastError;
            }
            Console.Write(expression + " = ");
            string[] expMas = expression.Split(' ');
            if (expMas.Length > 30)
            {
                //Сумарна кількість чисел і операторів перевищує 30.
                ShowMessage = true;
                lastError = "Error 08";
                return "Error 08";
            }
            ArrayList stack = new ArrayList();
            stack = CreateStack(stack, expMas);
            //for (int i = 0; i < stack.Count; i++)
            //{
            //    Console.WriteLine(stack[i]);

            //}
            RunEstimate(stack);
            if (ShowMessage)
            {
                return lastError;
            }
            return expression;
        }
    }
}
