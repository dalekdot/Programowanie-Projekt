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

    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-5KNOM36\SQLEXPRESS;Database=Reprezentacja1;User ID=dalek; Password=haslo ;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //MessageBox.Show("Połączenie Nawiązane");
            //cnn.Close();

            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "Select * From TabelaRep1 WHERE Pozycja='BR'";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + "-" + dataReader.GetValue(4) + "\n";
            }
            MessageBox.Show(Output);
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        private void OB_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-5KNOM36\SQLEXPRESS;Database=Reprezentacja1;User ID=dalek; Password=haslo ;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //MessageBox.Show("Połączenie Nawiązane");
            //cnn.Close();

            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "Select * From TabelaRep1 WHERE Pozycja='OB'";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + "-" + dataReader.GetValue(4) + "\n";
            }
            MessageBox.Show(Output);
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        private void POM_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-5KNOM36\SQLEXPRESS;Database=Reprezentacja1;User ID=dalek; Password=haslo ;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //MessageBox.Show("Połączenie Nawiązane");
            //cnn.Close();

            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "Select * From TabelaRep1 WHERE Pozycja='POM'";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + "-" + dataReader.GetValue(4) + "\n";
            }
            MessageBox.Show(Output);
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        private void ATAK_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-5KNOM36\SQLEXPRESS;Database=Reprezentacja1;User ID=dalek; Password=haslo ;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //MessageBox.Show("Połączenie Nawiązane");
            //cnn.Close();

            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "Select * From TabelaRep1 WHERE Pozycja='N'";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + "-" + dataReader.GetValue(4) + "\n";
            }
            MessageBox.Show(Output);
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        private void zalduj_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-5KNOM36\SQLEXPRESS;Database=Reprezentacja1;User ID=dalek; Password=haslo ;";
            try
            {
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                SqlCommand command;
                // SqlDataReader dataReader;
                String sql;

                sql = "Select * From TabelaRep1";
                command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                // dataReader = command.ExecuteReader();

                SqlDataAdapter dataAdp = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Reprezentanci2");
                dataAdp.Fill(dt);
                DataGrid1.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
