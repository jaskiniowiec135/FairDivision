using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public class Objects
    {
        private List<DivisionObject> ListOfObjects;

        public void AddObject(DivisionObject member)
        {
            ListOfObjects.Add(member);
        }

        public void ModifyObject(DivisionObject divisionObject)
        {
            int objectIndex = ListOfObjects.FindIndex(o => o.Name == divisionObject.Name);
            ListOfObjects[objectIndex] = divisionObject;
        }

        public void RemoveObject(DivisionObject divisionObject)
        {
            int objectIndex = ListOfObjects.FindIndex(o => o.Name == divisionObject.Name);
            ListOfObjects.RemoveAt(objectIndex);
        }

        public List<DivisionObject> GetMembers()
        {
            return ListOfObjects;
        }
    }
}
