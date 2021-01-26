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
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Data;

namespace ReprezentacjaPolski2
{
    public partial class Window2 : Window
    {

        private List<Reprezentacja> m_RepList = null;
        private string NumerID;
        private string szukaj;
        private string gdzie;
        public Window2(string text)
        {
            string id = text;
            NumerID = text;
            InitializeComponent();
            Bind(id);
        }

        private void Bind(string id)
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
            Reprezentacja mFound = m_RepList.Find(oElement => oElement.IdRep == Convert.ToInt32(NumerID));
            Text1.Text = mFound.Imie;
            Text2.Text = mFound.Nazwisko;
            Text3.Text = Convert.ToString(mFound.Wiek);
            Text4.Text = mFound.Klub;
            Text5.Text = Convert.ToString(mFound.Gole);
            Text6.Text = Convert.ToString(mFound.Wystepy);
            Text8.Text = mFound.Pozycja;
            NumerID = id;


        }

        private void Modyfikuj_Clikc(object sender, RoutedEventArgs e)
        {
            Text1.IsEnabled = true;
            Text2.IsEnabled = true;
            Text3.IsEnabled = true;
            Text4.IsEnabled = true;
            Text5.IsEnabled = true;
            Text6.IsEnabled = true;
            Text7.IsEnabled = true;
            Text8.IsEnabled = true;
            Wstaw.IsEnabled = true;
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            Reprezentacja mFound = m_RepList.Find(oElement => oElement.IdRep == Convert.ToInt32(NumerID));
            mFound.Pozycja = Text8.Text;
            mFound.Imie = Text1.Text;
            mFound.Nazwisko = Text2.Text;
            mFound.Wiek = Convert.ToInt32(Text3.Text);
            mFound.Klub = Text4.Text;
            mFound.Gole = Convert.ToInt32(Text5.Text);
            mFound.Wystepy = Convert.ToInt32(Text6.Text);
            mFound.Pozycja = Text8.Text;
            mFound.IdRep = Convert.ToInt32(Text7.Text);

            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-5KNOM36\SQLEXPRESS;Database=Reprezentacja1;User ID=dalek; Password=haslo ;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            String sql;

            sql = "Update TabelaRep1 Set ID='" +this.Text7.Text + "', Imie='" + this.Text1.Text + "', Nazwisko='" + this.Text2.Text + "', Wiek='" + this.Text3.Text + "', Klub='" + this.Text4.Text + "', Gole='" + this.Text5.Text + "', Wystepy ='" + this.Text6.Text + "', Pozycja='" + this.Text8.Text + "' Where ID='" + NumerID + "'";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();


            MessageBox.Show("Nowe dane zostały zapisane!");

            var serializer = new XmlSerializer(m_RepList.GetType());
            using (var writer = XmlWriter.Create("Reprezentacja.xml"))
            {
                serializer.Serialize(writer, m_RepList);
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

        private void Obraz_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Filses(*.jpg; *.jpeg; *.gif; *.png; *.bmp;)|*.jpg; *.jpeg; *.png; *.gif; *.bmp;";

            if (openFileDialog.ShowDialog() == true)
            {
                szukaj = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                Pokaz_obraz.Source = new BitmapImage(fileUri);
            }
        }
    }
}
