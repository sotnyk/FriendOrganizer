using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModels
{
    public interface IDetailViewModel
    {
        bool HasChanges { get; }
        Task LoadAsync(int? id);
    }
}