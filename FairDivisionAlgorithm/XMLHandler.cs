using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SaveToFile(Dictionary<string,string> dictionary)
        {
            XDocument document = PrepareXMLFromDictionary(dictionary);

            document.Save(Path + FileName + ".xml");
        }

        public Dictionary<string, string> ReadFromFile()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            return result;
        }

        private static XDocument PrepareXMLFromDictionary(Dictionary<string,string> dictionary)
        {
            XDocument result = new XDocument();

            return result;
        }
    }
}
