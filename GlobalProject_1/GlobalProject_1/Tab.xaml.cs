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
                Into_Log();
            } // Pro режим
            else
            {
                
                Frame_Page.Content = new Page1_Table();
                
            }
        } // оптимизировать!
        private void Into_Log()
        {
            try
            {

                Conn.Open();

                MySqlCommand command = new MySqlCommand("Proc_Into_Log", Conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                MySqlParameter p = new MySqlParameter();
                command.Parameters.AddWithValue("s_datatime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("s_log", Tb_Login.Text);
                command.Parameters.AddWithValue("s_pass", Tb_Pass.Text);
                if(Lab_status.Content.ToString() == "Status: Accept")        
                    command.Parameters.AddWithValue("s_stat", 1);
                else
                    command.Parameters.AddWithValue("s_stat", 0);

                command.ExecuteNonQuery();

                // закрываем соединение с БД
                Conn.Close();
                Conn.Dispose(); // освободить ресурс
            }
            catch
            {
                MessageBox.Show("ooops... Server AFK");
            }

        }
        
    }
}
