using System.Collections.Generic;

namespace FairDivisionAlgorithm.Validators
{
    public static class ConfigurationValidator
    {
        public static bool IsValid(List<string> parameters, List<string> units)
        {
            if (parameters.Count == units.Count)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (parameters[i] == "" | units[i] == "")
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }
    }
}
