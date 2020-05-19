using AnalaizerClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalcUI : Form
    {
        string[] args;
        static int alarmCounter = 0;
        static bool timerStart = false;

        private double tempNumber = 0;
        public CalcUI(string[] args)
        {
            this.args = args;
            InitializeComponent();
            if (args.Length > 0)
            {
                string expression = args[0];
                Console.WriteLine(AnalyzerClass.Estimate(expression));
            }
        }

        private void buttonLeftParenthesis_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "(";
        }

        private void buttonRightParenthesis_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + ")";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "9";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "0";
        }

        private void buttonDiv_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "/";
        }

        private void buttonMult_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "*";
        }

        private void buttonSub_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "-";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "+";
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text.Length > 0)
            {
                textBoxExpression.Text = textBoxExpression.Text.Substring(0, textBoxExpression.Text.Length - 1);
            }
        }

        private void buttonMod_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + "%";
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = "";
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = AnalyzerClass.Estimate(textBoxExpression.Text);
        }
        //private void buttonEqual_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)Keys.Enter)
        //    {
        //        MessageBox.Show("Enter key pressed");
        //    }

        //}

        private void buttonM_Click(object sender, EventArgs e)
        {
            double number;
            if (Double.TryParse(textBoxResult.Text, out number))
            {
                tempNumber = tempNumber + Convert.ToDouble(textBoxResult.Text);
            }
            else
            {
                textBoxResult.Text = "Cannot be converted to a number";
            }
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            tempNumber = 0;
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = textBoxExpression.Text + tempNumber.ToString();
        }

        private void buttonEscape_Click(object sender, EventArgs e)
        {
            CalcUI.ActiveForm.Close();
        }



        private void buttonUnary_MouseDown(object sender, MouseEventArgs e)
        {
            timerStart = true;
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
        }

        private void buttonUnary_MouseUp(object sender, MouseEventArgs e)
        {
            timerStart = false;
            timer1.Stop();
            if (alarmCounter < 3)
            {
                //MessageBox.Show("timer > 3= " + alarmCounter.ToString());
                if (textBoxExpression.Text != "")
                {
                    string exp = textBoxExpression.Text;
                    if (exp.Substring(exp.Length - 1, 1) == "-")
                    {
                        textBoxExpression.Text = exp.Substring(0, exp.Length - 1) + "+";
                    }
                    else if (exp.Substring(exp.Length - 1, 1) == "+")
                    {
                        textBoxExpression.Text = exp.Substring(0, exp.Length - 1) + "-";
                    }
                    else if (exp.Substring(exp.Length - 1, 1) == "*" || exp.Substring(exp.Length - 1, 1) == "/" || exp.Substring(exp.Length - 1, 1) == "%")
                    {
                        textBoxExpression.Text = exp + "(-";
                    }
                    else if (exp.Substring(exp.Length - 1, 1) == ")")
                    {
                        textBoxExpression.Text = exp + "-";
                    }
                    else
                    {
                        for (int i = exp.Length - 1; i >= 0; i--)
                        {

                            if (!Char.IsNumber(exp, i))
                            {
                                if (exp.Substring(i, 1) == "-")
                                {
                                    textBoxExpression.Text = exp.Substring(0, i) + "+" + exp.Substring(i + 1, exp.Length - i - 1);
                                }
                                else if (exp.Substring(i, 1) == "+")
                                {
                                    textBoxExpression.Text = exp.Substring(0, i) + "-" + exp.Substring(i + 1, exp.Length - i - 1);
                                }
                                else if (exp.Substring(i, 1) == "*" || exp.Substring(i, 1) == "/" || exp.Substring(i, 1) == "%")
                                {
                                    textBoxExpression.Text = exp.Substring(0, i + 1) + "(-" + exp.Substring(i + 1, exp.Length - i - 1);
                                }

                                break;
                            }
                        }
                    }

                }
            }
            else
            {
                if (textBoxExpression.Text != "")
                {
                    string exp = textBoxExpression.Text;
                    if (exp.Length >= 2)
                    {
                        if (exp.Substring(0, 1) == "-" && exp.Substring(1, 1) == "(" && exp.Substring(exp.Length - 1, 1) == ")")
                        {
                            textBoxExpression.Text = exp.Substring(2, exp.Length - 3);
                        }
                        else
                        {
                            textBoxExpression.Text = "-(" + exp + ")";
                        }
                    }
                    else
                    {
                        textBoxExpression.Text = "-" + exp;
                    }
                }
            }
            alarmCounter = 0;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void textBoxExpression_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
