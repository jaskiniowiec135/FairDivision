using FairDivisionAlgorithm;
using FairDivisionAlgorithm.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FairDivision
{
    public partial class MainWindow : Window
    {
        ConfigurationContext configurationContext;
        MembersContext membersContext;
        ObjectsContext objectsContext;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = e.Source as TabControl ;
            TabItem tabItem = new TabItem();

            foreach(TabItem item in tabControl.Items)
            {
                if(item.IsSelected)
                {
                    tabItem = item;
                }
            }

            if (tabItem != null)
            {
                switch (tabItem.Name)
                {
                    case "ConfigurationTab":
                        DataContext = configurationContext;
                        break;
                    case "MembersTab":
                        DataContext = membersContext;
                        break;
                    case "ObjectsTab":
                        DataContext = objectsContext;
                        break;
                }
            }

        }

        private void configComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = Configuration.GetAllConfigurations();
            configComboBox.Items.Clear();

            foreach (var c in configs)
            {
                configComboBox.Items.Add(c);
            }
        }

        private void membersConfigComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = Configuration.GetAllConfigurations();
            membersConfigComboBox.Items.Clear();

            foreach (var c in configs)
            {
                membersConfigComboBox.Items.Add(c);
            }
        }

        private void objectsConfigComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = Configuration.GetAllConfigurations();
            objectsConfigComboBox.Items.Clear();

            foreach (var c in configs)
            {
                objectsConfigComboBox.Items.Add(c);
            }
        }

        private void configGet_Click(object sender, RoutedEventArgs e)
        {
            string name = configComboBox.Text;

            Dictionary<string, string> configuration = Configuration.GetConfiguration(name);

            WriteToConfigurationControls(configuration);
        }

        private void configSave_Click(object sender, RoutedEventArgs e)
        {
            string name = configComboBox.Text;

            Dictionary<string, string> configuration = new Dictionary<string, string>();

            for (int i = 0; i < configurationContext.ConfigParams.Length; i++)
            {
                if (configurationContext.ConfigParams[i] != "" && configurationContext.ConfigUnits[i] != "")
                {
                    configuration.Add(configurationContext.ConfigParams[i], configurationContext.ConfigUnits[i]);
                }
            }

            Configuration.Save(configuration, name);
        }

        private void configRemove_Click(object sender, RoutedEventArgs e)
        {
            string name = configComboBox.Text;

            Configuration.Remove(name);
        }

        private void ReadFromConfigurationControls()
        {
            configurationContext.ConfigParams = new string[5];
            configurationContext.ConfigUnits = new string[5];

            configurationContext.ConfigParams[0] = txtConfigParameter1.Text;
            configurationContext.ConfigParams[1] = txtConfigParameter2.Text;
            configurationContext.ConfigParams[2] = txtConfigParameter3.Text;
            configurationContext.ConfigParams[3] = txtConfigParameter4.Text;
            configurationContext.ConfigParams[4] = txtConfigParameter5.Text;

            configurationContext.ConfigUnits[0] = txtConfigUnit1.Text;
            configurationContext.ConfigUnits[1] = txtConfigUnit2.Text;
            configurationContext.ConfigUnits[2] = txtConfigUnit3.Text;
            configurationContext.ConfigUnits[3] = txtConfigUnit4.Text;
            configurationContext.ConfigUnits[4] = txtConfigUnit5.Text;
        }

        private void WriteToConfigurationControls(Dictionary<string, string> configuration)
        {
            configurationContext.ConfigParams = new string[5];
            configurationContext.ConfigUnits = new string[5];

            for (int i = 0; i < configuration.Count; i++)
            {
                configurationContext.ConfigParams[i] = configuration.Keys.ElementAt(i);
                configurationContext.ConfigUnits[i] = configuration.Values.ElementAt(i);
            }

            txtConfigParameter1.Text = configuration.Keys.ElementAt(0);
            txtConfigParameter2.Text = configuration.Keys.ElementAt(1);
            txtConfigParameter3.Text = configuration.Keys.ElementAt(2);
            txtConfigParameter4.Text = configuration.Keys.ElementAt(3);
            txtConfigParameter5.Text = configuration.Keys.ElementAt(4);

            txtConfigUnit1.Text = configuration.Values.ElementAt(0);
            txtConfigUnit2.Text = configuration.Values.ElementAt(1);
            txtConfigUnit3.Text = configuration.Values.ElementAt(2);
            txtConfigUnit4.Text = configuration.Values.ElementAt(3);
            txtConfigUnit5.Text = configuration.Values.ElementAt(4);

        }

        private void membersAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
