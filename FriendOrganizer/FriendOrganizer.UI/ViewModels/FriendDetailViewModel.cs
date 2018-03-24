using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Wrappers;
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
        private readonly IFriendRepository _friendRepository;
        private readonly IEventAggregator _eventAggregator;
        private bool _hasChanges;

        public FriendWrapper Friend { get; set; }

        public bool HasChanges
        {
            get => _hasChanges;
            private set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        public FriendDetailViewModel(IFriendRepository friendRepository,
            IEventAggregator eventAggregator)
        {
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>()
                .Publish(new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = Friend.FirstName + " " + Friend.LastName,
                });
        }

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue ?
                await _friendRepository.GetByIdAsync(friendId.Value)
                : CreateNewFriend();
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
              {
                  if (!HasChanges)
                  {
                      HasChanges = _friendRepository.HasChanges();
                  }
                  if (e.PropertyName == nameof(Friend.HasErrors))
                  {
                      ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                  }
              };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend
            {

            };
            _friendRepository.Add(friend);
            return friend;
        }
    }
}
