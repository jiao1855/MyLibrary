using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace HSBC.INS.Utils.Wpf
{
    public class MessageColors
    {
        public const string Error = "Red";
        public const string Success = "Green";
        public const string Info = "Black";
        public const string Warning = "Orange";
    }

    public class Message
    {
        public Message(string message,string color)
        {
            this.Text = message;
            this.Color = color;
            this.CreateTime = DateTime.Now;
        }

        public string Text { get; set; }

        public DateTime CreateTime { get; private set; }

        public string Color { get; set; }
    }

    public class AppMessage
    {
        private ObservableCollection<Message> messages;

        public AppMessage(ObservableCollection<Message> messages)
        {
            this.messages = messages;
        }

        public void Error(string msg)
        {
            Output(msg, MessageColors.Error);
        }

        public void Success(string msg)
        {
            Output(msg, MessageColors.Success);
        }

        public void Warning(string msg)
        {
            Output(msg, MessageColors.Warning);
        }

        public void Infomation(string msg)
        {
            Output(msg, MessageColors.Info);
        }

        private void Output(string msg, string color)
        {
            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                messages.Add(new Message(msg, color));                
            }));
        }
    }
}