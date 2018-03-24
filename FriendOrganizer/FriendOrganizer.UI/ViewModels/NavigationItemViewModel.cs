using FriendOrganizer.UI.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModels
{
    public class NavigationItemViewModel: BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public int Id { get; set; }
        public string DisplayMember { get; set; }
        public ICommand OpenFriendDetailViewCommand { get; }

        public NavigationItemViewModel(int id, string displayMember,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;
            OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailView);
        }

        private void OnOpenFriendDetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(Id);
        }
    }
}
