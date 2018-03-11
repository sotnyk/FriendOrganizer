using Prism.Mvvm;

namespace FriendOrganizer.UI.ViewModels
{
    public class NavigationItemViewModel: BindableBase
    {
        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }

        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
