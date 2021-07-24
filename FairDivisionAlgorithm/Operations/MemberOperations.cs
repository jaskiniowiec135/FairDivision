using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public class MemberOperations
    {
        public static List<MemberObject> GetMembers(string name)
        {
            List<MemberObject> result = new List<MemberObject>();

            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", name));

            XMLHandler handler = new XMLHandler(path, "\\Members.xml");

            if (File.Exists(path + "\\Members.xml"))
            {
                result = handler.GetMembersFromDocument();
            }

            return result;
        }

        public static void Save(List<MemberObject> members)
        {

        }

        public static void Remove()
        {

        }
    }
}