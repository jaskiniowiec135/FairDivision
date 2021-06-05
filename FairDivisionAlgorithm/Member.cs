using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public class Member
    {
        public string Name;
        public List<int> Preferences;
        
        public Member(string name, List<int> preferences)
        {
            Name = name;
            Preferences = preferences;
        }
    }
}
