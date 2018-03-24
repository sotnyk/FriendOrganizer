using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Views.Services;
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

        private readonly IMessageDialogService _messageDialogService;

        public Func<IFriendDetailViewModel> FriendDetailViewModelFactory { get; }
        public IFriendDetailViewModel FriendDetailViewModel { get; private set; }

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelFactory,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
            FriendDetailViewModelFactory = friendDetailViewModelFactory;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailViewAsync);

            NavigationViewModel = navigationViewModel;
        }

        private async void OnOpenFriendDetailViewAsync(int friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                if (_messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?",
                    "Question") == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = FriendDetailViewModelFactory();
            await FriendDetailViewModel.LoadAsync(friendId);
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
