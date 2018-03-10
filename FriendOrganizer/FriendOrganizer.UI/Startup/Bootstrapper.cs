using Autofac;
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

            return builder.Build();
        }
    }
}
