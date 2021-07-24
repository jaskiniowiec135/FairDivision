using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FairDivisionAlgorithm
{
    public static class ConfigurationOperations
    {
        public static Dictionary<string,string> GetConfiguration(string name)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", name));

            XMLHandler handler = new XMLHandler(path, "\\Configuration.xml");

            if(File.Exists(path + "\\Configuration.xml"))
            {
                result = handler.GetConfigurationFromDocument();
            }

            return result;
        }

        public static void Save(Dictionary<string,string> config, string caseName)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", caseName));

            if(Directory.Exists(path))
            {
                if (File.Exists(path + "\\Configuration.xml"))
                {
                    File.Delete(path + "\\Configuration.xml");
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            XMLHandler handler = new XMLHandler(path, "\\Configuration.xml");

            handler.SaveConfigurationToFile(config);
        }

        public static void Remove(string caseName)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", caseName));

            if (Directory.Exists(path))
            {
                if (File.Exists(path + "\\Configuration.xml"))
                {
                    File.Delete(path + "\\Configuration.xml");
                }
            }

            Directory.Delete(path);
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
