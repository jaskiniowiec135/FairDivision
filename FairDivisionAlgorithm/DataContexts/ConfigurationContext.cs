using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairDivisionAlgorithm.DataContexts
{
    public class ConfigurationContext : INotifyPropertyChanged
    {
        ObservableCollection<string> configParams;
        ObservableCollection<string> configUnits;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> ConfigParams
        {
            get { return configParams; }
            set { configParams = value; OnPropertyChanged("ConfigParams"); }
        }
        public ObservableCollection<string> ConfigUnits
        {
            get { return configUnits; }
            set { configUnits = value; OnPropertyChanged("ConfigUnits"); }
        }

        public ConfigurationContext()
        {
            InitializeCollections();
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializeCollections()
        {
            ConfigParams = new ObservableCollection<string>();
            ConfigUnits = new ObservableCollection<string>();

            ConfigParams.Add("");
            ConfigParams.Add("");
            ConfigParams.Add("");
            ConfigParams.Add("");
            ConfigParams.Add("");

            ConfigUnits.Add("");
            ConfigUnits.Add("");
            ConfigUnits.Add("");
            ConfigUnits.Add("");
            ConfigUnits.Add("");
        }
    }
}
