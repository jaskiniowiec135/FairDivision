using System;
using System.Collections.Generic;
using System.Linq;

namespace FairDivisionAlgorithm
{
    public class Algorithm
    {
        Dictionary<string, string> configuration;
        List<MemberObject> members;
        List<DivisionObject> divisionObjects;
        int[] minimumParamValues;
        int[] maximumParamValues;
        List<MemberRanks> membersRanks;
        List<string> membersOrder;

        public Algorithm(Dictionary<string, string> c, List<MemberObject> m, List<DivisionObject> o)
        {
            configuration = c;
            members = m;
            divisionObjects = o;
            membersRanks = new List<MemberRanks>();
        }

        public Dictionary<string, string> Proceed()
        {
            Prepare();

            return FindBestObjects();
        }

        private Dictionary<string, string> FindBestObjects()
        {
            Dictionary<string, string> objectsAndOwners = divisionObjects.ToDictionary(x => x.ObjectName, x => x.OwnerName);
            List<string> passedInLineMembers = new List<string>();
            int choiceNumber = 0;

            while (membersOrder.Count > 0)
            {
                string desiredObjectName = membersRanks.Where(x => x.name == membersOrder[0])
                    .First().objectNames[choiceNumber];

                if (objectsAndOwners.Any(x => x.Key == desiredObjectName && x.Value == membersOrder[0]))
                {
                    membersOrder.RemoveAt(0);
                    passedInLineMembers = new List<string>();
                    choiceNumber = 0;
                }
                else if (objectsAndOwners.Any(x => x.Key == desiredObjectName && x.Value == ""))
                {
                    if(objectsAndOwners.Any(x => x.Value == membersOrder[0]))
                    {
                        objectsAndOwners[objectsAndOwners.Where(x => x.Value == membersOrder[0]).First().Key] = "";
                    }

                    objectsAndOwners[desiredObjectName] = membersOrder[0];
                    membersOrder.RemoveAt(0);
                    passedInLineMembers = new List<string>();
                    choiceNumber = 0;
                }
                else
                {
                    if(membersOrder.Contains(objectsAndOwners[desiredObjectName]))
                    {
                        if (!passedInLineMembers.Contains(objectsAndOwners[desiredObjectName]))
                        {
                            passedInLineMembers.Add(membersOrder[0]);

                            string nextMember = objectsAndOwners[desiredObjectName];

                            membersOrder.Remove(nextMember);
                            membersOrder.Insert(0, nextMember);
                            choiceNumber = 0;
                        }
                        else
                        {
                            objectsAndOwners[objectsAndOwners.Where(x => x.Value == membersOrder[0]).First().Value] = "";
                            objectsAndOwners[desiredObjectName] = membersOrder[0];
                            passedInLineMembers = new List<string>();
                            membersOrder.RemoveAt(0);
                            choiceNumber = 0;
                        }
                    }
                    else
                    {
                        choiceNumber += 1;
                    }
                }
            }

            return objectsAndOwners;
        }

        private void Prepare()
        {
            CalculatePreferences();
            SetMembersOrder();
        }

        private void SetMembersOrder()
        {
            Random r = new Random();
            membersOrder = members.Select(x => x.Name).OrderBy(o => r.Next()).ToList();
        }

        private void CalculatePreferences()
        {
            foreach (MemberObject member in members)
            {
                membersRanks.Add(new MemberRanks() { name = member.Name, objectNames = new List<string>() });

                Dictionary<string, double> ranks = new Dictionary<string, double>();

                foreach (DivisionObject item in divisionObjects)
                {
                    double objectRank = new double();

                    for (int i = 0; i < item.ParametersValues.Length; i++)
                    {
                        int[] tmpArr = new int[2] { member.BestValues[i], member.AcceptableValues[i] };

                        if(item.ParametersValues[i] <= tmpArr.Max() &&
                            item.ParametersValues[i] >= tmpArr.Min())
                        {
                            if(member.BestValues[i] > member.AcceptableValues[i])
                            {
                                double tmp = ((item.ParametersValues[i] + 1.0) / (member.BestValues[i] + 1.0)) * member.Rank[i];
                                objectRank += tmp;
                            }
                            else
                            {
                                double tmp = ((member.BestValues[i] + 1.0) / (item.ParametersValues[i] + 1.0)) * member.Rank[i];
                                objectRank += tmp;
                            }
                        }
                    }

                    ranks.Add(item.ObjectName, objectRank);
                }

                while (ranks.Count > 0)
                {
                    string objectName = ranks.Where(x => x.Value == ranks.Values.Max()).FirstOrDefault().Key;
                    membersRanks.Last().objectNames.Add(objectName);
                    ranks.Remove(objectName);
                }
            }
        }
    }
}
