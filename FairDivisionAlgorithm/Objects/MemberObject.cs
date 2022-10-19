using System.ComponentModel;

namespace FairDivisionAlgorithm.Objects
{
    public class MemberObject : INotifyPropertyChanged
    {
        private string name;
        private int[] acceptableValues;
        private int[] bestValues;
        private double[] ranks;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public int[] AcceptableValues
        {
            get { return acceptableValues; }
            set { acceptableValues = value; OnPropertyChanged("LessThan"); }
        }
        public int[] BestValues
        {
            get { return bestValues; }
            set { bestValues = value; OnPropertyChanged("Values"); }
        }
        public double[] Rank
        {
            get { return ranks; }
            set { ranks = value; OnPropertyChanged("Rank"); }
        }

        /// <summary>
        /// Member object storing member preferences.
        /// </summary>
        /// <param name="n">Name of member.</param>
        /// <param name="acceptable">Array of acceptable values for parameters.</param>
        /// <param name="best">Array of best values for parameters.</param>
        /// <param name="r">Array of ranks of objects parameters.</param>
        public MemberObject(string n = "", int[] acceptable = null, int[] best = null, double[] r = null)
        {
            name = n;
            acceptableValues = acceptable;
            bestValues = best;
            ranks = r;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
