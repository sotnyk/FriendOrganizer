using System;
using System.Windows;

namespace FriendOrganizer.UI.Views.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var res = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK) return MessageDialogResult.Ok;
            if (res == MessageBoxResult.Cancel) return MessageDialogResult.Cancel;
            throw new NotImplementedException("Unexpected dialog result: " + res);
        }

        public void ShowInfoDialog(string text)
        {
            MessageBox.Show(text, "Info");
        }
    }
}
