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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GlobalProject_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Plan Prog = new Plan();
            Prog.Show();
            //this.Close();

        }

        private void Button_Click_Start_Calculator1(object sender, RoutedEventArgs e)
        { 
            Calculator_1 Prog = new Calculator_1();
            Prog.Show();
            //this.Close();
        }
        private void Button_Click_Start_Calculator2(object sender, RoutedEventArgs e)
        {
            Сalculator_2 Prog = new Сalculator_2();
            Prog.Show();
            //this.Close();
        }
        private void Button_Click_Start_Sudoku(object sender, RoutedEventArgs e)
        {
            Sudoku Prog = new Sudoku();
            Prog.Show();
            //this.Close();
        }
        private void Button_Click_Start_GameIn15(object sender, RoutedEventArgs e)
        {
            GameIn15 Prog = new GameIn15();
            Prog.Show();
            //this.Close();
        }
        private void Button_Click_Start_Plan(object sender, RoutedEventArgs e)
        {
 
            try
            {
                Plan Prog = new Plan();
                Prog.Show();
                //this.Close();
            }
            catch
            {
                System.IO.File.Delete(@"result.xml");
 
            }
            finally
            {
                Plan Prog = new Plan();
                Prog.Show();
            }
                        
        }

        private void Button_Click_Start_Sapper(object sender, RoutedEventArgs e)
        {
            Sapper Prog = new Sapper();
            Prog.Show();
            //this.Close();
        }

        private void Button_Click_Start_Tab(object sender, RoutedEventArgs e)
        {
            Tab Prog = new Tab();
            Prog.Show();
            //this.Close();
        }
    }
}
