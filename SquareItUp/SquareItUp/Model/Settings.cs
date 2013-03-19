using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Resources;
using System.Reflection;
using System.ComponentModel;
using System.Data;

namespace SquareItUp.Model
{
    public  class Settings
    {
        string myAgainstWhom="";
        string myWhoMovesFirst="";
        int myLevel=1;

        public string AgainstWhom
        {
            get { return myAgainstWhom; }
            set {myAgainstWhom = value; }
        }

        public string WhoMovesFirst
        {
            get { return myWhoMovesFirst; }
            set { myWhoMovesFirst = value; }
        }

        public int Level
        {
            get { return myLevel; }
            set { myLevel = value; }
        }
        public void ModifySettingsFile(string myNodeName, string myValue)
        {
            try
            {
                XmlDocument myXmlDocument = new XmlDocument();
                string programFolder = Path.GetDirectoryName(this.GetType().Assembly.GetModules()[0].FullyQualifiedName);
                myXmlDocument.Load(Path.Combine(programFolder, "Settings.xml"));
                XmlNode node;
                node = myXmlDocument.DocumentElement;
                if (node.ChildNodes[0].Name.ToString() == myNodeName)
                     node.ChildNodes[0].InnerText = myValue;

                if (node.ChildNodes[1].Name.ToString() == myNodeName)
                     node.ChildNodes[1].InnerText = myValue;

                if (node.ChildNodes[2].Name.ToString() == myNodeName)
                     node.ChildNodes[2].InnerText = myValue;

                myXmlDocument.Save(Path.Combine(programFolder, "Settings.xml"));
            }
            catch { }
        }
        public  string GetXMLNodeValue(string myNodeName)
        {
            try
            {
                XmlDocument myXmlDocument = new XmlDocument();
                string programFolder = Path.GetDirectoryName(this.GetType().Assembly.GetModules()[0].FullyQualifiedName);
                myXmlDocument.Load(Path.Combine(programFolder, "Settings.xml"));
                XmlNode node;
                node = myXmlDocument.DocumentElement;

               if (node.ChildNodes[0].Name.ToString() == myNodeName)
                   return node.ChildNodes[0].InnerText;

               if (node.ChildNodes[1].Name.ToString() == myNodeName)
                   return node.ChildNodes[1].InnerText;

               if (node.ChildNodes[2].Name.ToString() == myNodeName)
                   return node.ChildNodes[2].InnerText;
                return "";
            }
            catch { return ""; }
        }
    }
}
