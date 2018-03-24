using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    public interface IFriendDetailViewModel
    {
        bool HasChanges { get; }
        Task LoadAsync(int friendId);
    }
}