using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FriendOrganizer.UI.Wrappers
{
    public class ModelWrapper<T> : NotifyDataErrorInfoBase
    {
        public T Model { get; }

        public ModelWrapper(T model)
        {
            Model = model;
        }

        protected void ValidatePropertyInternal(object currentValue, [CallerMemberName]string propertyName = null)
        {
            ClearErrors(propertyName);
            ValidateDataAnnotations(propertyName, currentValue);
            ValidateCustomErrors(propertyName);
        }

        private void ValidateDataAnnotations(string propertyName, object currentValue)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(Model) { MemberName = propertyName };
            Validator.TryValidateProperty(currentValue, context, results);

            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }

        private void ValidateCustomErrors(string propertyName)
        {
            var errors = ValidateProperty(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        protected virtual IEnumerable<string> ValidateProperty(string propertyName) => null;

    }
}
