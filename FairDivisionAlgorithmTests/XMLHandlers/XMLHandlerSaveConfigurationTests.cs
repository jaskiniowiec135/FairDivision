using FairDivisionAlgorithm.Handlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FairDivisionAlgorithmTests.XMLHandlers
{
    public class XMLHandlerSaveConfigurationTests
    {
        private XMLHandler handler;
        private string testPath = "..\\..\\TestResults\\";
        private string fileName;

        [SetUp]
        public void SetUp()
        {
            fileName = "SaveConfigurationTest " + Guid.NewGuid().ToString() + ".xml";

            handler = new XMLHandler(testPath, fileName);
        }

        [Test]
        public void SaveCorrectConfigurationToFile()
        {
            // a
            Dictionary<string, string> configuration = new Dictionary<string, string>
            {
                { "FirstName", "FirstValue" }
            };

            // aa
            handler.SaveConfigurationToFile(configuration);

            // aaa
            XDocument document = XDocument.Load(testPath + fileName);

            Assert.IsTrue(File.Exists(testPath + fileName) &&
                document.Descendants("element").First().Elements().Any(n => n.Name == "name" && n.Value == "FirstName") &&
                document.Descendants("element").First().Elements().Any(n => n.Name == "measureUnit" && n.Value == "FirstValue"));
        }

        [Test]
        public void SaveCorrectConfigurationWithNameOnlyToFile()
        {
            // a
            Dictionary<string, string> configuration = new Dictionary<string, string>
            {
                { "FirstName", "" }
            };

            // aa
            handler.SaveConfigurationToFile(configuration);

            // aaa
            XDocument document = XDocument.Load(testPath + fileName);
            Assert.IsTrue(File.Exists(testPath + fileName) &&
                document.Descendants("element").Elements().Any(n => n.Name == "name" && n.Value == "FirstName") &&
                document.Descendants("element").First().Elements().Any(n => n.Name == "measureUnit" && n.Value == ""));
        }

        [Test]
        public void SaveCorrectConfigurationWithValueOnlyToFile()
        {
            // a
            Dictionary<string, string> configuration = new Dictionary<string, string>
            {
                { "", "FirstValue" }
            };

            // aa
            handler.SaveConfigurationToFile(configuration);

            // aaa
            XDocument document = XDocument.Load(testPath + fileName);
            Assert.IsTrue(File.Exists(testPath + fileName) &&
                document.Descendants("element").First().Elements().Any(n => n.Name == "measureUnit" && n.Value == "FirstValue"));
        }

        [Test]
        public void SaveEmptyConfiguration()
        {
            // a
            Dictionary<string, string> configuration = new Dictionary<string, string>();

            // aa
            handler.SaveConfigurationToFile(configuration);

            // aaa

            Assert.IsTrue(File.Exists(testPath + fileName));
        }
    }
}
