using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public class Members
    {
        private List<Member> ListOfMembers;

        public void AddMembers(Member member)
        {
            ListOfMembers.Add(member);
        }

        public void ModifyMember(Member member)
        {
            int memberIndex = ListOfMembers.FindIndex(m => m.Name == member.Name);
            ListOfMembers[memberIndex] = member;
        }

        public void RemoveMember(int id)
        {
            ListOfMembers.RemoveAt(id);
        }

        public List<Member> GetMembers()
        {
            return ListOfMembers;
        }
    }
}
