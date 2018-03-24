using FriendOrganizer.Model;
using System.Runtime.CompilerServices;

namespace FriendOrganizer.UI.Wrappers
{
    public class FriendWrapper : ModelWrapper<Friend>
    {
        public int Id => Model.Id;

        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                Model.FirstName = value;
                ValidatePropertyInternal(value);
            }
        }

        public string LastName
        {
            get => Model.LastName;
            set
            {
                Model.LastName = value;
                ValidatePropertyInternal(value);
            }
        }

        public string Email
        {
            get => Model.Email;
            set
            {
                Model.Email = value;
                ValidatePropertyInternal(value);
            }
        }

        public int? FavoriteLanguageId
        {
            get => Model.FavoriteLanguageId;
            set
            {
                Model.FavoriteLanguageId = value;
            }
        }

        public FriendWrapper(Friend model) : base(model)
        {
        }
    }
}
