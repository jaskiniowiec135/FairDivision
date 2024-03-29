﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace FairDivisionAlgorithm.Handlers
{
    public class XMLHandler
    {
        public string path;
        public string fileName;

        public XMLHandler(string path, string fileName)
        {
            this.path = path;
            this.fileName = fileName;
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

            document.Save(path + fileName);
        }

        public void SaveMembersToFile(List<MemberObject> members)
        {
            XDocument document = new XDocument(
                new XElement("members"));

            foreach(MemberObject member in members)
            {
                document.Descendants().First(x => x.Name == "members").Add(new XElement("member",
                    new XAttribute("name", member.Name),
                    new XElement("params")));

                for (int i = 0; i < member.AcceptableValues.Length; i++)
                {
                    document.Descendants().Last(x => x.Name == "params").Add(new XElement("param",
                        new XElement("bestValue", member.BestValues[i]),
                        new XElement("acceptableValue", member.AcceptableValues[i]),
                        new XElement("rank", member.Rank[i])));
                }
            }

            document.Save(path + fileName);
        }

        public void SaveObjectsToFile(List<DivisionObject> divisionObjects)
        {
            XDocument document = new XDocument(
                new XElement("objects"));

            foreach (DivisionObject divisionObject in divisionObjects)
            {
                document.Descendants().First(x => x.Name == "objects").Add(new XElement("object",
                    new XElement("name", divisionObject.ObjectName),
                    new XElement("owner", divisionObject.OwnerName),
                    new XElement("params")));

                for (int i = 0; i < divisionObject.ParametersValues.Length; i++)
                {
                    document.Descendants().Last(x => x.Name == "params")
                        .Add(new XElement("param", divisionObject.ParametersValues[i]));
                }
            }

            document.Save(path + fileName);
        }

        public Dictionary<string, string> GetConfigurationFromDocument()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            XDocument configuration = XDocument.Load(path + fileName);

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
            XDocument members = XDocument.Load(path + fileName);

            foreach (XElement node in members.Descendants().Where(x => x.Name == "member"))
            {
                MemberObject member = new MemberObject("",
                    new int[5],
                    new int[5],
                    new double[5]);

                member.Name = node.Attribute("name").Value;

                for (int i = 0; i < node.Descendants().Where(x => x.Name == "param").Count(); i++)
                {
                    var n = node.Descendants().Where(x => x.Name == "param").ElementAt(i);

                    member.BestValues[i] = int.Parse(n.Elements().ElementAt(0).Value);
                    member.AcceptableValues[i] = int.Parse(n.Elements().ElementAt(1).Value);
                    member.Rank[i] = double.Parse(n.Elements().ElementAt(2).Value.Replace('.',','));
                }

                result.Add(member);
            }

            return result;
        }

        public List<DivisionObject> GetObjectsFromDocument()
        {
            List<DivisionObject> result = new List<DivisionObject>();
            XDocument objects = XDocument.Load(path + fileName);

            foreach (XElement node in objects.Descendants().Where(x => x.Name == "object"))
            {
                DivisionObject divisionObject = new DivisionObject(
                    "", "", new int[5]);

                divisionObject.ObjectName = node.Descendants().FirstOrDefault(x => x.Name == "name").Value;
                divisionObject.OwnerName = node.Descendants().FirstOrDefault(x => x.Name == "owner").Value;

                for (int i = 0; i < node.Descendants().Where(x => x.Name == "param").Count(); i++)
                {
                    divisionObject.ParametersValues[i] = int.Parse(node.Descendants().Where(
                        x => x.Name == "param").ElementAt(i).Value);
                }

                result.Add(divisionObject);
            }

            return result;
        }
    }
}
