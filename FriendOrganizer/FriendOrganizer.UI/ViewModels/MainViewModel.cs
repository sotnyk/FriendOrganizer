using FriendOrganizer.UI.Events;
using GalaSoft.MvvmLight;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        public INavigationViewModel NavigationViewModel { get; }
        public Func<IFriendDetailViewModel> FriendDetailViewModelFactory { get; }
        public IFriendDetailViewModel FriendDetailViewModel { get; private set; }

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelFactory,
            IEventAggregator eventAggregator)
        {
            FriendDetailViewModelFactory = friendDetailViewModelFactory;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailViewAsync);

            NavigationViewModel = navigationViewModel;
        }

        private async void OnOpenFriendDetailViewAsync(int friendId)
        {
            FriendDetailViewModel = FriendDetailViewModelFactory();
            await FriendDetailViewModel.LoadAsync(friendId);
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
