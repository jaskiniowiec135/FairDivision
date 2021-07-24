using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public void SaveConfigurationToFile(Dictionary<string, string> configuration)
        {
            XDocument document = new XDocument(
                new XElement("elements"));

            foreach (var keyValuePair in configuration)
            {
                document.Descendants().First(x => x.Name == "elements").Add(new XElement("element",
                    new XElement("name", keyValuePair.Key),
                    new XElement("measureUnit", keyValuePair.Value)));
            }

            document.Save(Path + FileName);
        }

        public void SaveMembersToFile(List<MemberObject> members)
        {

        }

        public Dictionary<string, string> GetConfigurationFromDocument()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            XDocument configuration = ReadFromFile();

            foreach (XElement node in configuration.Descendants())
            {
                if (node.Name == "element")
                {
                    result.Add(node.Descendants().FirstOrDefault(x => x.Name == "name").Value,
                        node.Descendants().FirstOrDefault(x => x.Name == "measureUnit").Value);
                }
            }

            return result;
        }

        public List<MemberObject> GetMembersFromDocument()
        {
            List<MemberObject> result = new List<MemberObject>();
            XDocument members = ReadFromFile();

            foreach (XElement node in members.Descendants().Where(x => x.Name == "member"))
            {
                MemberObject member = new MemberObject("",
                    new bool[5],
                    new int[5],
                    new double[5]);

                member.Name = node.Attribute("name").Value;

                for (int i = 0; i < node.Descendants().Where(x => x.Name == "param").Count(); i++)
                {
                    var n = node.Descendants().Where(x => x.Name == "param").ElementAt(i);

                    Debug.WriteLine(n.Elements().ElementAt(2).Value);

                    member.LessThan[i] = bool.Parse(n.Elements().ElementAt(0).Value);
                    member.Values[i] = int.Parse(n.Elements().ElementAt(1).Value);
                    member.Rank[i] = double.Parse(n.Elements().ElementAt(2).Value);
                }

                result.Add(member);
            }

            return result;
        }

        private XDocument ReadFromFile()
        {
            XDocument result = XDocument.Load(Path + FileName);

            return result;
        }
    }
}
