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
using MySql.Data.MySqlClient;
using System.Data;

namespace GlobalProject_1
{
    /// <summary>
    /// Логика взаимодействия для Page1_Table.xaml
    /// </summary>
    public partial class Page1_Table : Page
    {
        private static string ConnStr = @"SslMode=none;
                                        server=81.200.119.82;
                                        port=3306;                                      
                                        username=student;
                                        password=Student!@#;
                                        database=Juravlini"; //Строка подключения к серверу
        private static MySqlConnection Conn = new MySqlConnection(ConnStr); // создаём объект для подключения к БД

        public Page1_Table()
        {
            InitializeComponent();

            Conn.Open();
            MySqlCommand command = new MySqlCommand("show tables", Conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Cb1.Items.Add(reader[0].ToString());
            }
            
            Conn.Close();
            Conn.Dispose(); 
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboItem = (string)Cb1.SelectedValue;
            //MessageBox.Show(comboItem.Content.ToString());

            Conn.Open();
            
            MySqlCommand command = new MySqlCommand($"SELECT * FROM {comboItem}", Conn);
            DataTable DT = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(command);

            da.Fill(DT);
            datagrid.DataContext = DT;

            // закрываем соединение с БД
            Conn.Close();
            Conn.Dispose(); // освободить ресурс
        }
    }
}
