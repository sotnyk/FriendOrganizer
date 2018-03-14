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
                ValidateProperty();
            }
        }

        private void ValidateProperty([CallerMemberName]string propertyName = null)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (FirstName.StartsWith("R"))
                        AddError(propertyName, "I don't like names started with 'R' ");
                    break;
            }
        }

        public string LastName
        {
            get => Model.LastName;
            set
            {
                Model.LastName = value;
            }
        }

        public string Email
        {
            get => Model.Email;
            set
            {
                Model.Email = value;
            }
        }

        public FriendWrapper(Friend model) : base(model)
        {
        }
    }
}
