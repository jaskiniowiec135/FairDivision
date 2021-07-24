using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FairDivisionAlgorithm.DataContexts
{
    public class MembersContext : INotifyPropertyChanged
    {
        MemberObject currentMember;
        ObservableCollection<MemberObject> members;
        ObservableCollection<string> memberParams;

        public event PropertyChangedEventHandler PropertyChanged;

        public MemberObject CurrentMember
        {
            get { return currentMember; }
            set { currentMember = value; OnPropertyChanged("CurrentMember"); }
        }

        public ObservableCollection<MemberObject> Members
        {
            get { return members; }
            set { members = value; OnPropertyChanged("Members"); }
        }

        public ObservableCollection<string> MemberParams
        {
            get { return memberParams; }
            set { memberParams = value; OnPropertyChanged("MemberParams"); }
        }

        public MembersContext()
        {
            Members = new ObservableCollection<MemberObject>();
            CurrentMember = new MemberObject("",
                new bool[5],
                new int[5],
                new double[5]);

            InitializeCollections();
        }

        public void SaveMember(MemberObject member)
        {
            if (!Members.Any(x => x.Name == member.Name))
            {
                AddMember(member);
            }
            else
            {
                UpdateMember(member);
            }
        }

        private void AddMember(MemberObject member)
        {
            Members.Add(member);
        }

        private void UpdateMember(MemberObject member)
        {
            MemberObject memberToUpdate = Members.First(x => x.Name == member.Name);
            int index = Members.IndexOf(memberToUpdate);
            Members[index] = CurrentMember;
        }

        public void RemoveMember(string name)
        {
            MemberObject memberToRemove = members.First(x => x.Name == name);

            Members.Remove(memberToRemove);
        }

        public MemberObject ReturnSelectedCustomer(string name)
        {
            MemberObject result = new MemberObject("",
                    new bool[5],
                    new int[5],
                    new double[5]);

            MemberObject selected = Members.First(
                x => x.Name == name.ToString());

            result.Name = name.ToString();

            for (int i = 0; i < MemberParams.Count; i++)
            {
                result.LessThan[i] = selected.LessThan[i];
                result.Rank[i] = selected.Rank[i];
                result.Values[i] = selected.Values[i];
            }

            return result;
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializeCollections()
        {
            MemberParams = new ObservableCollection<string>();

            MemberParams.Add("");
            MemberParams.Add("");
            MemberParams.Add("");
            MemberParams.Add("");
            MemberParams.Add("");
        }
    }
}
