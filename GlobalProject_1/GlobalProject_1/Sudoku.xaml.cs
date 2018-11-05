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
using System.Text.RegularExpressions;
namespace GlobalProject_1
{
    /// <summary>
    /// Логика взаимодействия для Sudoku.xaml
    /// </summary>
    public partial class Sudoku : Window
    {
        List<Border> BordersList = new List<Border>(); // форма бордер
        List<Grid> GridsList = new List<Grid>(); // форма бордер грид

        
        TextBox[,] MasText = new TextBox[9, 9]; // лог массив  

        public Sudoku()
        {
            InitializeComponent();
            SET();
        }

        private void SET()
        {
            //Grid_Game.ShowGridLines = true;
            for (int i = 0; i < 9; i++)
            {
                Grid_Game.RowDefinitions.Add(new RowDefinition());
                Grid_Game.ColumnDefinitions.Add(new ColumnDefinition());

            } // GRID


            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    Border b = new Border() {
                        BorderThickness = new Thickness(2),
                        BorderBrush = Brushes.Black,
                    }; 

                    Grid.SetRow(b, i); // на грид
                    Grid.SetRowSpan(b, 3);

                    Grid.SetColumn(b, j); // на грид
                    Grid.SetColumnSpan(b, 3);

                    Grid_Game.Children.Add(b);

                    BordersList.Add(b);
                }
            } // BORDER рамка 3 на 3


            for (int i = 1; i <= 9; i++)
            {
                Grid gr = new Grid();
                for (int j = 0; j < 3; j++)
                {
                    gr.RowDefinitions.Add(new RowDefinition());
                    gr.ColumnDefinitions.Add(new ColumnDefinition());

                }
                gr.ShowGridLines = true;
                BordersList[i - 1].Child = gr;
                GridsList.Add(gr);
            }  // BORODER GRID. Грид в бордер

          
            int str = 0, stol = 0;
            for (int glob = 0; glob < GridsList.Count; glob++)
            {
                int temp = 1;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {

                        TextBox b = new TextBox()
                        {
                            Text = temp.ToString(),
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            FontSize = 24,
                            MaxLength = 1
                        };
                        b.PreviewTextInput += new TextCompositionEventHandler(TextBox_PreviewTextInput);
                        temp++;
                        Grid.SetRow(b, i); // на грид
                        Grid.SetColumn(b, j); // на грид

                        //TextList.Add(b); // в список
                        MasText[i + str, j + stol] = b;

                        GridsList[glob].Children.Add(b);

                    }
                }
                stol+=3; temp = 0;
                if (stol == 9)
                {
                    stol = 0; str += 3;
                }
            }  // BORDER GRID TEXTBOX

        
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
                Regex regex = new Regex("[^1-9]+"); // регулярка на доступные чиста
                e.Handled = regex.IsMatch(e.Text);  // установка текста
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int str = 0, stol = 0;
            for (int glob = 0; glob < 9; glob++)
            {
                int temp = 1;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //MasText[i, j].Text = temp.ToString();
                        MasText[i + str, j + stol].Text = temp.ToString();
                        temp++;
                    }
                }
                stol += 3; temp = 0;
                if (stol == 9)
                {
                    stol = 0; str += 3;
                }
            }  

            Random rand = new Random();
            int _i = 0;
            
            while(_i < 3)
            {
                int i = rand.Next(0, 9);
                if(MasText[i, i].Text != " ")
                {
                    MasText[i, i].Text = " ";
                    _i++;
                }           
            } // Левая  Г Д
            _i = 0;
            while (_i < 3)
            {
                int i = rand.Next(0, 9);
                if (MasText[i, 8-i].Text != " ")
                {
                    MasText[i, 8-i].Text = " ";
                    _i++;
                }
            } // правая Г Д

            _i = 0;
            while (_i < 3) // диагональ 1, 2, 3, 4. Боковые от М
            {
                int i = rand.Next(0, 8);

                //ок
                MasText[i, i + 1].Text = " "; //левая + 1
                MasText[i + 1, i].Text = " "; //левая - 1 

                //ок
                MasText[i + 1, 8 - i].Text = " "; //правая + 1
                MasText[i, 7 - i].Text = " "; //правая - 1


                //ок
                i = rand.Next(0, 7);
                MasText[i, i + 2].Text = " "; //левая + 2
                MasText[i + 2, i].Text = " "; //левая - 2

                //ок
                MasText[i, 6 - i].Text = " "; //правая - 2
                MasText[i + 2, 8 - i].Text = " "; //правая + 2

                _i++;
            } // 1й и 3й квадрат - диагонали

            _i = 0;
            while (_i < 2)
            {
                int i = rand.Next(0, 6);
                MasText[i, 5 - i].Text = " "; // главная диагональ
                MasText[i + 1, 5 - i].Text = " "; // + 1
                MasText[i + 2, 5 - i].Text = " "; // + 2

                i = rand.Next(0, 5);
                MasText[i, 4 - i].Text = " "; //  - 1 
                i = rand.Next(0, 4);
                MasText[i, 3 - i].Text = " "; //  - 2 
                
                             
                _i++;
            } // 2й квадрат диагонали

            _i = 0;
            while (_i < 2)
            {
                int i = rand.Next(3, 9);
                MasText[i, 8 - i + 3].Text = " "; // главная диагональ

                MasText[i, 8 - i + 2].Text = " "; // - 1
                MasText[i, 8 - i + 1].Text = " "; // - 2

                i = rand.Next(3 + 1, 9);
                MasText[i, 8 - i + 4].Text = " "; //  + 1 
                i = rand.Next(3 + 2, 9);
                MasText[i, 8 - i + 5].Text = " "; //  + 2 

                _i++;
            }// 6й квадрат диагонали
        }
    }
}
