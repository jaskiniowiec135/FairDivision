using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FairDivisionAlgorithm.DataContexts
{
    public class ObjectsContext : INotifyPropertyChanged
    {
        DivisionObject currentObject;
        ObservableCollection<DivisionObject> divisionObjects;
        ObservableCollection<string> divisionObjectParams;

        public event PropertyChangedEventHandler PropertyChanged;

        public DivisionObject CurrentObject
        {
            get { return currentObject; }
            set { currentObject = value; OnPropertyChanged("CurrentObject"); }
        }
        public ObservableCollection<DivisionObject> DivisionObjects
        {
            get { return divisionObjects; }
            set { divisionObjects = value; OnPropertyChanged("Objects"); }
        }
        public ObservableCollection<string> DivisionObjectParams
        {
            get { return divisionObjectParams; }
            set { divisionObjectParams = value; OnPropertyChanged("ObjectParams"); }
        }

        public ObjectsContext()
        {
            CurrentObject = new DivisionObject(
                "", "", 0, new int[5]);

            InitializeCollections();
        }
        public DivisionObject ReturnSelectedObject(string name)
        {
            DivisionObject result = new DivisionObject(
                "", "", 0, new int[5]);

            DivisionObject selected = DivisionObjects.First(
                x => x.ObjectName == name.ToString());

            result.ObjectName = name.ToString();
            result.OwnerName = selected.OwnerName;
            result.Value = selected.Value;

            for (int i = 0; i < DivisionObjectParams.Count; i++)
            {
                result.ParametersValues[i] = selected.ParametersValues[i];
            }

            return result;
        }

        private void InitializeCollections()
        {
            DivisionObjects = new ObservableCollection<DivisionObject>();
            DivisionObjectParams = new ObservableCollection<string>();

            DivisionObjectParams.Add("");
            DivisionObjectParams.Add("");
            DivisionObjectParams.Add("");
            DivisionObjectParams.Add("");
            DivisionObjectParams.Add("");
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
