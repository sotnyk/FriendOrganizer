using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.ViewModels;
using FriendOrganizer.UI.Views.Services;
using Prism.Events;

namespace FriendOrganizer.UI.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();


            builder.RegisterType<FriendRepository>().AsImplementedInterfaces();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();

            builder.RegisterType<MessageDialogService>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
