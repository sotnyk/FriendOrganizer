using FriendOrganizer.Model;
using System.Runtime.CompilerServices;

namespace FriendOrganizer.UI.Wrappers
{
    public class FriendPhoneNumberWrapper : ModelWrapper<FriendPhoneNumber>
    {
        public string Number
        {
            get => Model.Number;
            set
            {
                Model.Number = value;
                ValidatePropertyInternal(value);
            }
        }

        public FriendPhoneNumberWrapper(FriendPhoneNumber model) : base(model)
        {
        }
    }
}
