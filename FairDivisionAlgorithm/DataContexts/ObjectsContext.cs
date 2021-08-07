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
                "", "", new int[5]);

            InitializeCollections();
        }

        public void SaveObject(DivisionObject divisionObject)
        {
            if (!DivisionObjects.Any(x => x.ObjectName == divisionObject.ObjectName))
            {
                AddObject(divisionObject);
            }
            else
            {
                UpdateObject(divisionObject);
            }
        }

        private void AddObject(DivisionObject divisionObject)
        {
            DivisionObjects.Add(divisionObject);
        }

        private void UpdateObject(DivisionObject divisionObject)
        {
            DivisionObject objectToUpdate = DivisionObjects.First(x => x.ObjectName == divisionObject.ObjectName);
            int index = DivisionObjects.IndexOf(objectToUpdate);
            DivisionObjects[index] = CurrentObject;
        }

        public void RemoveObject(string name)
        {
            DivisionObject objectToRemove = DivisionObjects.First(x => x.ObjectName == name);

            DivisionObjects.Remove(objectToRemove);
        }

        public DivisionObject ReturnSelectedObject(string name)
        {
            DivisionObject result = new DivisionObject(
                "", "", new int[5]);

            DivisionObject selected = DivisionObjects.First(
                x => x.ObjectName == name.ToString());

            result.ObjectName = name.ToString();
            result.OwnerName = selected.OwnerName;

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
