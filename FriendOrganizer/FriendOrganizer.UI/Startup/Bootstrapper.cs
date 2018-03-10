using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModels;

namespace FriendOrganizer.UI.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FriendDataService>().AsImplementedInterfaces();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainWindow>();
            builder.RegisterType<FriendOrganizerDbContext>();

            return builder.Build();
        }
    }
}
