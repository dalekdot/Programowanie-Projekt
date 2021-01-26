using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;

namespace ReprezentacjaPolski2
{
    public partial class Window1 : Window
    {
        private string gdzie;
        private List<Reprezentacja> m_RepList = null;
        public Window1()
        {
            InitializeComponent();
            Binding();
        }

        private void Binding()
        {
            m_RepList = new List<Reprezentacja>();
            try
            {
                using (var reader = new StreamReader("Reprezentacja.xml"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Reprezentacja>),
                        new XmlRootAttribute("ArrayOfReprezentacja"));
                    m_RepList = (List<Reprezentacja>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
                MessageBox.Show("Brak pliku do załadowania!", "Uwaga", MessageBoxButton.OK);
            }

            Image image = new Image();
            BitmapImage bitmapImage = new BitmapImage(new Uri("C:\\Users\\DaleckiKOMP\\source\\repos\\ReprezentacjaPolski2\\Programowanie-Projekt\\Obrazy\\Lewandowski.jpeg"));
            obraz.Source = bitmapImage;
        }

        private void Obraz_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Filses(*.jpg; *.jpeg; *.gif; *.png; *.bmp;)|*.jpg; *.jpeg; *.png; *.gif; *.bmp;";

            if (openFileDialog.ShowDialog() == true)
            {
                gdzie = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                obraz.Source = new BitmapImage(fileUri);
            }
        }

        private void Odswiez_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var reader = new StreamReader("Reprezentacja.xml"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Reprezentacja>),
                        new XmlRootAttribute("ArrayOfReprezentacja"));
                    m_RepList = (List<Reprezentacja>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
                MessageBox.Show("Brak pliku do załadowania!", "Uwaga", MessageBoxButton.OK);
            }

            ZawodnicySkrócony.ItemsSource = m_RepList;
        }

        private void Zapisz(object sender, RoutedEventArgs e)
        {
            try
            {
                ZawodnicySkrócony.ItemsSource = null;
                var serializer = new XmlSerializer(m_RepList.GetType());
                m_RepList.Add(new Reprezentacja(m_RepList.Count + 1, Text1.Text, Text2.Text, Convert.ToInt32(Text3.Text),
                    Text4.Text, Convert.ToInt32(Text5.Text), Convert.ToInt32(Text6.Text), gdzie, Text8.Text));
                using (var writer = XmlWriter.Create("Reprezentacja.xml"))
                {
                    serializer.Serialize(writer, m_RepList);
                }

                string connetionString;

                SqlConnection cnn;
                connetionString = @"Data Source=DESKTOP-5KNOM36\SQLEXPRESS;Database=Reprezentacja1;User ID=dalek; Password=haslo ;";
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                String sql;

                sql = "Insert into TabelaRep1 (ID,Imie,Nazwisko,Wiek,Klub,Gole , Wystepy, Pozycja) values('" + this.Text7.Text + "','" + this.Text1.Text + "','" + this.Text2.Text + "','" + this.Text3.Text + "','" + this.Text4.Text + "','" + this.Text5.Text + "','" + this.Text6.Text + "','" + this.Text8.Text + "')";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                MessageBox.Show("Dodano");

                try
                {
                    using (var reader = new StreamReader("Reprezentacja.xml"))
                    {
                        XmlSerializer deserializer = new XmlSerializer(typeof(List<Reprezentacja>),
                            new XmlRootAttribute("ArrayOfReprezentacja"));
                        m_RepList = (List<Reprezentacja>)deserializer.Deserialize(reader);
                    }
                }
                catch
                {
                    MessageBox.Show("Brak pliku do załadowania!", "Uwaga", MessageBoxButton.OK);
                }
                ZawodnicySkrócony.ItemsSource = m_RepList;
            }
            catch(Exception)
            {
                MessageBox.Show("Wpisz poprawnie wszystkie dane!");
            }
        }

        private void Modify(object sender, MouseButtonEventArgs e)
        {
            var i = (sender as ListView).SelectedIndex;
            if (i > -1)
            {
                Window2 openWindow2 = new Window2(Convert.ToString(i + 1));
                openWindow2.ShowDialog();
            }
        }


        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var serializer = new XmlSerializer(m_RepList.GetType());
            using (var writer = XmlWriter.Create("Reprezentacja.xml"))
            {
                serializer.Serialize(writer, m_RepList);
            }
        }
    }
}
