public class User
    {
        public string Data { get; set; }
        public string Time { get; set; }
        public string Delo { get; set; }
    }

    public class Catalog
    {
        /// <summary>
        /// Список дел.
        /// </summary>
        public List<User> catalog { get; set; } = new List<User>();
    }



private void Write_XML(object sender, RoutedEventArgs e)
        {
            List<TextBox> list = new List<TextBox>();
            foreach (FrameworkElement txt in Stack_Delo.Children)
            {
                if (txt is TextBox)
                {
                    TextBox txtBlock = (TextBox)txt;
                    list.Add(txtBlock);
                }
                else
                    continue;
            }

            using (StreamWriter sr = new StreamWriter("Sample.txt"))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    sr.Write(list[i].Text + "\t");
                }
            }

            var catalog = new Catalog() // Корневой элемент
            {
                catalog = new List<User>() // Коллекция номеров телефонов.
                {
                    new User() {Data = "Саша", Time = "1", Delo = "Не бери трубку!"}, // Запись номера телефона.
                    new User() {Data = "Дима", Time = "2", Delo = "Босс"},
                    new User() {Data = "Рита", Time = "3", Delo = "Невероятная девчонка"}
                }
            };

            //Пишем в файл.
            WriteXmlFile("result.xml", catalog);
        }

        private void WriteXmlFile(string FName, Catalog catalog)
        {
            string[] temp = new string[3] { "Дата", "Время", "Занятие" };

            // Создаем новый Xml документ.
            XmlDocument xDoc = new XmlDocument();

            // Создаем Xml заголовок.
            var xmlDeclaration = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

            // Добавляем заголовок перед корневым элементом.
            xDoc.AppendChild(xmlDeclaration);

            // Создаем Корневой элемент
            var root = xDoc.CreateElement("Catalog");

            foreach (var ii in catalog.catalog)
            {
                // Создаем элемент записи .
                var Elem = xDoc.CreateElement("Use");

                foreach(var yy in temp)
                {
                    // Создаем атрибут и нужным именем.
                    var attribute = xDoc.CreateAttribute(yy);

                    // Устанавливаем содержимое атрибута.
                    attribute.InnerText = yy;

                    // Добавляем атрибут к элементу.
                    Elem.Attributes.Append(attribute);
                }
                   
                // Создаем зависимые элементы.
                AddChildNode("Data", ii.Data, Elem, xDoc);
                AddChildNode("Time", ii.Time, Elem, xDoc);
                AddChildNode("Delo", ii.Delo, Elem, xDoc);

                // Добавляем запись в каталог.
                root.AppendChild(Elem);

            }

            // Добавляем новый корневой элемент в документ.
            xDoc.AppendChild(root);

            // Сохраняем документ.
            xDoc.Save(FName);
        }


        /// <summary>
        /// Добавить зависимый элемент с текстом.
        /// </summary>
        /// <param name="childName"> Имя дочернего элемента. </param>
        /// <param name="childText"> Текст, который будет внутри дочернего элемента. </param>
        /// <param name="parentNode"> Родительский элемент. </param>
        /// <param name="doc"> Xml документ. </param>
        private static void AddChildNode(string childName, string childText, XmlElement parentNode, XmlDocument doc)
        {
            var child = doc.CreateElement(childName);
            child.InnerText = childText;
            parentNode.AppendChild(child);
        }