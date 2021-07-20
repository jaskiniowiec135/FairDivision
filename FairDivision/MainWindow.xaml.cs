using FairDivisionAlgorithm;
using FairDivisionAlgorithm.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

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
            InitializeContexts();
        }

        private void InitializeContexts()
        {
            configurationContext = new ConfigurationContext();
            membersContext = new MembersContext();
            objectsContext = new ObjectsContext();
        }

        void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabControl tabControl = e.Source as TabControl;
                TabItem tabItem = new TabItem();

                foreach (TabItem item in tabControl.Items)
                {
                    if (item.IsSelected)
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
        }

        private void configComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = ConfigurationOperations.GetAllConfigurations();
            configComboBox.Items.Clear();

            foreach (var c in configs)
            {
                configComboBox.Items.Add(c);
            }
        }

        private void membersConfigComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = ConfigurationOperations.GetAllConfigurations();
            membersConfigComboBox.Items.Clear();

            foreach (var c in configs)
            {
                membersConfigComboBox.Items.Add(c);
            }
        }

        private void membersConfigComboBox_DropDownClosed(object sender, EventArgs e)
        {
            membersContext.InitializeCollections();

            string name = membersConfigComboBox.Text;

            Dictionary<string, string> configuration = ConfigurationOperations.GetConfiguration(name);

            for (int i = 0; i < configuration.Count; i++)
            {
                membersContext.MemberParams[i] = configuration.Keys.ElementAt(i);
            }
        }

        private void objectsConfigComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = ConfigurationOperations.GetAllConfigurations();
            objectsConfigComboBox.Items.Clear();

            foreach (var c in configs)
            {
                objectsConfigComboBox.Items.Add(c);
            }
        }

        private void configGet_Click(object sender, RoutedEventArgs e)
        {
            configurationContext.InitializeCollections();
            string name = configComboBox.Text;

            Dictionary<string, string> configuration = ConfigurationOperations.GetConfiguration(name);

            for (int i = 0; i < configuration.Count; i++)
            {
                configurationContext.ConfigParams[i] = configuration.Keys.ElementAt(i);
                configurationContext.ConfigUnits[i] = configuration.Values.ElementAt(i);
            }
        }

        private void configSave_Click(object sender, RoutedEventArgs e)
        {
            string name = configComboBox.Text;

            Dictionary<string, string> configuration = new Dictionary<string, string>();

            for (int i = 0; i < configurationContext.ConfigParams.Count; i++)
            {
                if (configurationContext.ConfigParams[i] != "" && configurationContext.ConfigUnits[i] != "")
                {
                    configuration.Add(configurationContext.ConfigParams[i], configurationContext.ConfigUnits[i]);
                }
            }

            ConfigurationOperations.Save(configuration, name);
        }

        private void configRemove_Click(object sender, RoutedEventArgs e)
        {
            string name = configComboBox.Text;

            ConfigurationOperations.Remove(name);
        }

        private void memberAdd_Click(object sender, RoutedEventArgs e)
        {
            int numberOfParams = membersContext.MemberParams.Count;

            MemberObject newMember = new MemberObject("",
                new bool[numberOfParams],
                new int[numberOfParams],
                new double[numberOfParams]);

            newMember.Name = membersContext.CurrentMember.Name;

            for (int i = 0; i < numberOfParams; i++)
            {
                newMember.LessThan[i] = membersContext.CurrentMember.LessThan[i];
                newMember.Values[i] = membersContext.CurrentMember.Values[i];
                newMember.Rank[i] = membersContext.CurrentMember.Rank[i];
            }

            membersContext.AddMember(newMember);
        }

        private void membersComboBox_DropDownOpened(object sender, EventArgs e)
        {
            membersComboBox.Items.Clear();

            Debug.WriteLine("Member params:");

            foreach (var m in membersContext.Members)
            {
                membersComboBox.Items.Add(m.Name);

                Debug.WriteLine(m.Name);

                for (int i = 0; i < m.Values.Length; i++)
                {
                    Debug.WriteLine($"Less than: {m.LessThan[i]}, /t Value: {m.Values[i]}, /t Rank: {m.Rank[i]}");
                }
            }

            Debug.WriteLine("Current member: ");

            for (int i = 0; i < membersContext.CurrentMember.Values.Length; i++)
            {
                Debug.WriteLine($"Less than: {membersContext.CurrentMember.LessThan[i]}, /t Value: {membersContext.CurrentMember.Values[i]}, /t Rank: {membersContext.CurrentMember.Rank[i]}");
            }
        }

        private void membersComboBox_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}
