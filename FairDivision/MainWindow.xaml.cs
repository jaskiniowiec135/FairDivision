using FairDivisionAlgorithm;
using FairDivisionAlgorithm.DataContexts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        #region ConfigTab

        private void configComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = ConfigurationOperations.GetAllConfigurations();
            configComboBox.Items.Clear();

            foreach (var c in configs)
            {
                configComboBox.Items.Add(c);
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

        #endregion

        #region MemberTab

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

            List<MemberObject> members = MemberOperations.GetMembers(name);

            foreach (var item in members)
            {
                membersContext.Members.Add(item);
            }
        }

        private void memberSave_Click(object sender, RoutedEventArgs e)
        {
            int numberOfParams = membersContext.MemberParams.Count;

            MemberObject member = new MemberObject("",
                new bool[numberOfParams],
                new int[numberOfParams],
                new double[numberOfParams]);

            member.Name = membersComboBox.Text;

            for (int i = 0; i < numberOfParams; i++)
            {
                member.LessThan[i] = membersContext.CurrentMember.LessThan[i];
                member.Values[i] = membersContext.CurrentMember.Values[i];
                member.Rank[i] = membersContext.CurrentMember.Rank[i];
            }

            membersContext.SaveMember(member);
        }

        private void membersComboBox_DropDownOpened(object sender, EventArgs e)
        {
            membersComboBox.Items.Clear();

            foreach (var m in membersContext.Members)
            {
                membersComboBox.Items.Add(m.Name);
            }
        }

        private void membersComboBox_DropDownClosed(object sender, EventArgs e)
        {
            var name = membersComboBox.SelectedItem;

            if (name != null)
            {
                membersContext.CurrentMember = membersContext.ReturnSelectedCustomer(name.ToString());
            }
        }

        private void memberRemove_Click(object sender, RoutedEventArgs e)
        {
            var name = membersComboBox.SelectedItem;

            if (name != null && membersContext.Members.Any(
                x => x.Name == name.ToString()))
            {
                membersContext.RemoveMember(name.ToString());

                membersComboBox.Text = "";
                membersContext.CurrentMember = new MemberObject();
            }
        }

        private void membersSave_Click(object sender, RoutedEventArgs e)
        {
            MemberOperations.Save(membersContext.Members.ToList(), 
                membersConfigComboBox.SelectedItem.ToString());
        }
    }
}
