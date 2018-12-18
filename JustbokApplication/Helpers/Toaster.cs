using System;
using System.ComponentModel;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace JustbokApplication.Helpers
{
    public class Toaster : INotifyPropertyChanged
    {
        private readonly Notifier _notifier;

        MessageOptions options = null;

        public Toaster()
        {
            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new PrimaryScreenPositionProvider(
                    corner: Corner.TopRight,
                    offsetX: 5,
                    offsetY: 5);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(6));

                cfg.Dispatcher = Application.Current.Dispatcher;

                cfg.DisplayOptions.TopMost = true;
                cfg.DisplayOptions.Width = 250;
            });

            _notifier.ClearMessages();

            options = new MessageOptions
            {
                FreezeOnMouseEnter = false,
                UnfreezeOnMouseLeave = false,
                ShowCloseButton = true
            };
        }

        public void OnUnloaded()
        {
            _notifier.Dispose();
        }

        public void ShowInformation(string message,MessageOptions opt=null)
        {
            if (opt != null)
            {
                _notifier.ShowInformation(message, opt);
            }
            else
            {
                _notifier.ShowInformation(message, options);
            }
        }

        public void ShowSuccess(string message, MessageOptions opt = null)
        {
            if (opt != null)
            {
                _notifier.ShowSuccess(message, opt);
            }
            else
            {
                _notifier.ShowSuccess(message, options);
            }
        }

        public void ShowWarning(string message, MessageOptions opt = null)
        {
            if (opt != null)
            {
                _notifier.ShowWarning(message, opt);
            }
            else
            {
                _notifier.ShowWarning(message, options);
            }
        }

        public void ShowError(string message, MessageOptions opt = null)
        {
            if (opt != null)
            {
                _notifier.ShowError(message, opt);
            }
            else
            {
                _notifier.ShowError(message, options);
            }
        }

        internal void ClearMessages(string msg)
        {
            _notifier.ClearMessages(msg);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
