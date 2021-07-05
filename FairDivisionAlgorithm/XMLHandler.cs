using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FairDivisionAlgorithm
{
    public class XMLHandler
    {
        public string Path;
        public string FileName;

        public XMLHandler(string path, string fileName)
        {
            Path = path;
            FileName = fileName;
        }

        public void SaveToFile(Dictionary<string,string> dictionary, string[] attributeNames)
        {
            XDocument document = PrepareXMLFromDictionary(dictionary, attributeNames);

            document.Save(Path + FileName);
        }

        public Dictionary<string, string> ReadFromFile()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            XDocument document = XDocument.Load(Path + FileName);

            result = PrepareDictionaryFromXML(document);

            return result;
        }

        private static Dictionary<string,string> PrepareDictionaryFromXML(XDocument document)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach(XElement node in document.DescendantNodes())
            {
                if(node.Name == "element")
                {
                    result.Add(node.Attributes().ElementAt(0).Value, node.Attributes().ElementAt(1).Value);
                }
                
            }

            return result;
        }

        private static XDocument PrepareXMLFromDictionary(Dictionary<string,string> dictionary, string[] attributes)
        {
            XDocument result = new XDocument(
                new XElement("elements"));

            foreach(var kvpair in dictionary)
            {
                result.Element("elements").Add(
                    new XElement(name: "element", new XAttribute(attributes[0], kvpair.Key), new XAttribute(attributes[1], kvpair.Value)));
            }

            return result;
        }
    }
}
