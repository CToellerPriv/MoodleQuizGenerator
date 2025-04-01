using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lernmaschine
{
    internal class ModelXML : IModel
    {
        private IView view;
        private IController controller;
        private XDocument doc;
        private Karteikarte karteikarte=new Karteikarte();
        private List<Karteikarte> karteikarten=new List<Karteikarte>();
        private string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Lernmaschine";


        public ModelXML()
        {
            //string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Lernmaschine";

            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            if (!File.Exists(LogPath+@".\lernmaschine.xml"))
            {
                doc = new XDocument(new XElement("Karteikarten"));
                doc.Save(LogPath + @".\lernmaschine.xml");
            }
            else
                doc = XDocument.Load(LogPath + @".\lernmaschine.xml");
        }


        IView IModel.View { set
            { 
                view = value;
                view.anzeigen((this as  IModel).suchen(new Karteikarte()));
            }
        }
        IController IModel.Controller { set => controller=value; }

        void IModel.aendern(Karteikarte karteikarte)
        {
            throw new NotImplementedException();
        }

        void IModel.einfuegen(Karteikarte karteikarte)
        {
            XElement newElement = new XElement("Karteikarte",
                 new XAttribute("Karteikartennummer", karteikarte.Karteikartennummer),
                 new XAttribute("Fach", karteikarte.Fach),
                 new XElement("Unterrichtsfach", karteikarte.Unterrichtsfach),
                 new XElement("Thema", karteikarte.Thema),
                 new XElement("Vorderseite", karteikarte.Vorderseite),
                 new XElement("Rueckseite", karteikarte.Rueckseite));
            doc.Element("Karteikarten").Add(newElement);
            doc.Save(LogPath+@".\lernmaschine.xml");
        }

        void IModel.loeschen(Karteikarte karteikarte)
        {
            throw new NotImplementedException();
        }

        List<Karteikarte> IModel.suchen(Karteikarte karteikarte)
        {
            
            karteikarten.Clear();
            foreach(var erg in doc.Descendants("Karteikarte"))
            {
                karteikarte = new Karteikarte();
                karteikarte.Vorderseite =erg.Element("Vorderseite").Value;
                karteikarte.Rueckseite = erg.Element("Rueckseite").Value;
                karteikarte.Thema = erg.Element("Thema").Value;
                karteikarte.Karteikartennummer = Convert.ToInt32(erg.Attribute("Karteikartennummer").Value);
                karteikarte.Fach = erg.Attribute("Fach").Value;
                karteikarte.Unterrichtsfach = erg.Element("Unterrichtsfach").Value;
                 
                karteikarten.Add(karteikarte);                 
            }
            return karteikarten;
        }
    }
}
