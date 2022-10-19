using FairDivisionAlgorithm.Handlers;
using FairDivisionAlgorithm.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FairDivisionAlgorithmTests.XMLHandlers
{
    public class XMLHandlerSaveMembersTests
    {
        private XMLHandler handler;
        private string testPath = "..\\..\\TestResults\\";
        private string fileName;

        [SetUp]
        public void SetUp()
        {
            fileName = "SaveMembersTest " + Guid.NewGuid().ToString() + ".xml";

            handler = new XMLHandler(testPath, fileName);
        }

        [Test]
        public void SaveCorrectMemberToFile()
        {
            //a
            List<MemberObject> members = new List<MemberObject>
            {
                new MemberObject(
                    "Mark",
                    new int[1] { 1 },
                    new int[1] {1},
                    new double[1] { 0.2 })
            };

            //aa
            handler.SaveMembersToFile(members);

            //aaa
            XDocument document = XDocument.Load(testPath + fileName);

            Assert.IsTrue(File.Exists(testPath + fileName) &&
                document.Descendants("members").Elements().Any(e => e.Attribute("name").Value == "Mark") &&
                document.Descendants("member").Descendants("params").Elements().First().Elements().Any(n => n.Name == "bestValue" && n.Value == "1") &&
                document.Descendants("member").Descendants("params").Elements().First().Elements().Any(n => n.Name == "acceptableValue" && n.Value == "1") &&
                document.Descendants("member").Descendants("params").Elements().First().Elements().Any(n => n.Name == "rank" && n.Value == "0.2"));
        }

        [Test]
        public void SaveCorrectMemberWithManyParamsToFile()
        {
            //a
            List<MemberObject> members = new List<MemberObject>
            {
                new MemberObject(
                    "Mark",
                    new int[3] { 1,2,3 },
                    new int[3] {1,2,3},
                    new double[3] { 0.2, 0.3, 0.4 })
            };

            //aa
            handler.SaveMembersToFile(members);

            //aaa
            XDocument document = XDocument.Load(testPath + fileName);

            Assert.IsTrue(File.Exists(testPath + fileName) &&
                document.Descendants("members").Elements().Any(e => e.Attribute("name").Value == "Mark") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(0).Elements().Any(n => n.Name == "bestValue" && n.Value == "1") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(0).Elements().Any(n => n.Name == "acceptableValue" && n.Value == "1") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(0).Elements().Any(n => n.Name == "rank" && n.Value == "0.2") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(1).Elements().Any(n => n.Name == "bestValue" && n.Value == "2") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(1).Elements().Any(n => n.Name == "acceptableValue" && n.Value == "2") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(1).Elements().Any(n => n.Name == "rank" && n.Value == "0.3") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(2).Elements().Any(n => n.Name == "bestValue" && n.Value == "3") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(2).Elements().Any(n => n.Name == "acceptableValue" && n.Value == "3") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").ElementAt(2).Elements().Any(n => n.Name == "rank" && n.Value == "0.4"));
        }

        [Test]
        public void SaveCorrectMemberWithEmptyValuesToFile()
        {
            //a
            List<MemberObject> members = new List<MemberObject>
            {
                new MemberObject(
                    "Mark",
                    new int[1] { 0 },
                    new int[1] { 0 },
                    new double[1] { 0 })
            };

            //aa
            handler.SaveMembersToFile(members);

            //aaa
            XDocument document = XDocument.Load(testPath + fileName);

            Assert.IsTrue(File.Exists(testPath + fileName) &&
                document.Descendants("members").Elements().Any(e => e.Attribute("name").Value == "Mark") &&
                document.Descendants("member").Descendants("params").Elements().First().Elements().Any(n => n.Name == "bestValue" && n.Value == "0") &&
                document.Descendants("member").Descendants("params").Elements().First().Elements().Any(n => n.Name == "acceptableValue" && n.Value == "0") &&
                document.Descendants("member").Descendants("params").Elements().First().Elements().Any(n => n.Name == "rank" && n.Value == "0"));
        }

        [Test]
        public void SaveCorrectMembersToFile()
        {
            //a
            List<MemberObject> members = new List<MemberObject>
            {
                new MemberObject(
                    "Mark",
                    new int[1] { 1 },
                    new int[1] {1},
                    new double[1] { 0.2 }),
                new MemberObject (
                    "John",
                    new int[1] { 2 },
                    new int[1] {2},
                    new double[1] { 0.4 })
            };

            //aa
            handler.SaveMembersToFile(members);

            //aaa
            XDocument document = XDocument.Load(testPath + fileName);

            Assert.IsTrue(File.Exists(testPath + fileName) &&
                document.Descendants("members").Elements().Any(e => e.Attribute("name").Value == "Mark") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").Elements().Any(n => n.Name == "bestValue" && n.Value == "1") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").Elements().Any(n => n.Name == "acceptableValue" && n.Value == "1") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "Mark").Descendants("param").Elements().Any(n => n.Name == "rank" && n.Value == "0.2") &&
                document.Descendants("members").Elements().Any(e => e.Attribute("name").Value == "John") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "John").Descendants("param").Elements().Any(n => n.Name == "bestValue" && n.Value == "2") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "John").Descendants("param").Elements().Any(n => n.Name == "acceptableValue" && n.Value == "2") &&
                document.Descendants("member").Where(m => m.Attribute("name").Value == "John").Descendants("param").Elements().Any(n => n.Name == "rank" && n.Value == "0.4"));
        }
    }
}
