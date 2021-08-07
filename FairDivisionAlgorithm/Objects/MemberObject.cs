using System.ComponentModel;

namespace FairDivisionAlgorithm
{
    public class MemberObject : INotifyPropertyChanged
    {
        private string name;
        private int[] acceptable;
        private int[] best;
        private double[] rank;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public int[] AcceptableValues
        {
            get { return acceptable; }
            set { acceptable = value; OnPropertyChanged("LessThan"); }
        }
        public int[] BestValues
        {
            get { return best; }
            set { best = value; OnPropertyChanged("Values"); }
        }
        public double[] Rank
        {
            get { return rank; }
            set { rank = value; OnPropertyChanged("Rank"); }
        }

        public MemberObject(string n = "", int[] a = null, int[] b = null, double[] r = null)
        {
            name = n;
            acceptable = a;
            best = b;
            rank = r;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
