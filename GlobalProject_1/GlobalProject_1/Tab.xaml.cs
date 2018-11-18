using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace GlobalProject_1
{
    /// <summary>
    /// Логика взаимодействия для Tab.xaml
    /// </summary>
    public partial class Tab : Window
    {
        private static string ConnStr = @"SslMode=none;
                                        server=81.200.119.82;
                                        port=3306;                                      
                                        username=student;
                                        password=Student!@#;
                                        database=Juravlini"; //Строка подключения к серверу
        private static MySqlConnection Conn = new MySqlConnection(ConnStr); // создаём объект для подключения к БД

        public Tab()
        {
            InitializeComponent();
        }
        private void Aut(object sender, RoutedEventArgs e)
        {
            if (CheckBox.IsChecked == true)
            {
                Frame_Page.Content = null;
                
                if(Tb_Login.Text != "" && Tb_Pass.Text != "")
                { // Процедура
                  // устанавливаем соединение с БД
                    Conn.Open();

                    // объект для выполнения SQL-запроса
                    MySqlCommand command = new MySqlCommand($"SELECT 'Accept' FROM Aut where login = '{Tb_Login.Text}' and pass = '{Tb_Pass.Text}'", Conn);

                    // объект для чтения ответа сервера
                  // MySqlDataReader reader = command.ExecuteReader();

                    // выполняем запрос и получаем ответ
                    string name = "";
                    try
                    {
                        name = command.ExecuteScalar().ToString();
                        Lab_Log.Content = "Login: " + Tb_Login.Text;
                        Lab_status.Content = "Status: Accept";
                        Lab_status.Background = Brushes.Green;
                        Frame_Page.Content = new Page1_Table();
                    }
                    catch
                    {
                        Lab_Log.Content = "Login: " + Tb_Login.Text;
                        Lab_status.Content = "Status: Bad Hacker";
                        Lab_status.Background = Brushes.Red;
                    }
                   
                    // закрываем соединение с БД
                    Conn.Close();
                    Conn.Dispose(); // освободить ресурс
                   
                }
                else
                {
                    Lab_Log.Content = "Login: " + Tb_Login.Text;
                    Lab_status.Content = "Status: Bad Hacker";
                    Lab_status.Background = Brushes.Red;
                }
                //Into_Log();
            } // Pro режим
            else
            {
                    //Into_Log();
                    Frame_Page.Content = new Page1_Table();     
            }
        }
        private void Into_Log()
        {
            try // запись в лог ДЕЛАТЬ
            {
                Conn.Open();

                MySqlCommand command = new MySqlCommand("call Proc_1", Conn)
                {
                    // указываем, что команда представляет хранимую процедуру
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                //текст ответа
                MySqlParameter contentParam = new MySqlParameter
                {
                    ParameterName = "@login",
                    Value = "1",
                    MySqlDbType = MySqlDbType.VarChar
                };
                command.Parameters.Add(contentParam);

                contentParam = new MySqlParameter
                {
                    ParameterName = "@pass",
                    Value = "1",
                    MySqlDbType = MySqlDbType.VarChar
                };
                command.Parameters.Add(contentParam);

                // закрываем соединение с БД
                Conn.Close();
                Conn.Dispose(); // освободить ресурс

                //Frame_Page.Content = new Page1_Table();  
            }
            catch
            {
                MessageBox.Show("oops... Server AFK");
            }
        }
  
    }
}
