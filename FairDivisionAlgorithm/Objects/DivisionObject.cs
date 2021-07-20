using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public class DivisionObject
    {
        public string Name;
        public List<int> ParametersValues;

        public DivisionObject(string name, List<int> values)
        {
            Name = name;
            ParametersValues = values;
        }
    }
}
