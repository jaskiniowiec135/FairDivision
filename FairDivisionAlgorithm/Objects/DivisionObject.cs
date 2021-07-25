using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm
{
    public class DivisionObject : INotifyPropertyChanged
    {
        string objectName;
        string ownerName;
        int value;
        int[] parametersValues;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ObjectName
        {
            get { return objectName; }
            set { objectName = value; OnPropertyChanged("Name"); }
        }

        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; OnPropertyChanged("OwnerName"); }
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; OnPropertyChanged("Value"); }
        }

        public int[] ParametersValues
        {
            get { return parametersValues; }
            set { parametersValues = value; OnPropertyChanged("ParametersValues"); }
        }

        public DivisionObject(string obName, string owName, int v, int[] pValues)
        {
            ObjectName = obName;
            OwnerName = owName;
            Value = v;
            ParametersValues = pValues;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
