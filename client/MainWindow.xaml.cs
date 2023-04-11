using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TelegramBotClient bot;

        static ObservableCollection<TGUser> Users;

        public MainWindow()
        {
            InitializeComponent();

            Users = new ObservableCollection<TGUser>();

            usersList.ItemsSource = Users;

            string token = "6286955732:AAEUOG2_l5LLqui1WyQSzxIdJte1kcjYnKk";
            bot = new TelegramBotClient(token);


            bot.StartReceiving(
            async (bot, update, cts) =>
            {
                if (update.Message is Message message)
                {
                    string msg = $"{DateTime.Now}: {update.Message.Chat.FirstName} {update.Message.Chat.Id} {update.Message.Text}";


                    //File.AppendAllText("data.log", $"{msg}\n");

                    //Debug.WriteLine(msg);
                    this.Dispatcher.Invoke(() =>
                    {
                        var person = new TGUser(update.Message.Chat.FirstName, update.Message.Chat.Id);

                        if (!Users.Contains(person)) Users.Add(person);

                        Users[Users.IndexOf(person)].AddMessage($"{person.Nick}: {update.Message.Text}");
                    });
                }
            }, Error);
            btnSendMsg.Click += async delegate { await SendMsg(); };

        }




        public async Task SendMsg()
        {
            var concreteUser = Users[Users.IndexOf(usersList.SelectedItem as TGUser)];
            string responseMsg = $"Support: {txtBxSendMsg.Text}";
            concreteUser.Messages.Add(responseMsg);

            await bot.SendTextMessageAsync(concreteUser.Id, txtBxSendMsg.Text);
            //string logText = $"{DateTime.Now}: >> {concreteUser.Id} {concreteUser.Nick} {responseMsg}\n";
            //File.AppendAllText("data.log", logText);

            txtBxSendMsg.Text = String.Empty;
        }
        private static async Task Error(ITelegramBotClient bot, Exception e, CancellationToken cts)
        {

        }
    }
}
