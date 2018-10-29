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
using System.Xml;
using System.Timers;
using System.Windows.Threading;
using System.IO;

namespace GlobalProject_1
{

    /// <summary>
    /// Логика взаимодействия для Plan.xaml
    /// </summary>
    public partial class Plan : Window
    {
        public Plan()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            Label_date.Content = DateTime.Now.ToLongDateString();

            L_C_File_XML();
            Write_TXT();
        }

        private void Set_Data(object sender, SelectionChangedEventArgs e)
        {

            Tb1.Text = datePicker1.SelectedDate.Value.Date.ToShortDateString();
            //MessageBox.Show(Tb1.Text);

        }

        void timer_Tick(object sender, EventArgs e)
        {
            Label_time.Content = DateTime.Now.ToLongTimeString();
        }
 
        private void L_C_File_XML()
        {
            // Создаем новый Xml документ.
            XmlDocument xDoc = new XmlDocument();

            try // доделать
            {
                xDoc.Load("result.xml");
                // получим корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;
                // обход всех узлов в корневом элементе
                foreach (XmlNode xnode in xRoot)
                {
                    // получаем атрибут name
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("Number");
                        if (attr != null)
                        {
                            
                        }
                    }
                }

                //MessageBox.Show("File XML Load");
            }
            catch
            {
                // Создаем Xml заголовок.
                var xmlDeclaration = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

                // Добавляем заголовок перед корневым элементом.
                xDoc.AppendChild(xmlDeclaration);

                // Создаем Корневой элемент
                var root = xDoc.CreateElement("Catalog");

                // Добавляем новый корневой элемент в документ.
                xDoc.AppendChild(root);

                // Сохраняем документ.
                xDoc.Save("result.xml");

                //MessageBox.Show("File XML Create");
            }
            
        }

        private void Write_TXT()
        {

            StreamWriter sr = new StreamWriter("Sample.txt");

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("result.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибут name
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("Number");
                    if (attr != null)
                    {
                        sr.Write(attr.Value + " ");

                    }

                }
                var InnerText1 = "";
                var InnerText2 = "";
                var InnerText3 = "";
                // обходим все дочерние узлы элемента user
                foreach (XmlNode childnode in xnode.ChildNodes)
                {


                    // если узел - company
                    if (childnode.Name == "Data")
                    {
                        sr.Write("Data: {0} ", childnode.InnerText);
                        InnerText1 = childnode.InnerText;
                    }

                    // если узел age
                    if (childnode.Name == "Time")
                    {
                        sr.Write("Time: {0} ", childnode.InnerText);
                        InnerText2 = childnode.InnerText;
                    }
                    // если узел age
                    if (childnode.Name == "Delo")
                    {
                        sr.Write("Delo: {0} ", childnode.InnerText);
                        InnerText3 = childnode.InnerText;
                    }

                }
                DDBOrder(InnerText1, InnerText2, InnerText3);
                sr.Write("\r\n");


            }
            sr.Close();
        } // TO Txt  ----

        private void Add_In_XML(object sender, RoutedEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("result.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            // создаем новый элемент user
            XmlElement userElem = xDoc.CreateElement("Use");

            // создаем атрибут name
            XmlAttribute nameAttr1 = xDoc.CreateAttribute("Number");
            //XmlAttribute nameAttr2 = xDoc.CreateAttribute("Time");
            //XmlAttribute nameAttr3 = xDoc.CreateAttribute("Delo");

            // создаем текстовые значения для элементов и атрибута
            XmlText nameText1 = xDoc.CreateTextNode(Tb1.Text + " " + Tb2.Text);
           //XmlText nameText2 = xDoc.CreateTextNode("Время");
            //XmlText nameText3 = xDoc.CreateTextNode("Занятие");


            // создаем элементы 
            XmlElement DataElem = xDoc.CreateElement("Data");
            XmlElement TimeElem = xDoc.CreateElement("Time");
            XmlElement DeloElem = xDoc.CreateElement("Delo");

            XmlText DataText = xDoc.CreateTextNode(Tb1.Text);
            XmlText TimeText = xDoc.CreateTextNode(Tb2.Text);
            XmlText DeloText = xDoc.CreateTextNode(Tb3.Text);


            //добавляем узлы
            nameAttr1.AppendChild(nameText1);
            //nameAttr2.AppendChild(nameText2);
            //nameAttr3.AppendChild(nameText3);

            DataElem.AppendChild(DataText);
            TimeElem.AppendChild(TimeText);
            DeloElem.AppendChild(DeloText);

            /*                                   */
            userElem.Attributes.Append(nameAttr1);
            //userElem.Attributes.Append(nameAttr2);
            //userElem.Attributes.Append(nameAttr3);

            userElem.AppendChild(DataElem);
            userElem.AppendChild(TimeElem);
            userElem.AppendChild(DeloElem);

            xRoot.AppendChild(userElem);
            xDoc.Save("result.xml");
            DDBOrder(Tb1.Text, Tb2.Text, Tb3.Text);
        }  // OK

        private void DDBOrder(string T1, string T2, string T3)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FFCBF703");


            Border bord = new Border( )
            {
                BorderBrush = brush,
                BorderThickness = new Thickness(2, 0, 2, 2),
                MinHeight = 95
            };
            Stack_Delo.Children.Add(bord);

            Grid grid = new Grid();

            //grid.ShowGridLines = true;

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition() { Width = System.Windows.GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            string[] temp = new string[3] { "Дата", "Время", "Занятие" };
            string[] temp2 = new string[3] {T1,T2,T3};

            for (int i = 0; i < 3; i++)
            {
                Label lab = new Label()
                {
                    Content = temp[i].ToString(),
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Background = (Brush)converter.ConvertFromString("#FF7378EC"),
                    FontSize = 16,
                    MinHeight = 30,
                    MinWidth = 68,

                };
                Label tb = new Label()
                {
                    Content = temp2[i].ToString(),
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    FontSize = 16,
                    MinWidth = 117
                };



                Grid.SetRow(lab, i); // на грид
                Grid.SetColumn(lab, 0); // на грид

                Grid.SetRow(tb, i); // на грид
                Grid.SetColumn(tb, 1); // на грид
                if (i == 2)
                {
                    Grid.SetColumnSpan(tb, 4);
                    tb.Margin = new Thickness(0, 0, 10, 0);
                }

                grid.Children.Add(lab);
                grid.Children.Add(tb);
            }
            Button but = new Button()
            {
                Content = "Выполненно",
                Margin = new Thickness(10, 0, 10, 1),
                FontSize = 16,
                Width = 97,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,

            };

            but.Click += new RoutedEventHandler(Del_Border);

            Grid.SetRow(but, 2); Grid.SetColumn(but, 5);
            grid.Children.Add(but);

            bord.Child = grid;

        }  // add border

        
        private void Del_Border(object sender, RoutedEventArgs e)
        {

            var parent2 = VisualTreeHelper.GetParent((Button)sender); // grid

            var parent = VisualTreeHelper.GetParent(parent2); // border

            var parent0 = VisualTreeHelper.GetParent(parent); // Stack

            string Temp_Data = "";
            string Temp_Time = "";

            int count = VisualTreeHelper.GetChildrenCount(parent0);
            for (int i = 0; i < count; i++)
            {
                var childVisual = (Border)VisualTreeHelper.GetChild(parent0, i);
                if (childVisual is Border)
                {
                    if (childVisual == parent)
                    {
                        Stack_Delo.Children.Remove(childVisual);

                        int count1 = VisualTreeHelper.GetChildrenCount(parent2);
                        for (int ii = 0; ii < count1; ii++)
                        {
                            var childVisuala = (Label)VisualTreeHelper.GetChild(parent2, ii);
                            if (childVisuala is Label)
                            {
                                if(ii == 1)
                                {
                                    Temp_Data = childVisuala.ToString().Split(' ')[1];
                                   //MessageBox.Show(Temp_Data);
                                }
                                    
                                if(ii == 3)
                                {
                                    Temp_Time = childVisuala.ToString().Split(' ')[1];
                                    //MessageBox.Show(Temp_Time);
                                    break;
                                }
                                    
                            }
                        
                        }

                        break;
                        //MessageBox.Show(childVisual.ToString());
                    }
                        
                    
                }
                else
                {

                }
            }

            Delete_Delo(Temp_Data, Temp_Time);

            //parent0.Children.Remove(parent);
        }

        private void Delete_Delo(string TData, string TTime)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("result.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            string ttmp = TData + " " + TTime;
            //MessageBox.Show(ttmp);
            XmlNode childnode = xRoot.SelectSingleNode("Use[@Number='" + ttmp + "']");
            try { xRoot.RemoveChild(childnode); }
            catch { }
            
            xDoc.Save("result.xml");

        } // Delete

       
    }
}