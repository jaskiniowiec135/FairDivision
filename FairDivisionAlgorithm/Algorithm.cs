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
        List<MemberRanks> memberRanks;

        public Algorithm(Dictionary<string, string> c, List<MemberObject> m, List<DivisionObject> o)
        {
            configuration = c;
            members = m;
            divisionObjects = o;
            memberRanks = new List<MemberRanks>();
        }

        public void Proceed()
        {
            CalculatePreferences();
            // set random order and proceed acording to the preferences

        }

        private void CalculatePreferences()
        {
            SetBorderValuesForParameters();

            foreach (MemberObject member in members)
            {
                memberRanks.Add(new MemberRanks() { name = member.Name, ranks = new List<double>() });

                foreach (DivisionObject item in divisionObjects)
                {
                    double objectRank = new double();

                    for (int i = 0; i < item.ParametersValues.Length; i++)
                    {
                        if (member.LessThan[i] == true)
                        {
                            double tmp = (member.Values[i] + 1.0) / (item.ParametersValues[i] + 1.0) * member.Rank[i];
                            objectRank += tmp;
                        }
                        else
                        {
                            double tmp = (item.ParametersValues[i] + 1.0) / (member.Values[i] + 1.0) * member.Rank[i];
                            objectRank += tmp;
                        }
                    }

                    memberRanks.Last().ranks.Add(objectRank);
                }
            }
        }

        private void SetBorderValuesForParameters()
        {
            minimumParamValues = new int[5] {
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue};

            maximumParamValues = new int[5] {
                int.MinValue,
                int.MinValue,
                int.MinValue,
                int.MinValue,
                int.MinValue};

            for (int o = 0; o < divisionObjects.Count; o++)
            {
                for (int c = 0; c < configuration.Count; c++)
                {
                    if (minimumParamValues[c] > divisionObjects[o].ParametersValues[c])
                    {
                        minimumParamValues[c] = divisionObjects[o].ParametersValues[c];
                    }

                    if (maximumParamValues[c] < divisionObjects[o].ParametersValues[c])
                    {
                        maximumParamValues[c] = divisionObjects[o].ParametersValues[c];
                    }
                }
            }
        }
    }
}
