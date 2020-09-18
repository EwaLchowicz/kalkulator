using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kalkulator_poig
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string value1 = "0";
        string value2 = "0";
        string sign = "";
        double number1, number2, result;
        int counter = 1;
        int click_counter = 0;
        bool isPoint = false;
        bool isMinus = false;
        int operation, state;

        enum State
        {
            NONE,
            NUMBER1,
            NUMBER2
        }

        enum Operation
        {
            NONE,
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVIDE,
            PERCENT
        }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Double.Parse(value1) == 0)
            {
                but0.IsEnabled = false;
            }
            else
            {
                but0.IsEnabled = true;
            }

            if (value1.Contains("-"))
            {
                isMinus = true;
            }

            if (value1.Contains(","))
            {
                isPoint = true;
                but_point.IsEnabled = false;
            }

            operation = (int)Operation.NONE;
            state = (int)State.NUMBER1;
            refreshDisplay(state);
        }



        private int set_operation(string o)
        {
            if (o.Equals("+")) return (int)Operation.ADD;
            else if (o.Equals("-")) return (int)Operation.SUBTRACT;
            else if (o.Equals("*")) return (int)Operation.MULTIPLY;
            else if (o.Equals("/")) return (int)Operation.DIVIDE;
            else if (o.Equals("%")) return (int)Operation.PERCENT;
            else return (int)Operation.NONE;
        }
        private void refreshDisplay(int state)
        {
            if (state == (int)State.NUMBER1)
            {
                textbox_display.Text = value1;
            }
            else if (state == (int)State.NUMBER2)
            {
                //if (value2 == "0")
                //{
                //    textbox_display.Text = value1 + " " + sign;
                //}
                //else
                //{
                //    textbox_display.Text = value1 + " " + sign + " " + value2;
                //}
                textbox_display.Text = value1 + " " + sign + " " + value2;
            }
        }

        private double calculate()
        {
            number2 = Double.Parse(value2);

            switch (operation)
            {
                case (int)Operation.NONE:
                    return 0;

                case (int)Operation.ADD:
                    return number1 + number2;

                case (int)Operation.SUBTRACT:
                    return number1 - number2;

                case (int)Operation.MULTIPLY:
                    return number1 * number2;

                case (int)Operation.DIVIDE:
                    return number1 / number2;

                case (int)Operation.PERCENT:
                    return (number2 / 100) * number1;

                default:
                    return 0;
            }
        }


        private void  Number_Buttons_Click(object sender, RoutedEventArgs e)
        {
            string but_number = ((Button)sender).Content.ToString();
            
            if(counter<12) 
            {
                if(state==(int)State.NUMBER1)
                {
                    if(textbox_display.Text=="0")
                    {
                        value1 = but_number;
                    }
                    else
                    {
                        value1 += but_number;
                        counter++;
                    }

                    if(Double.Parse(value1)>0)
                    {
                        but0.IsEnabled = true;
                    }
                }

                else if (state == (int)State.NUMBER2)
                {
                    if (value2 == "0")
                    {
                        value2 = but_number;
                    }
                    else
                    {
                        value2 += but_number;
                        counter++;
                    }

                    if (Double.Parse(value2) > 0)
                    {
                        but0.IsEnabled = true;
                    }

                    if (Double.Parse(value2) == 0 && operation == (int)Operation.DIVIDE)
                    {
                        but_result.IsEnabled = false;
                        but_plus.IsEnabled = false;
                        but_minus.IsEnabled = false;
                        but_mul.IsEnabled = false;
                        but_div.IsEnabled = false;
                        but_percent.IsEnabled = false;
                    }
                    else
                    {
                        but_result.IsEnabled = true;
                        but_plus.IsEnabled = true;
                        but_minus.IsEnabled = true;
                        but_mul.IsEnabled = true;
                        but_div.IsEnabled = true;
                        but_percent.IsEnabled = true;
                    }
                }
                refreshDisplay(state);
            }

            if (counter == 12)
            {
                but_point.IsEnabled = false;
            }
        }


        private void Backspace_Button_Click(object sender, RoutedEventArgs e)
        {
            int numbers_left;

            if (isMinus)
            {
                numbers_left = 2;
            }
            else
            {
                numbers_left = 1;
            }

            if (state == (int)State.NUMBER1)
            {
                if (value1.Length > numbers_left)
                {
                    value1 = value1.Remove(value1.Length - 1);
                    counter--;
                    if (!value1.Contains(","))
                    {
                        isPoint = false;
                        but_point.IsEnabled = true;
                    }
                }
                else
                {
                    value1 = "0";
                    but0.IsEnabled = false;
                    isMinus = false;
                }
            }
            else if (state == (int)State.NUMBER2)
            {
                if (value2.Length > numbers_left)
                {
                    value2 = value2.Remove(value2.Length - 1);
                    counter--;
                    if (!value2.Contains(","))
                    {
                        isPoint = false;
                        but_point.IsEnabled = true;
                    }
                }
                else
                {
                    value2 = "0";
                    but0.IsEnabled = false;
                    isMinus = false;
                }

                if (Double.Parse(value2) == 0 && operation == (int)Operation.DIVIDE)
                {
                    but_result.IsEnabled = false;
                    but_plus.IsEnabled = false;
                    but_minus.IsEnabled = false;
                    but_mul.IsEnabled = false;
                    but_div.IsEnabled = false;
                    but_percent.IsEnabled = false;
                }
                else
                {
                    but_result.IsEnabled = true;
                    but_plus.IsEnabled = true;
                    but_minus.IsEnabled = true;
                    but_mul.IsEnabled = true;
                    but_div.IsEnabled = true;
                    but_percent.IsEnabled = true;
                }
            }
            refreshDisplay(state);
        }

        private void Point_Button_Click(object sender, RoutedEventArgs e)
        {
            if (state == (int)State.NUMBER1 && !isPoint)
            {
                value1 += ",";
                counter++;
                isPoint = true;
                but_point.IsEnabled = false;
                but0.IsEnabled = true;
            }
            else if (state == (int)State.NUMBER2 && !isPoint)
            {
                value2 += ",";
                counter++;
                isPoint = true;
                but_point.IsEnabled = false;
                but0.IsEnabled = true;
            }
            refreshDisplay(state);
        }

        private void C_Button_Click(object sender, RoutedEventArgs e)
        {
            isPoint = false;
            isMinus = false;
            counter = 1;
            but_point.IsEnabled = true;
            but0.IsEnabled = false;
            value1 = "0";
            value2 = "0";
            state = (int)State.NUMBER1;
            if (operation == (int)Operation.DIVIDE)
            {
                but_result.IsEnabled = true;
                but_plus.IsEnabled = true;
                but_minus.IsEnabled = true;
                but_mul.IsEnabled = true;
                but_div.IsEnabled = true;
                but_percent.IsEnabled = true;
                operation = (int)Operation.NONE;
            }
            else
            {
                operation = (int)Operation.NONE;
            }
            refreshDisplay(state);
        }

        private void Plusminus_Button_Click(object sender, RoutedEventArgs e)
        {
            if (state == (int)State.NUMBER1)
            {
                if (Double.Parse(value1) != 0)
                {
                    if (!isMinus)
                    {
                        value1 = "-" + value1;
                        isMinus = true;
                    }
                    else
                    {
                        value1 = value1.Remove(0, 1);
                        isMinus = false;
                    }
                }
            }
            else if (state == (int)State.NUMBER2)
            {
                if (Double.Parse(value2) != 0)
                {
                    if (!isMinus)
                    {
                        value2 = "-" + value2;
                        isMinus = true;
                    }
                    else
                    {
                        value2 = value2.Remove(0, 1);
                        isMinus = false;
                    }
                }
            }
            refreshDisplay(state);
        }


        private void Operation_Button_Click(object sender, RoutedEventArgs e)
        {
            string but_val = ((Button)sender).Content.ToString();
            sign = but_val;
            but_point.IsEnabled = true;

            if (state == (int)State.NUMBER1)
            {
                number1 = Double.Parse(value1);
                value1 = number1.ToString();
                state = (int)State.NUMBER2;
                refreshDisplay(state);
                counter = 1;
                isMinus = false;
                isPoint = false;
            }
            else if (state == (int)State.NUMBER2)
            {
                result = calculate();
                value1 = result.ToString();
                value2 = "0";
                number1 = result;
                number2 = 0;
                refreshDisplay(state);
            }

            operation = set_operation(but_val);

            if (operation == (int)Operation.DIVIDE)
            {
                but_result.IsEnabled = false;
                but_plus.IsEnabled = false;
                but_minus.IsEnabled = false;
                but_mul.IsEnabled = false;
                but_div.IsEnabled = false;
                but_percent.IsEnabled = false;
            }
            click_counter++;

            if (click_counter > 1) 
            but0.IsEnabled = false;
        }

        private void Result_Button_Click(object sender, RoutedEventArgs e)
        {
            click_counter = 0;
            if (operation != (int)Operation.NONE)
            {
                result = calculate();
                value1 = result.ToString();
                value2 = "0";
                number1 = 0;
                number2 = 0;
                counter = value1.Length;
                operation = (int)Operation.NONE;
                state = (int)State.NUMBER1;
                refreshDisplay(state);

                if (result == 0)
                {
                    but0.IsEnabled = false;
                }
                else
                {
                    but0.IsEnabled = true;
                }
            }
        }
    }
}
