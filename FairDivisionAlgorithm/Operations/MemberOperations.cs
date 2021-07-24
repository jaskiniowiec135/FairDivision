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
        static string fileName = "\\Members.xml";

        public static List<MemberObject> GetMembers(string name)
        {
            List<MemberObject> result = new List<MemberObject>();

            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", name));

            XMLHandler handler = new XMLHandler(path, fileName);

            if (File.Exists(path + fileName))
            {
                result = handler.GetMembersFromDocument();
            }

            return result;
        }

        public static void Save(List<MemberObject> members, string name)
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
            else
            {
                Directory.CreateDirectory(path);
            }

            XMLHandler handler = new XMLHandler(path, fileName);

            handler.SaveMembersToFile(members);
        }

        public static void Remove()
        {

        }
    }
}