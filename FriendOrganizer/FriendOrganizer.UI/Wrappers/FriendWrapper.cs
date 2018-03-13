using FriendOrganizer.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Wrappers
{
    public class FriendWrapper: ViewModelBase, INotifyDataErrorInfo
    {
        public Friend Model { get; }

        public int Id => Model.Id;

        public string FirstName {
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

        public FriendWrapper(Friend model)
        {
            Model = model;
        }

        #region INotifyDataErrorInfo
        private Dictionary<string, List<string>> _errorsByPropertyName =
            new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            _errorsByPropertyName.TryGetValue(propertyName, out var errors);
            return errors;                
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            Console.WriteLine("Errors changed for: " + propertyName);
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.TryGetValue(propertyName, out var errors))
                _errorsByPropertyName.Add(propertyName, errors = new List<string>());
            if (!errors.Contains(error))
            {
                errors.Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
        #endregion
    }
}
