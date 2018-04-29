using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Wrappers
{
    public class MeetingWrapper : ModelWrapper<Meeting>
    {
        public int Id => Model.Id;

        public string Title
        {
            get => Model.Title;
            set
            {
                Model.Title = value;
                ValidatePropertyInternal(value);
            }
        }

        public DateTime DateFrom
        {
            get => Model.DateFrom;
            set
            {
                Model.DateFrom = value;
                ValidatePropertyInternal(value);
                if (DateTo < DateFrom)
                {
                    DateTo = DateFrom;
                }
            }
        }

        public DateTime DateTo
        {
            get => Model.DateTo;
            set
            {
                Model.DateTo = value;
                ValidatePropertyInternal(value);
                if (DateTo < DateFrom)
                {
                    DateFrom = DateTo;
                }
            }
        }

        public MeetingWrapper(Meeting model) : base(model) { }

    }
}
