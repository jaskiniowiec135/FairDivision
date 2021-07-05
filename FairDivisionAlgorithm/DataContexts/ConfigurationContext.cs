using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm.DataContexts
{
    public class ConfigurationContext
    {
        string[] configParams;
        string[] configUnits;

        public string[] ConfigParams
        {
            get { return configParams; }
            set { configParams = value; }
        }
        public string[] ConfigUnits
        {
            get { return configUnits; }
            set { configUnits = value; }
        }
    }
}
