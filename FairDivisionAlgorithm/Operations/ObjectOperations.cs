using System.Collections.Generic;
using System.IO;

namespace FairDivisionAlgorithm
{
    public class ObjectOperations
    {
        static string fileName = "\\Objects.xml";

        public static List<DivisionObject> GetObjects(string name)
        {
            List<DivisionObject> result = new List<DivisionObject>();

            string path = Directory.GetCurrentDirectory();
            path = Path.GetFullPath(Path.Combine(path, @"..\..\..\FairDivisionAlgorithm\\AppData\\", name));

            XMLHandler handler = new XMLHandler(path, fileName);

            if (File.Exists(path + fileName))
            {
                result = handler.GetObjectsFromDocument();
            }

            return result;
        }

        public List<DivisionObject> GetMembers()
        {
            return ListOfObjects;
        }
    }
}
