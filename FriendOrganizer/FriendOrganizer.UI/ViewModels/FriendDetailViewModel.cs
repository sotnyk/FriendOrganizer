using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Events;
using GalaSoft.MvvmLight;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModels
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private readonly IFriendDataService _dataService;
        private readonly IEventAggregator _eventAggregator;

        public Friend Friend { get; set; }

        public ICommand SaveCommand { get; }            

        public FriendDetailViewModel(IFriendDataService dataService,
            IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailViewAsync);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            return true;
        }

        private async void OnSaveExecute()
        {
            await _dataService.SaveAsync(Friend);
            _eventAggregator.GetEvent<AfterFriendSavedEvent>()
                .Publish(new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = Friend.FirstName + " " + Friend.LastName,
                });
        }

        private async void OnOpenFriendDetailViewAsync(int friendId)
        {
            await LoadAsync(friendId);
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = await _dataService.GetByIdAsync(friendId);
        }
    }
}
