﻿using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Events;
using GalaSoft.MvvmLight;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IFriendLookupDataService _friendLookupDataService;
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemViewModel> Friends { get; } 
            = new ObservableCollection<NavigationItemViewModel>();

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService,
            IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Subscribe(AfterFriendSaved);
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs obj)
        {
            var lookupItem = Friends.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                    _eventAggregator));
            }
        }
    }
}
