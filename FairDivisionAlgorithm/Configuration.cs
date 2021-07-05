using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public static class Configuration
    {
        private static Dictionary<string, string> _currentConfiguration;
        
        public static Dictionary<string,string> GetCurrentConfiguration()
        {
            return _currentConfiguration;
        }

        public static Dictionary<string,string> GetConfiguration(string caseName)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", caseName));

            XMLHandler handler = new XMLHandler(path, "\\Configuration.xml");

            if(File.Exists(path + "\\Configuration.xml"))
            {
                result = handler.ReadFromFile();
            }

            return result;
        }

        public static void Save(Dictionary<string,string> config, string caseName)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", caseName));
            string[] attributeNames = new string[] { "name", "measureUnit" };

            if (File.Exists(path + "\\Configuration.xml"))
            {
                File.Delete(path + "\\Configuration.xml");
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            XMLHandler handler = new XMLHandler(path, "\\Configuration.xml");

            handler.SaveToFile(config, attributeNames);
        }

        public static void Remove(string caseName)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", caseName));

            if (File.Exists(path + "\\Configuration.xml"))
            {
                File.Delete(path + "\\Configuration.xml");
            }
        }

        public static List<string> GetAllConfigurations()
        {
            List<string> configurations = new List<string>();
            string path = "";

            path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\", "FairDivisionAlgorithm\\AppData"));

            var files = Directory.GetDirectories(path);

            foreach(var f in files)
            {
                int index = f.LastIndexOf("\\");
                string name = f.Substring(index + 1);
                configurations.Add(name);
            }

            return configurations;
        }
    }
}
