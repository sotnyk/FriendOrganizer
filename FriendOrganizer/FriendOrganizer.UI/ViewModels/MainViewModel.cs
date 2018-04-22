using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Views.Services;
using GalaSoft.MvvmLight;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        public INavigationViewModel NavigationViewModel { get; }

        private readonly IMessageDialogService _messageDialogService;

        public Func<IFriendDetailViewModel> FriendDetailViewModelFactory { get; }
        public IDetailViewModel DetailViewModel { get; private set; }

        public ICommand CreateNewFriendCommand { get; }

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
            _eventAggregator.GetEvent<AfterFriendDeletedEvent>()
                .Subscribe(AfterFriendDeleted);

            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);

            NavigationViewModel = navigationViewModel;
        }

        private void AfterFriendDeleted(int friendId)
        {
            DetailViewModel = null;
        }

        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDetailViewAsync(null);
        }

        private async void OnOpenFriendDetailViewAsync(int? friendId)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                if (_messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?",
                    "Question") == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            DetailViewModel = FriendDetailViewModelFactory();
            await DetailViewModel.LoadAsync(friendId);
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
