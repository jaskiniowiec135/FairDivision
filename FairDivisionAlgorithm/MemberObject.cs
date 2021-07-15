using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public class MemberObject
    {
        public string Name;
        public bool[] LessThan;
        public int[] Values;
        public double[] Rank;
        
        public MemberObject(string name = null, bool[] lessThan = null, int[] values = null, double[] rank = null)
        {
            Name = name;
            LessThan = lessThan;
            Values = values;
            Rank = rank;
        }
    }
}
