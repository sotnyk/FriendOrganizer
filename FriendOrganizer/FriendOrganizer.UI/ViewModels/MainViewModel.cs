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

        private readonly Func<IFriendDetailViewModel> _friendDetailViewModelFactory;

        private readonly Func<IMeetingDetailViewModel> _meetingDetailViewModelFactory;

        public IDetailViewModel DetailViewModel { get; private set; }

        public ICommand CreateNewDetailCommand { get; }

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelFactory,
            Func<IMeetingDetailViewModel> meetingDetailViewModelFactory,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
            _friendDetailViewModelFactory = friendDetailViewModelFactory;
            _meetingDetailViewModelFactory = meetingDetailViewModelFactory;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailViewAsync);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            NavigationViewModel = navigationViewModel;
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            DetailViewModel = null;
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            var args = new OpenDetailViewEventArgs
            {
                ViewModelName = viewModelType.Name,
            };
            OnOpenDetailViewAsync(args);
        }

        private async void OnOpenDetailViewAsync(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                if (_messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?",
                    "Question") == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            switch (args.ViewModelName) {
                case nameof(FriendDetailViewModel):
                    DetailViewModel = _friendDetailViewModelFactory();
                    break;
                case nameof(MeetingDetailViewModel):
                    DetailViewModel = _meetingDetailViewModelFactory();
                    break;
                default:
                    throw new Exception($"ViewModel '{args.ViewModelName}' not mapped.");
            }
            await DetailViewModel.LoadAsync(args.Id);
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
