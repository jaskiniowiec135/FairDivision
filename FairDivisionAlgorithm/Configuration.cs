using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public static class Configuration
    {
        private static Dictionary<string, string> _currentConfiguration;
        
        public static Dictionary<string,string> GetConfiguration()
        {
            return _currentConfiguration;
        }

        public static void Add(Dictionary<string,string> config)
        {
            // save config to new file
        }

        public static void Remove(string configName)
        {
            // remove file with given name
        }

        public static void Modify(Dictionary<string,string> modifiedConfig)
        {
            // overwrite existing file
        }

        public static List<string> GetAllConfigurations()
        {
            List<string> configurations = new List<string>();

            // get config file names to list

            return configurations;
        }
    }
}
