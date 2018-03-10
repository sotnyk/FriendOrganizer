using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace FriendOrganizer.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFriendDataService _friendDataService;

        public ObservableCollection<Friend> Friends { get; set; } = new ObservableCollection<Friend>();
        public Friend SelectedFriend { get; set; }

        public MainViewModel(IFriendDataService friendDataService)
        {
            _friendDataService = friendDataService;
        }

        public void Load()
        {
            var friends = _friendDataService.GetAll();
            Friends.Clear();
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }
    }
}
