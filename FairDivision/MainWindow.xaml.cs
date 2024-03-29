﻿using FairDivisionAlgorithm;
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

            configurationContext.SetConfiguration(
                ConfigurationOperations.GetConfiguration(name));
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
            membersComboBox.Items.Clear();
            membersContext.InitializeObjects();

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

        private void memberSave_Click(object sender, RoutedEventArgs e)
        {
            int numberOfParams = membersContext.MemberParams.Count;

            MemberObject member = new MemberObject("",
                new int[numberOfParams],
                new int[numberOfParams],
                new double[numberOfParams]);

            member.Name = membersComboBox.Text;

            for (int i = 0; i < numberOfParams; i++)
            {
                member.AcceptableValues[i] = membersContext.CurrentMember.AcceptableValues[i];
                member.BestValues[i] = membersContext.CurrentMember.BestValues[i];
                member.Rank[i] = membersContext.CurrentMember.Rank[i];
            }

            membersContext.SaveMember(member);
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

        private void membersRemove_Click(object sender, RoutedEventArgs e)
        {
            MemberOperations.Remove(membersConfigComboBox.SelectedItem.ToString());
        }

        #endregion

        #region ObjectsTab

        private void objectsConfigComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = ConfigurationOperations.GetAllConfigurations();
            objectsConfigComboBox.Items.Clear();

            foreach (var c in configs)
            {
                objectsConfigComboBox.Items.Add(c);
            }
        }

        private void objectsConfigComboBox_DropDownClosed(object sender, EventArgs e)
        {
            string name = objectsConfigComboBox.Text;
            objectsNameComboBox.Text = "";
            objectsContext.InitializeObjects();

            Dictionary<string, string> configuration = ConfigurationOperations.GetConfiguration(name);

            for (int i = 0; i < configuration.Count; i++)
            {
                objectsContext.DivisionObjectParams[i] = configuration.Keys.ElementAt(i);
            }

            List<DivisionObject> objects = ObjectOperations.GetObjects(name);

            objectsContext.DivisionObjects.Clear();

            foreach (var item in objects)
            {
                objectsContext.DivisionObjects.Add(item);
            }

            objectsOwnerComboBox.Items.Clear();
            objectsOwnerComboBox.Items.Add("");

            List<string> memberNames = MemberOperations.GetMembers(name).Select(x => x.Name.ToString()).ToList();

            foreach(var member in memberNames)
            {
                objectsOwnerComboBox.Items.Add(member);
            }
        }

        private void objectsComboBox_DropDownOpened(object sender, EventArgs e)
        {
            objectsNameComboBox.Items.Clear();

            foreach (var m in objectsContext.DivisionObjects)
            {
                objectsNameComboBox.Items.Add(m.ObjectName);
            }
        }

        private void objectsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            var name = objectsNameComboBox.SelectedItem;

            if (name != null)
            {
                objectsContext.CurrentObject = objectsContext.ReturnSelectedObject(name.ToString());
            }

            objectsOwnerComboBox.SelectedItem = objectsContext.CurrentObject.OwnerName;
        }

        private void objectSave_Click(object sender, RoutedEventArgs e)
        {
            int numberOfParams = objectsContext.DivisionObjectParams.Count;

            DivisionObject divisionObject = new DivisionObject(
                "", "", new int[5]);

            divisionObject.ObjectName = objectsNameComboBox.Text;
            divisionObject.OwnerName = objectsOwnerComboBox.Text;

            for (int i = 0; i < numberOfParams; i++)
            {
                divisionObject.ParametersValues[i] = objectsContext.CurrentObject.ParametersValues[i];
            }

            objectsContext.SaveObject(divisionObject);
        }

        private void objectRemove_Click(object sender, RoutedEventArgs e)
        {
            var name = objectsNameComboBox.SelectedItem;

            if(name != null && objectsContext.DivisionObjects.Any(
                x => x.ObjectName == name.ToString()))
            {
                objectsContext.RemoveObject(name.ToString());

                objectsNameComboBox.Text = "";
                objectsContext.CurrentObject = new DivisionObject();
            }
        }

        private void objectsSave_Click(object sender, RoutedEventArgs e)
        {
            ObjectOperations.Save(objectsContext.DivisionObjects.ToList(),
                objectsConfigComboBox.SelectedItem.ToString());
        }

        private void objectsRemove_Click(object sender, RoutedEventArgs e)
        {
            ObjectOperations.Remove(objectsConfigComboBox.Text);
        }


        #endregion

        #region Algorithm

        private void algorithmComboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> configs = ConfigurationOperations.GetAllConfigurations();
            algorithmComboBox.Items.Clear();

            foreach (var c in configs)
            {
                algorithmComboBox.Items.Add(c);
            }
        }

        private void algorithmRun_Click(object sender, RoutedEventArgs e)
        {
            string name = algorithmComboBox.Text;

            Algorithm algorithm = new Algorithm(
                ConfigurationOperations.GetConfiguration(name),
                MemberOperations.GetMembers(name),
                ObjectOperations.GetObjects(name));

            Dictionary<string,string> returnedMembers = algorithm.Proceed();

            string result = $"Result of algorithm run:\n";

            foreach (var item in returnedMembers)
            {
                result += $"{item.Key} = {item.Value}\n";
            }

            MessageBox.Show(result, "Algorithm result");
        }

        #endregion
    }
}
