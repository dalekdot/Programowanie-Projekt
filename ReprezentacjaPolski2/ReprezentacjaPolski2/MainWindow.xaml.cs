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
    public partial class MainWindow : Window
    {

        private List<Reprezentacja> m_RepList = null;
        Window1 openWindow1 = new Window1();
        Window3 openWindow3 = new Window3();

        public MainWindow()
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

            if (m_RepList.Count == 0)
            {
                m_RepList.Add(new Reprezentacja(1, "Robert", "Lewandowski", 33, "Bayern Monachium", 123456789, 115, "C:\\Users\\DaleckiKOMP\\source\\repos\\ReprezentacjaPolski2\\Programowanie-Projekt\\Obrazy\\Lewandowski.jpeg", "BRAK"));
                m_RepList.Add(new Reprezentacja(2, "Krzysztof", "Piątek", 22, "Hearta Berlin", 123456789, 20, "C:\\Users\\DaleckiKOMP\\source\\repos\\ReprezentacjaPolski2\\Programowanie-Projekt\\Obrazy\\Lewandowski.jpeg","BRAK"));
                m_RepList.Add(new Reprezentacja(3, "Arek", "Milik", 26, "Napoli", 123456789, 115, "C:\\Users\\DaleckiKOMP\\source\\repos\\ReprezentacjaPolski2\\Programowanie-Projekt\\Obrazy\\Lewandowski.jpeg", "BRAK"));
            }
            Zawodnicy1.ItemsSource = m_RepList;

        }

        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var serializer = new XmlSerializer(m_RepList.GetType());
            using (var writer = XmlWriter.Create("Reprezentacja.xml"))
            {
                serializer.Serialize(writer, m_RepList);
            }
        }

        private void Usuń_Click(object sender, RoutedEventArgs e)
        {
            m_RepList.RemoveAt(Convert.ToInt32(SID.Text) - 1);
            var serializer = new XmlSerializer(m_RepList.GetType());
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

            sql = "Delete From TabelaRep1 where ID='" + SID.Text + "'";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            MessageBox.Show("Usunięto Reprezentanta od ID = " + SID.Text);
        }

        private void Edytuj_Click(object sender, RoutedEventArgs e)
        {
            Window2 openWindow2 = new Window2(SID.Text);
            openWindow2.Show();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            openWindow1.Show();
        }

        private void Baza_Click(object sender, RoutedEventArgs e)
        {
            openWindow3.Show();
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

            Zawodnicy1.ItemsSource = m_RepList;
        }
    }
}
