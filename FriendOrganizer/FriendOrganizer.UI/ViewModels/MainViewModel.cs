using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        public async Task LoadAsync()
        {
            var friends = await _friendDataService.GetAllAsync();
            Friends.Clear();
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }
    }
}
