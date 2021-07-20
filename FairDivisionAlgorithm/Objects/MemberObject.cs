using System.ComponentModel;

namespace FairDivisionAlgorithm
{
    public class MemberObject : INotifyPropertyChanged
    {
        private string name;
        private bool[] lessThan;
        private int[] values;
        private double[] rank;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public bool[] LessThan
        {
            get { return lessThan; }
            set { lessThan = value; OnPropertyChanged("LessThan"); }
        }
        public int[] Values
        {
            get { return values; }
            set { values = value; OnPropertyChanged("Values"); }
        }
        public double[] Rank
        {
            get { return rank; }
            set { rank = value; OnPropertyChanged("Rank"); }
        }

        public MemberObject(string n = "", bool[] lT = null, int[] v = null, double[] r = null)
        {
            name = n;
            lessThan = lT;
            values = v;
            rank = r;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
