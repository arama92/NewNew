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
using ELW.Library.Math;
using ELW.Library.Math.Calculators;
using ELW.Library.Math.Exceptions;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;

namespace GlobalProject_1
{
    /// <summary>
    /// Логика взаимодействия для Сalculator_2.xaml
    /// </summary>
    public partial class Сalculator_2 : Window
    {

        private string Pole = "";

        public Сalculator_2()
        {
            InitializeComponent();
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            TextBox_In.Text = "";
            TextBox_Resul.Text = "";
        }


        private void Button_Enter(object sender, RoutedEventArgs e)
        {
            try
            {
                PreparedExpression preparedExpression = ToolsHelper.Parser.Parse(TextBox_In.Text);
                CompiledExpression compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
                List<VariableValue> variables = new List<VariableValue>();
                double res = ToolsHelper.Calculator.Calculate(compiledExpression, variables);
                TextBox_Resul.Text = String.Format("Результат: {0}", res);
            }
            catch (CompilerSyntaxException ex)
            {
                TextBox_Resul.Text = String.Format("Ошибка: {0}", ex.Message);
                
            }
            catch (MathProcessorException ex)
            {
                TextBox_Resul.Text = String.Format("Ошибка: {0}", ex.Message);
            }
            catch (ArgumentException)
            {
                TextBox_Resul.Text = "Ошибка";
            }
            
        }
    }
}
