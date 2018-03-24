using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Events;
using FriendOrganizer.UI.Wrappers;
using GalaSoft.MvvmLight;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModels
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IEventAggregator _eventAggregator;

        public FriendWrapper Friend { get; set; }

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
            // TODO: Check in addition if friend has changes
            return Friend != null && !Friend.HasErrors;
        }

        private async void OnSaveExecute()
        {
            await _friendRepository.SaveAsync();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>()
                .Publish(new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = Friend.FirstName + " " + Friend.LastName,
                });
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = new FriendWrapper(await _friendRepository.GetByIdAsync(friendId));
            Friend.PropertyChanged += (s, e) =>
              {
                  if (e.PropertyName == nameof(Friend.HasErrors))
                  {
                      ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                  }
              };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
