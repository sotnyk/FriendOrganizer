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
        private readonly string _detailViewModelName;

        public int Id { get; set; }
        public string DisplayMember { get; set; }
        public ICommand OpenDetailViewCommand { get; }

        public NavigationItemViewModel(int id, string displayMember,
            string detailViewModelName,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }

        private void OnOpenDetailViewExecute()
        {
            var args = new OpenDetailViewEventArgs
            {
                Id = Id,
                ViewModelName = _detailViewModelName,
            };
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(args);
        }
    }
}
