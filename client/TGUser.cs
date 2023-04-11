using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace client
{
    internal class TGUser : INotifyPropertyChanged, IEquatable<TGUser>
    {
        public TGUser(string Nickname, long chatId)
        {
            this.nick = Nickname;
            this.id = chatId;
            Messages = new ObservableCollection<string>();
        }

        private string nick;
        public string Nick
        {
            get { return this.nick; }
            set
            {
                this.nick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Nick)));
            }
        }
        private long id;
        public long Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Id)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool Equals(TGUser other) => other.Id == this.id;


        public ObservableCollection<string> Messages { get; set; }
        public async void AddMessage(string text) => Messages.Add(text);
    }
}
