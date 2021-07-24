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
        static string fileName = "\\Configuration.xml";

        public static Dictionary<string,string> GetConfiguration(string name)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", name));

            XMLHandler handler = new XMLHandler(path, fileName);

            if(File.Exists(path + fileName))
            {
                result = handler.GetConfigurationFromDocument();
            }

            return result;
        }

        public static void Save(Dictionary<string,string> config, string name)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", name));

            if(Directory.Exists(path))
            {
                if (File.Exists(path + fileName))
                {
                    File.Delete(path + fileName);
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            XMLHandler handler = new XMLHandler(path, fileName);

            handler.SaveConfigurationToFile(config);
        }

        public static void Remove(string name)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", name));

            if (Directory.Exists(path))
            {
                if (File.Exists(path + fileName))
                {
                    File.Delete(path + fileName);
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
