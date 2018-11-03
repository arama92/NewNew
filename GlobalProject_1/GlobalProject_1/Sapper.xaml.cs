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
    /// Логика взаимодействия для Sapper.xaml
    /// </summary>
    public partial class Sapper : Window
    {
        List<Button> ListBut = new List<Button>();
        Button[,] BPole = new Button[12, 12];
        int[,] Pole = new int[12, 12]; // минус
        int CMina = 0;
        int fine = 0;
        int Notfine = 0;
        public Sapper()
        {
            InitializeComponent();
            Set_Full_Pole();
        }

        private void Set_Full_Pole()
        {
             CMina = 0;
             fine = 0;
             Notfine = 0;
            for (int i = 1; i < 11;i++)
            {
                Label MyControl1 = new Label() { Content = i.ToString()};
               
                MyControl1.FontSize = 28;
                MyControl1.VerticalAlignment = VerticalAlignment.Stretch;
                MyControl1.HorizontalAlignment = HorizontalAlignment.Stretch;

                MyControl1.HorizontalContentAlignment = HorizontalAlignment.Center;
                MyControl1.VerticalContentAlignment = VerticalAlignment.Center;

                MyControl1.Background = Brushes.Red;

                Grid.SetColumn(MyControl1, i);
                Main_Grid_Game.Children.Add(MyControl1);
            } // Label

            int beg = 64;// первый символ
            for (int i = 1; i < 11; i++)
            {
                Label MyControl1 = new Label() { Content = ((char)(i + beg)).ToString() };

                MyControl1.FontSize = 28;
                MyControl1.VerticalAlignment = VerticalAlignment.Stretch;
                MyControl1.HorizontalAlignment = HorizontalAlignment.Stretch;

                MyControl1.HorizontalContentAlignment = HorizontalAlignment.Center;
                MyControl1.VerticalContentAlignment = VerticalAlignment.Center;

                MyControl1.Background = Brushes.Red;

                Grid.SetRow(MyControl1, i);
                Main_Grid_Game.Children.Add(MyControl1);
            } // Label

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Button but = new Button()
                    {
                        Content = i.ToString() + " " + j.ToString(),
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        FontSize = 24
                    };
                    if (i == 0 || j == 0 || i == 11 || j == 11) { BPole[i, j] = but; }
                    else
                    {
                        but.Click += new RoutedEventHandler(Click_Left_But);
                        but.MouseRightButtonUp += new MouseButtonEventHandler(Button_MouseRightButtonUp);

                        

                        Grid.SetColumn(but, j); // на грид
                        Grid.SetRow(but, i); // на грид
                        BPole[i, j] = but; //в массив
                        Main_Grid_Game.Children.Add(but);
                    }
                        
                }

            } // Button


        } // ok ~ image
        private void Set_Pole_Game()
        {
            Set_Full_Pole(); // обнулить возможные картинки
            int[] mas = new int[5] { 10, 15, 20, 25, 30 };
            Random random = new Random();
            int  Count_Mina = random.Next(5);
            int Cc = 0;

            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    BPole[i, j].Content = "0";
                    BPole[i, j].FontSize = 24; // Размер в игре
                }
            }
            CMina = mas[Count_Mina];
            while (Cc < mas[Count_Mina])
            {
                int row = random.Next(11);
                int col = random.Next(11);
                if (row == 0 || col == 0) { }
                else
                {
                    if (BPole[row, col].Content.ToString() != "MM")
                    {
                        BPole[row, col].Content = "MM";
                        Cc++;
                    }
                }

            }
        }  // ok ~ image

        

        
        private void Print(int a)
        {
            StreamWriter sr = new StreamWriter("Sample.txt");
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    //string s = BPole[i, j].Content.ToString();
                    sr.Write(Pole[i,j] + "\t" );
                }
                sr.WriteLine("");
            }
            sr.Close();
        } // ok
        private void Print(int a, int b)
        {
            StreamWriter sr = new StreamWriter("Sample.txt");
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    //string s = BPole[i, j].Content.ToString();
                    sr.Write(BPole[i,j].Content.ToString() + "\t\t" );
                }
                sr.WriteLine("");
            }
            sr.Close();
        } // ok


        private void Click_Start(object sender, RoutedEventArgs e)
        { 
            Set_Pole_Game(); Print(1, 2); 
        }
        private void Click_Left_But(object sender, RoutedEventArgs e)
        {
            //Получаем текст кнопки
            string s = (string)((Button)e.OriginalSource).Content;
            if (s == "MM"){ MessageBox.Show("BOOM"); Set_Full_Pole(); }
            else
            {
                var btn = (Button)sender;

                int row = 0;
                int col = 0;

                //MessageBox.Show(btn.Content.ToString());
                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        if (btn == BPole[i, j])
                        {
                            row = i; col = j;
                            //MessageBox.Show(i.ToString() + " " + j.ToString());
                            break;
                        }
                    }
                }
              
                int ch = 0;
                if ("MM" == BPole[row - 1, col - 1].Content.ToString())
                    ch++;
                if ("MM" == BPole[row - 1, col].Content.ToString())
                    ch++;
                if ("MM" == BPole[row - 1, col + 1].Content.ToString())
                    ch++;
                if ("MM" == BPole[row, col - 1].Content.ToString())
                    ch++;
                if ("MM" == BPole[row, col + 1].Content.ToString())
                    ch++;
                if ("MM" == BPole[row + 1, col - 1].Content.ToString())
                    ch++;
                if ("MM" == BPole[row + 1, col].Content.ToString())
                    ch++;
                if ("MM" == BPole[row + 1, col + 1].Content.ToString())
                    ch++;
                BPole[row, col].FontSize = 28;
                BPole[row, col].Content = ch.ToString(); // ~image
                //BPole[row, col].SetResourceReference(StackPanel.BackgroundProperty, "BG_7_Flag_BIB");
                if (Notfine == CMina && fine == 0)
                {
                    //MessageBox.Show("WIN");
                    WinWindow proc = new WinWindow();
                    

                    proc.Show();
                }
                    
            }
        }
        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e) // флажок придумать!
        {
            
            var btn = (Button)sender;
            string s = btn.Content.ToString();
            btn.SetResourceReference(StackPanel.BackgroundProperty, "BG_7_Flag_BIB");
            if (s != "MM")
                fine++;
            else if(s == "MM")
                Notfine++;
            if (Notfine == CMina && fine == 0)
            {   
                    WinWindow proc = new WinWindow();
                    proc.Show(); 
            }
        }
    }
}
