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

        List<TextBox> TextList = new List<TextBox>();
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
    }
}
