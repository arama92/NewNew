using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GlobalProject_1
{
    /// <summary>
    /// Логика взаимодействия для Calculator_1.xaml
    /// </summary>
    public partial class Calculator_1 : Window
    {

        private string Symbol = ""; // Знак действия
        private bool Flag = false;  // Для открытия и закрытия знаков

        public Calculator_1()
        {
            InitializeComponent();
            All_But_Off(); // Выключить символя для исключения ошибок
            Button_Koren.IsEnabled = false; 
        }

        private void All_But_Off() // Выключить кнопки
        {
            List<Button> buts = new List<Button>() {Button_plus, Button_monus, Button_del,
                                                    Button_pow, Button_poww,  Button_Plus_Minus};
            for (int i = 0; i < buts.Count; i++)
            {
                buts[i].IsEnabled = false;
            }
        }

        private void All_But_On() // Включить кнопки
        {
            List<Button> buts = new List<Button>() {Button_plus, Button_monus, Button_del,
                                                    Button_pow, Button_poww,  Button_Plus_Minus};
            for (int i = 0; i < buts.Count; i++)
            {
                buts[i].IsEnabled = true;
            }
        }

        private void Button_Click_Get_Text(object sender, RoutedEventArgs e) // получение текста и ответа
        {
            // Получаем текст кнопки
            string s = (string)((Button)e.OriginalSource).Content;


            if (s == "=") { } // не печатать ровно
            else
            {
                // Добавляем его в текстовое поле
                Label_Pole.Text += s;
            }
            if (Flag == false) // Открытие кнопок
            {
                All_But_On();
                Button_Koren.IsEnabled = true;
                Flag = true;
            }

            // убирать повторную точку
            if (s == ",")
            {
                Button_Dot.IsEnabled = false;
            }
                    

            // убирать двойной символ
            if (s == "+" || s == "-" || s == "*" || s == "/" || s == "^")
            {
                Symbol = s; // Установка знака
                All_But_Off();  // отключить символы для исключния ошибок
                Button_Otvet.IsEnabled = true; // активировать кнопку Ровно
                Button_Dot.IsEnabled = true; // активировать кнопку Точка
                Button_Koren.IsEnabled = false; // Выключить кнопку Корень
            }
            // формировка ответа
            if (s == "=")
            {

                string[] temp = Label_Pole.Text.Split(new char[] { '+', '-', '/', '*', '^' }); // Разбиваем на числа строку
                if (temp.Length == 1) // Проверка на правильность строки
                    Label_Pole.Text = temp[0];
                else
                {
                    if (temp[1] == "") // Проверка на правильность строки
                        Label_Pole.Text = temp[0];
                    else
                    {
                        if (temp[0] == "," || temp[1] == ",") // Проверка на правильность строки
                            Label_Pole.Text = "Error --> Clear";
                        else
                            // Формирование ответа при правильной строке
                            if (Symbol == "+")
                            Label_Pole.Text = Convert.ToString(Convert.ToDouble(temp[0]) + Convert.ToDouble(temp[1]));
                        else if (Symbol == "-")
                            Label_Pole.Text = Convert.ToString(Convert.ToDouble(temp[0]) - Convert.ToDouble(temp[1]));
                        else if (Symbol == "*")
                            Label_Pole.Text = Convert.ToString(Convert.ToDouble(temp[0]) * Convert.ToDouble(temp[1]));
                        else if (Symbol == "/")
                            Label_Pole.Text = Convert.ToString(Convert.ToDouble(temp[0]) / Convert.ToDouble(temp[1]));
                        else if (Symbol == "^")
                            Label_Pole.Text = Convert.ToString(Math.Pow(Convert.ToDouble(temp[0]), Convert.ToDouble(temp[1])));
                    }

                }

                All_But_Off();
                Button_Dot.IsEnabled = false;
                Button_Otvet.IsEnabled = false;
                Button_Koren.IsEnabled = true;


            }


        }

        private void Button_Click_Get_Koren(object sender, RoutedEventArgs e) // Высчитывание корня
        {
            double temp = Math.Sqrt(Convert.ToDouble(Label_Pole.Text));
            Label_Pole.Text = Convert.ToString(temp);


        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e) // Очитить поле (вернуть всё как при старте)
        {
            Label_Pole.Text = "";
            Button_Dot.IsEnabled = true;
            All_But_Off();
            Button_Koren.IsEnabled = false;
            Flag = false;
        }

        private void Button_Click_RightClear(object sender, RoutedEventArgs e) // стереть только последний символ
        {
            if (Label_Pole.Text.Length != 0)
                Label_Pole.Text = Label_Pole.Text.Substring(0, Label_Pole.Text.Length - 1);
            if (Label_Pole.Text.Length == 0)
            {
                Button_Dot.IsEnabled = true;
                All_But_Off();
                Button_Koren.IsEnabled = false;
                Flag = false;
            }
        }

        private void Button_Click_PLus_Minus(object sender, RoutedEventArgs e) // Кнопка Плюс-Минус. Меняем знак числа
        {
            string s = Label_Pole.Text;
            if (s.Length != 0)
            {
                double temp = -1 * Convert.ToDouble(s);
                Label_Pole.Text = Convert.ToString(temp);
            }

        }
    }
}
