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
using System.Windows.Shapes;

namespace GlobalProject_1
{
    /// <summary>
    /// Логика взаимодействия для GameIn15.xaml
    /// </summary>
    public partial class GameIn15 : Window
    {
        List<Button> ListBut = new List<Button>();
        int t = 128 * 2;
        int[,] Pole = new int[6,6];

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public GameIn15()  // ОК
        {
            InitializeComponent();

            for (int i = 1; i <= 16; i++)
            {
                //string s = "../../Resources/" + Convert.ToString(i) + ".png"; // динамически сделать
                string s = "BG_15_" + Convert.ToString(i) + "_BIB"; // динамически
                /*
                ImageBrush imBr = new ImageBrush();
                imBr.ImageSource = new BitmapImage(new Uri(s, UriKind.Relative));*/
                Button btn = new Button() {Content = Convert.ToString(i)};

                btn.Width = 128;
                btn.Height = 128;

                btn.FontSize = 1;
                //btn.SetValue(Canvas.LeftProperty, (double)(i * 50));
                //btn.SetValue(Canvas.TopProperty, (double)(i * 64));
                //btn.Background = imBr; // YES
                btn.SetResourceReference(StackPanel.BackgroundProperty, s);

                btn.Click += new RoutedEventHandler(Butpp);
               
                ListBut.Add(btn);
                Main_Grid.Children.Add(btn);
            }
            Set_Pos();

        }

        private void Rand(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            for (int i = ListBut.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = ListBut[j];
                ListBut[j] = ListBut[i];
                ListBut[i] = temp;
            }
            Set_Pos();
        } // ОК

        private void Set_Pos()
        {
            ListBut[0].Margin = new Thickness(left: 0, top: 0, right: t * 1.5, bottom: t * 1.5);
            ListBut[1].Margin = new Thickness(left: 0, top: 0, right: t / 2, bottom: t * 1.5);
            ListBut[2].Margin = new Thickness(left: t / 2, top: 0, right: 0, bottom: t * 1.5);
            ListBut[3].Margin = new Thickness(left: t * 1.5, top: 0, right: 0, bottom: t * 1.5);

            ListBut[4].Margin = new Thickness(left: 0, top: 0, right: t * 1.5, bottom: t / 2);
            ListBut[5].Margin = new Thickness(left: 0, top: 0, right: t / 2, bottom: t / 2);
            ListBut[6].Margin = new Thickness(left: t / 2, top: 0, right: 0, bottom: t / 2);
            ListBut[7].Margin = new Thickness(left: t * 1.5, top: 0, right: 0, bottom: t / 2);

            ListBut[8].Margin = new Thickness(left: 0, top: t / 2, right: t * 1.5, bottom: 0);
            ListBut[9].Margin = new Thickness(left: 0, top: t / 2, right: t / 2, bottom: 0);
            ListBut[10].Margin = new Thickness(left: t / 2, top: t / 2, right: 0, bottom: 0);
            ListBut[11].Margin = new Thickness(left: t * 1.5, top: t / 2, right: 0, bottom: 0);

            ListBut[12].Margin = new Thickness(left: 0, top: t * 1.5, right: t * 1.5, bottom: 0);
            ListBut[13].Margin = new Thickness(left: 0, top: t * 1.5, right: t / 2, bottom: 0);
            ListBut[14].Margin = new Thickness(left: t / 2, top: t * 1.5, right: 0, bottom: 0);
            ListBut[15].Margin = new Thickness(left: t * 1.5, top: t * 1.5, right: 0, bottom: 0);
        } // ОК
        private void Set_Pole() // 2мерный масив тип логика
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    Pole[i, j] = -1;

           
            int temp = 0;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    Pole[i, j] = Convert.ToInt32(ListBut[temp].Content);
                    temp++;     
                }
            }
        } // ОК
        private void Set_content()
        {
            int temp = 0;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    ListBut[temp].Content = Convert.ToString(Pole[i, j]);
                    temp++;
                }
            }
        } // OK
        private void Set_Image()
        {
            for (int i = 0; i < ListBut.Count; i++)
            {
                string s = "BG_15_" + Convert.ToString(ListBut[i].Content) + "_BIB"; // динамически
                ListBut[i].SetResourceReference(StackPanel.BackgroundProperty, s);
            }
        } // ok

        private void Butpp(object sender, RoutedEventArgs e)
        {
            string stemp = (string)((Button)e.OriginalSource).Content;
            Set_Pole();

            for (int i = 1; i < 5; i++)
            {
                for(int j = 1; j < 5; j++)
                {
                    if (Pole[i, j] == Convert.ToInt32(stemp))
                    {
                        if (Pole[i + 1, j] == 16) // Up
                            Swap(ref Pole[i, j], ref Pole[i + 1, j]);
                        else if(Pole[i, j - 1] == 16)
                            Swap(ref Pole[i, j], ref Pole[i , j - 1]);
                        else if(Pole[i, j + 1] == 16)
                            Swap(ref Pole[i, j], ref Pole[i , j + 1]);
                        else if (Pole[i - 1, j] == 16) // bot
                            Swap(ref Pole[i, j], ref Pole[i - 1, j]);
                           
                        goto exit_label;
                    }
                }
            }
            exit_label:

            //Print();
            Set_content();
            Set_Pos();
            Set_Image();
            Check_Win();

        } //ok
        private void Check_Win()
        {
            bool temp = true;
            for(int i = 1; i < ListBut.Count; i++)
            {
                if (Convert.ToInt16(ListBut[i].Content) > Convert.ToInt16(ListBut[i - 1].Content))
                    temp = true;
                else
                    temp = false; 

            }
            if (temp)
            {
                WinWindow proc = new WinWindow();
                proc.Show();
            }
            
        }


        private void Print()
        {
            StreamWriter sr = new StreamWriter("Sample.txt"); 
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    sr.Write(Pole[i, j] + " ");
                }
                sr.WriteLine("");
            }
            sr.Close(); 
        } // ok


    }

}
