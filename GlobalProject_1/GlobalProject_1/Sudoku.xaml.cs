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
    /// Логика взаимодействия для Sudoku.xaml
    /// </summary>
    public partial class Sudoku : Window
    {
        List<Border> BordersList = new List<Border>(); // форма бордер
        List<Grid> GridsList = new List<Grid>(); // форма бордер грид

        List<TextBox> TextList = new List<TextBox>();
        TextBox[,] MasText = new TextBox[9, 9]; // лог массив 
        //int[,] MasText = new int[9, 9];

        public Sudoku()
        {
            InitializeComponent();
            SET();
            Conver_List_To_Mas();
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
            } // BORDER


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
            }  // BRODER GRID

            int temp = 1;
            for (int glob = 0; glob < GridsList.Count; glob++)
            {

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {

                        TextBox b = new TextBox()
                        {
                            Text = temp.ToString(),
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            FontSize = 24
                        };
                        temp++;
                        Grid.SetRow(b, i); // на грид
                        Grid.SetColumn(b, j); // на грид

                        TextList.Add(b); // в список

                        GridsList[glob].Children.Add(b);

                    }
                }
            }  // BORDER GRID TEXTBOX

        }

        private void Conver_List_To_Mas()
        {/*
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    MasText[i, j] = TextList[0];
                }
            }*/
            
            
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[,] mas = new int[3, 3];


            int temp = 0;
            for(int i = 0; i < 9; i+=3)
            {
                for(int j = 0; j < 9; j++)
                {
                    MasText[i, j] = TextList[temp];
                    temp++;
                    //MessageBox.Show(MasText[i, j].Text);
                    if (j == 2 || j == 5 || j==8)
                    {
                        temp += 6;
                    }
                }   
            }
            /*
            temp = 0;
            for (int i = 1; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j++)
                {
                    MasText[i, j] = TextList[temp];
                    temp++;
                    MessageBox.Show(MasText[i, j].Text);
                    if (j == 0)
                    {
                        temp += 6;
                    }
                }
            }*/

            /*temp = 0;
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    MessageBox.Show(list[temp].ToString());
                    mas[i, j] = list[temp];
                    temp += 3;
                }
                temp -= 5;
            }*/

            StreamWriter sr = new StreamWriter("Sample.txt");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sr.Write(mas[i, j].ToString() + " ");
                }
                sr.WriteLine("");
            }
           
    
        }

        
    }
}
