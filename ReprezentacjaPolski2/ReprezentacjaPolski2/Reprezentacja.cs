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
using System.Xml.Serialization;

namespace ReprezentacjaPolski2
{
    public class Reprezentacja
    {
        [XmlAttribute("IdRep")]
        public int IdRep { get; set; }
        [XmlElement("Imie")]
        public string Imie { get; set; }
        [XmlElement("Nazwisko")]
        public string Nazwisko { get; set; }
        [XmlElement("Wiek")]
        public int Wiek { get; set; }
        [XmlElement("Klub")]
        public string Klub { get; set; }
        [XmlElement("Gole")]
        public int Gole { get; set; }
        [XmlElement("Wystepy")]
        public int Wystepy { get; set; }
        [XmlElement("Obraz")]
        public string Obraz { get; set; }

        [XmlElement("Pozycja")]
        public string Pozycja { get; set; }


        public Reprezentacja(int nIdRep, string nImie, string nNazwisko, int nWiek, string nKlub,
    int nTelefon, int nGole, string nObraz, string nPozycja)
        {
            IdRep = nIdRep;
            Imie = nImie;
            Nazwisko = nNazwisko;
            Wiek = nWiek;
            Klub = nKlub;
            Gole = nTelefon;
            Wystepy = nGole;
            Obraz = nObraz;
            Pozycja = nPozycja;
        }

        public Reprezentacja()
        {
            IdRep = 0;
            Imie = "Jan";
            Nazwisko = "Kowalski";
            Wiek = 18;
            Klub = "FC Ulani";
            Gole = 0;
            Wystepy = 0;
            Obraz = "C:\\Users\\DaleckiKOMP\\source\\repos\\ReprezentacjaPolski2\\Programowanie-Projekt\\Obrazy\\Lewandowski.jpeg";
            Pozycja = "BRAK";
        }
    }
}
