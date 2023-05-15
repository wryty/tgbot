using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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

            string token = "TOOOOKEN";
            bot = new TelegramBotClient(token);

            Users.Clear();
            bot.StartReceiving(
            async (bot, update, cts) =>
            {
                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
                {
                    await HandCallBackQuerry(bot, update.CallbackQuery);
                }
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
                    var button = new InlineKeyboardMarkup(new[]
                    {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Обучение 📄", @"/learn")
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Знакомство 💼", @"/meeting"),
                                InlineKeyboardButton.WithCallbackData("Информация о компании 💾", @"/info")
                            }
                    });


                    if (Users[Users.IndexOf(new TGUser(update.Message.Chat.FirstName, update.Message.Chat.Id))].isHelping == true)
                    {
                        if (update.Message.Text == "Support.End")
                        {
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "❗️Здравствуйте 👋, уважаемый пользователь. ❗️\r\n✨Меня зовут Куль, я универсальный бот компании Leopold, созданный группой программистов Brilliance.✨\r\n📑Моя задача - помогать вам во всех задачах компании и направлять вас в нужное русло.📑", replyMarkup: button);
                            Users[Users.IndexOf(new TGUser(update.Message.Chat.FirstName, update.Message.Chat.Id))].isHelping = false;
                        }
                    }
                    else
                        switch (update.Message.Text)
                        {
                            case @"/start":
                                await bot.SendTextMessageAsync(update.Message.Chat.Id, "❗️Здравствуйте 👋, уважаемый пользователь. ❗️\r\n✨Меня зовут Куль, я универсальный бот компании Leopold, созданный группой программистов Brilliance.✨\r\n📑Моя задача - помогать вам во всех задачах компании и направлять вас в нужное русло.📑", replyMarkup: button);
                                break;
                            default:
                                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Неправильно!");
                                break;
                        }
                }
            }, Error);
            btnSendMsg.Click += async delegate { await SendMsg(); };

        }


        private static async Task HandCallBackQuerry(ITelegramBotClient bot, CallbackQuery callback)
        {
            if (callback.Message == null || Users[Users.IndexOf(new TGUser(callback.Message.Chat.FirstName, callback.Message.Chat.Id))].isHelping == true) return;
            string backToMenu = "Назад в меню ⬅️";
            string back = "Назад ⬅️";
            string profFirstName = "Маркетолог 📊";
            string profSecondName = "Доставщик 🚘";
            string profThirdName = "Кладовщик 📦";
            string education = "Обучение 📄";
            string acquaintance = "Знакомство 💼";
            string informationAboutTheCompany = "Информация о компании 💾";
            string workflowBasics = "Основы рабочего процесса 🧑‍🎓";
            string plan = "План 📅";
            string schedule = "График работы 📈";
            string profFourthName = "Начальник отдела 🔎";
            string profFifthName = "Менеджер 🧑‍💻";
            string profSixthName = "Копирайтер ✉️";
            string profSeventhName = "Координатор международной логистики 🧠";
            string contacts = "Контакты 📱";
            string office = "Офис 🏢";
            string production = "Продукция ⌨️";
            const string literalBackToMain = @"/back.tomain";
            const string literalBackToLearn = @"/back.tolearn";
            const string literalBackToInfo = @"/back.toinfo";
            const string literalBackToMeeting = @"/back.tomeeting";
            const string literalBackToMeetingProfs = @"/back.tomeeting.profs";
            const string literalBackToProfFirstName = @"/back.tomarket";
            const string literalBackToProfSecondName = @"/back.todelivery";
            const string literalBackToProfThirdName = @"/back.tostorekeeper";
            const string literalBackToProduct = @"/back.toproduct";
            const string learn = @"/learn";
            const string meeting = @"/meeting";
            const string meetingProfs = @"/meeting.profs";
            const string meetingProfsFirst = @"/meeting.profs.first";
            const string meetingProfsSecond = @"/meeting.profs.second";
            const string meetingProfsThird = @"/meeting.profs.third";
            const string meetingProfsFourth = @"/meeting.profs.fourth";

            const string meetingChat = @"/meeting.chat"; // !!!
            const string meetingSupport = @"/meeting.support";
            const string supportActivate = @"/support.active";

            const string info = @"/info";
            const string learnProfFirstName = @"/learn.market";
            const string learnProfSecondName = @"/learn.delivery";
            const string learnProfThirdName = @"/learn.storekeeper";
            const string learnWorkprocessProfFirstName = @"/learn.workprocess.market";
            const string learnTodayProfFirstName = @"/learn.today.market";
            const string learnWorktimeProfFirstName = @"/learn.worktime.market";
            const string learnWorkprocessProfSecondName = @"/learn.workprocess.delivery";
            const string learnTodayProfSecondName = @"/learn.today.delivery";
            const string learnWorktimeProfSecondName = @"/learn.worktime.delivery";
            const string learnWorkprocessProfThirdName = @"/learn.workprocess.storekeeper";
            const string learnTodayProfThirdName = @"/learn.today.storekeeper";
            const string learnWorktimeProfThirdName = @"/learn.worktime.storekeeper";

            string contactProfFourthName = @"https://t.me/wryty";
            string contactProfFifthName = @"https://t.me/bydrynok";
            string contactProfSixthName = @"https://t.me/lastdaybeforesuicide";
            string contactProfSeventhName = @"https://t.me/suicidesONMYMIND";

            const string infoContact = @"/info.contact";
            const string infoLocation = @"/info.location";
            const string infoProduct = @"/info.product";
            const string firstProduct = @"/info.product.first";
            const string secondProduct = @"/info.product.second";

            string panelLearn = "🔎Выберите свою профессию, чтобы посмотреть график работы, ближайшие планы, основные требования от вас.🔍";
            string panelMeetingMenu = "👤В вашей группе есть 4 человека(не считая вас)👤 \r\n(В рамках вопроса, касающегося рабочего плана обращайтесь к менеджеру); \r\n ⁉️Если у Вас возникли вопросы на которые вы не нашли ответ, вызовите Помощника;";
            string panelMeetingProfs = "⚡️Ссылки и краткая информация о коллегах⚡️";
            string panelMeetingProfsText = "👨‍⚖Сабиров А.Р. - начальник отдела, который организует табельный учет, составляет графики отпусков, следит за состоянием трудовой дисциплины в подразделениях предприятия и соблюдением работниками правил внутреннего трудового распорядка\r\n📊Мичуров К.Ф. - координатор по международной логистике, получает и следит за графиком доставок и отгрузок.\r\n📑Ураков В.А. - копирайтер, который пишет тексты, статьи и посты для публикации в соцсетях. Это могут быть рекламные тексты товаров или услуг или просто обзорные статьи на любую тему.\r\n📌Бушуев Е.А. - менеджер, который занят управлением процессами и персоналом (подчинёнными) на определённом участке коммерческой организации.";
            string panelMeetingPersonaly = "📎Пообщаться лично📎";
            string panelMeetingChat = "✅С чего начать общение с коллегой:✅\r\n1⃣Представление себя должно основываться на окружающей среде\r\n2⃣Задавая вопросы коллегам после того, как представитесь, вы можете создать двустороннюю беседу и установить с ними связь, что может привести к позитивным отношениям в будущем.\r\n3⃣Отправка электронных писем коллегам, которых вы встретили – отличный способ укрепить ваши отношения с ними. Особенно важно отправлять эти письма людям, с которыми вы будете тесно сотрудничать.";
            string panelMeetingChatName = "🆘С чего начать общение?🆘";
            string panelMeetingSupport = "✨Помощник✨";
            string panelSupportActivate = "❇️Активировать помощника и ожидать оператора❇️";
            string panelSupportActivateName = "Наши операторы обязательно ответят вам и помогут с вашей проблемой, для этого вызовите менеджера";
            string panelSupportDeactivate = "✴️Чтобы отключить помощника, напишите Support.End✴️";

            string meetingProfsFirstName = "👨‍⚖Сабиров А.Р - наш начальник отдела, ответит вам в течение 20 минут.";
            string meetingProfsSecondName = "🧠Мичуров К.Ф - наш координатор по международной логистике - ответит вам в течение 20 минут.";
            string meetingProfsThirdName = "📑Ураков В.А- наш копирайтер - ответит вам в течение 20 минут.";
            string meetingProfsFourthName = "📌Бушуев Е.А- наш менеджер - ответит вам в течение 20 минут.";
            string panelLearnProfSecondName = "🚲Каждый день доставщики - наши специалисты, которые не доставляют проблем покупателю.🚲";
            string panelLiteralBackToProfSecondName = "🚲Каждый день доставщики - наши специалисты, которые не доставляют проблем покупателю.🚲";
            string panelLearnWorkprocessProfSecondName = "🚘Доставщик доставляет товар по месту, указанному в чеке заказа какого-то продукта🚘";
            string panelLearnWorktimeProfSecondName = "📶График работы:\r\n1 смена: С 8:00 🕗  до 16:00 🕓; \r\n2 смена: С 16:00 🕓  до 00:00 🕛;\r\n3 смена: С 00:00 🕛 до 8:00 🕗;";
            string panelLearnTodayProfSecondName = "💼Собеседование с менеджером насчет прогулов.(17 апреля 13:00)💼";

            string panelLearnProfFirstName = "📈Каждый день маркетологи - наши аналики и специалисты мониторят рынок📈";
            string panelLiteralBackToProfFirstName = "📈Каждый день марктелоги - наши аналики и специалисты мониторят рынок📈";
            string panelLearnWorkprocessProfFirstName = "📝Изучение рынка и рыночных тенденций(Анализ рынка – это процесс оценки, моделирования и выявления типичных особенностей рыночных тенденций, где выводы влияют на развитие бизнеса.)📝";
            string panelLearnWorktimeProfFirstName = "📶График работы:\r\n1 смена: С 8:00 🕗  до 16:00 🕓; \r\n2 смена: С 16:00 🕓  до 00:00 🕛;\r\n3 смена: С 00:00 🕛 до 8:00 🕗;";
            string panelLearnTodayProfFirstName = "📢Брифинг-короткая пресс-конференция информативного характера.(17 апреля 2023 года в главном офисе, о подробностях обращайтесь к менеджеру)📢";

            string panellearnProfThirdName = "🗃️Каждый день кладовщики - наши специалисты, которые помогают компании сортировкой товаров на складе.🗃️";
            string panelLiteralBackToProfThirdName = "🗃️Каждый день кладовщики - наши специалисты, которые помогают компании сортировкой товаров на складе.🗃️";
            string panelLearnWorkprocessProfThirdName = "📦 Кладовщик сортирует товар на складе, чтобы его было удобнее выгружать на витрине магазина, а также выкладывать на официальный сайт производителя 📦";
            string panelLearnWorktimeProfThirdName = "📶График работы:\r\n1 смена: С 8:00 🕗  до 16:00 🕓; \r\n2 смена: С 16:00 🕓  до 00:00 🕛;\r\n3 смена: С 00:00 🕛 до 8:00 🕗;";
            string panelLearnTodayProfThirdName = "🔎Проверка складов начальником(16 апреля 15:00)🔍";

            string panelInfoContact = "👤CONTACTS 👤\r\nLeopold.co.,Ltd. \r\n📱Tel: 82-31-926-7701📱\r\n🖨️Fax: 82-31-926-7704🖨️";
            string panelInfoLocation = "ℹ️Leopold.co.,Ltd. ℹ️\r\n🌐Office: # B-dong306,Daebang triplaon,1682, Jungsan-Dong,Ilsan-Donggu,Goyang-Si,Gyeonggi-Do,Korea 10355🌐";
            string panelInfoProduct = "📎Leopold— это магазин механических клавиатур для профессионалов. Мы продаём только то, что нравится нам самим.📎\r\n🔅Каждую представленную на сайте клавиатуру мы рекомендуем к покупке.🔅\r\n⚡️В данном боте находятся наши самые популярные клавиатуры, более конкретная информация находится на официальном сайте Leopold.⚡️";
            string panelFirstProduct = "Leopold FC750R BT White/Mint";
            string panelSecondProduct = "Leopold FC660M PD V2.0";
            string panelInfoFirstProduct = "⚡️Leopold FC750R BT White/Gray⚡️\r\n👉Беспроводная клавиатура 80% формата с низкопрофильными double-shot PBT кейкапами и заводской шумоизоляцией.👈\r\n\r\n▶️USB Type-C порт спрятан в центре под корпусом, а на самой клавиатуре имеются центральная и две боковые канавки под кабель.◀️\r\n\r\n⭐️Низкопрофильные клавиши Step-Sculpture2.⭐️\r\n💎Заметно ниже Cherry профиля и имеют оптимальный угол наклона для комфортной печати.💎\r\n\r\n💥Комплектация💥:\r\n· Клавиатура;\r\n· Коробка;\r\n· Защита от пыли;\r\n· Кейкап-пуллер;\r\n· Дополнительные клавиши модификаторы (Ctrl, Caps Lock);\r\n· Батарейки типа ААА - 2шт;";
            string panelInfoSecondProduct = "⚡️Легендарные Leopold FC660M PD V2.0 теперь с русскими doubleshot кейкапами и улучшенными стабилизаторами.⚡️\r\n\r\n👉Стабилизаторы с ANTI-RATTLING (Дребезг и шум уже в прошлом)\r\nболее гладкие, тихие и почти не люфтят благодаря апгрейду, который мы называем anti-rattling мод.👈\r\n\r\n⭐️Звукопоглощающая подложка⭐️\r\n▶️Войлочная подложка внутри корпуса поглощает звуки при нажатии и делает процесс печати тише.◀️\r\nЕсли тишина имеет большое значение, рекомендуем заменить заводскую шумоизоляцию на нашу из бипласта.\r\n\r\n💣Превосходное качество сборки💣\r\n💥Leopold славится качественными материалами и сборкой.\r\nПрочный пластиковый корпус, монолитная конструкция, PBT кейкапы, шумоизоляция — все это делает клавиатуру надежным товарищем на многие годы.💥";
            //string panelThirdProduct = "Leopold FC750R BT White/Gray";

            string panelLiteralBackToMain = "❗️Здравствуйте 👋, уважаемый пользователь. ❗️\r\n✨Меня зовут Куль, я универсальный бот компании Leopold, созданный группой программистов Brilliance.✨\r\n📑Моя задача - помогать вам во всех задачах компании и направлять вас в нужное русло.📑";
            string panelLiteralBackToLearn = "👤Наша команда состоит из талантливых и опытных профессионалов, которые готовы поделиться своими знаниями и помочь вам в адаптации.\r\n🎉Добро пожаловать в нашу IT компанию - Leopold!🎉\r\n⚡️Мы рады, что вы присоединились к нашей команде и желаем вам успехов в работе с нами.⚡️";
            string panelLiteralBackToMeeting = "👤В вашей группе  есть 4 человека👤\r\n(В рамках вопроса, касающегося рабочего плана обращайтесь к менеджеру);";
            string panelLiteralBackToInfo = "🏢О компании Leopold и первых ее продуктах мир узнал в 2006 году – именно тогда этот бренд был основан.🏢\r\n🇰🇷Официальное представительство данной организации находится в Кояне (Южная Корея). 🇰🇷\r\n\r\n⌨️Бренд Leopold – торговая марка, специалисты которой создают компьютерные периферийные устройства, в том числе игровые клавиатуры. ⌨️\r\n📞Специалисты этой южнокорейской компании разрабатывают и сами производят девайсы и сопутствующие товары, уделяя максимум внимания их качеству, технологичности и добротности.📞\r\n💵Приобрести продукцию от Leopold, отличающуюся стильным дизайном, отличными рабочими характеристиками и умеренной ценой, вы сможете в нашем магазине.💵\r\n\r\n📌Если вас интересует вопрос, где заказать Leopold клавиатуры, качественные и стильные, ответ на него прост – вы их сможете приобрести в нашем интернет-магазине.📌\r\n✏️Мы рады предложить вам широкий ассортимент новых брендовых моделей по привлекательной цене – выбирайте и покупайте!✏️";
            string panelLiteralBackToProduct = "📎Leopold— это магазин механических клавиатур для профессионалов. Мы продаём только то, что нравится нам самим.📎\r\n🔅Каждую представленную на сайте клавиатуру мы рекомендуем к покупке.🔅\r\n⚡️В данном боте находятся наши самые популярные клавиатуры, более конкретная информация находится на официальном сайте Leopold.⚡️";
            string panelIDK = "Я не знаю что вы выбрали";


            var button = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(education, learn)
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData(acquaintance, meeting),
                    InlineKeyboardButton.WithCallbackData(informationAboutTheCompany, info)
                }
            });

            var buttonLearnProffesions = (new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(profFirstName, learnProfFirstName),
                    InlineKeyboardButton.WithCallbackData(profSecondName, learnProfSecondName)
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData(profThirdName, learnProfThirdName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(backToMenu, literalBackToMain)
                }
            });


            var buttonProducts = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(panelFirstProduct, firstProduct),
                    InlineKeyboardButton.WithCallbackData(panelSecondProduct, secondProduct)
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData(back, literalBackToInfo)
                },
            });


            var buttonLearnMarket = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(workflowBasics, learnWorkprocessProfFirstName)
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData(plan, learnTodayProfFirstName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(schedule, learnWorktimeProfFirstName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToLearn)
                }
            });
            var buttonLearnDelivery = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(workflowBasics, learnWorkprocessProfSecondName)
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData(plan, learnTodayProfSecondName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(schedule, learnWorktimeProfSecondName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToLearn)
                }
            });
            var buttonLearnStoreKeeper = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(workflowBasics, learnWorkprocessProfThirdName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(plan, learnTodayProfThirdName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(schedule, learnWorktimeProfThirdName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToLearn)
                }
            });

            var buttonMeetingLinksFirst = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl(panelMeetingPersonaly, contactProfFourthName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToMeetingProfs)
                }
            });

            var buttonMeetingLinksSecond = new InlineKeyboardMarkup(new[]
{
                new[]
                {
                    InlineKeyboardButton.WithUrl(panelMeetingPersonaly, contactProfFifthName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToMeetingProfs)
                }
            });

            var buttonMeetingLinksThird = new InlineKeyboardMarkup(new[]
{
                new[]
                {
                    InlineKeyboardButton.WithUrl(panelMeetingPersonaly, contactProfSixthName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToMeetingProfs)
                }
            });

            var buttonMeetingLinksFourth = new InlineKeyboardMarkup(new[]
{
                new[]
                {
                    InlineKeyboardButton.WithUrl(panelMeetingPersonaly, contactProfSeventhName)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToMeetingProfs)
                }
            });

            var buttonMeeting = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(panelMeetingProfs, meetingProfs)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(panelMeetingChatName, meetingChat)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(panelMeetingSupport, meetingSupport)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(backToMenu, literalBackToMain)
                }
            });

            var buttonMeetingProfs = new InlineKeyboardMarkup(new[]
{
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(profFourthName, meetingProfsFirst)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(profFifthName, meetingProfsSecond)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(profSixthName, meetingProfsThird)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(profSeventhName, meetingProfsFourth)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToMeeting)
                }
            });
            var buttonInfo = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(contacts, infoContact)
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData(office, infoLocation)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(production,  infoProduct)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(backToMenu, literalBackToMain)
                }
            });

            var buttonBackToMeeting = new InlineKeyboardMarkup(new[]
            {
                new[] {
                InlineKeyboardButton.WithCallbackData(back, literalBackToMeeting)
                }
            });

            var buttonBackToMeetingProfs = new InlineKeyboardMarkup(new[]
            {
                new[] {
                InlineKeyboardButton.WithCallbackData(back, literalBackToMeetingProfs)
                }
            });

            var buttonSupportActivate = new InlineKeyboardMarkup(new[]
            {
                new[]
                {

                    InlineKeyboardButton.WithCallbackData(panelSupportActivate, supportActivate)
                },
                new[] {
                    InlineKeyboardButton.WithCallbackData(back, literalBackToMeeting)
                }
            });



            /*var buttonBackToLearn = new InlineKeyboardMarkup(new[]
            //{
            //    new[]
            //    {
            //    InlineKeyboardButton.WithCallbackData(back, literalBackToLearn)
            //    }
            //});*/
            var buttonBackToInfo = new InlineKeyboardMarkup(new[]
            {
                new[] {
                InlineKeyboardButton.WithCallbackData(back, literalBackToInfo)
                }
            });
            var buttonBackToMarket = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                InlineKeyboardButton.WithCallbackData(back, literalBackToProfFirstName)
                }
            });
            var buttonBackToDelivery = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                InlineKeyboardButton.WithCallbackData(back, literalBackToProfSecondName)
                }
            });
            var buttonBackToStoreKeeper = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                InlineKeyboardButton.WithCallbackData(back, literalBackToProfThirdName)
                }
            });
            var buttonBackToProducts = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                InlineKeyboardButton.WithCallbackData(back, literalBackToProduct)
                }
            });
            //await bot.SendTextMessageAsync(callback.Message.Chat.Id, $"Вы выбрали {callback.Data}");
            switch (callback.Data)
            {
                case learn:
                    //await bot.SendTextMessageAsync(callback.Message.Chat.Id, "Добро пожаловать в нашу IT компанию - __Альцеймер__! Мы рады, что вы присоединились к нашей команде и желаем вам успехов в работе с нами.Наша команда состоит из талантливых и опытных профессионалов, которые готовы поделиться своими знаниями и помочь вам в адаптации. Мы уверены, что ваше присутствие в нашей компании будет приятным и продуктивным опытом. Добро пожаловать в нашу команду!)", replyMarkup: buttonLearn);
                    //await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, "Наша команда состоит из талантливых и опытных профессионалов, которые готовы поделиться своими знаниями и помочь вам в адаптации. Добро пожаловать в нашу IT компанию - Leopold!\r\nМы рады, что вы присоединились к нашей команде и желаем вам успехов в работе с нами.", replyMarkup: buttonLearn);
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearn, replyMarkup: buttonLearnProffesions); // ! +
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case meeting:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelMeetingMenu, replyMarkup: buttonMeeting); // ! +
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case info:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToInfo, replyMarkup: buttonInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;


                case learnProfSecondName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnProfSecondName, replyMarkup: buttonLearnDelivery);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case literalBackToProfSecondName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToProfSecondName, replyMarkup: buttonLearnDelivery);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnWorkprocessProfSecondName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnWorkprocessProfSecondName, replyMarkup: buttonBackToDelivery);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnWorktimeProfSecondName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnWorktimeProfSecondName, replyMarkup: buttonBackToDelivery);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnTodayProfSecondName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnTodayProfSecondName, replyMarkup: buttonBackToDelivery);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;







                case learnProfFirstName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnProfFirstName, replyMarkup: buttonLearnMarket);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case literalBackToProfFirstName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToProfFirstName, replyMarkup: buttonLearnMarket);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnWorkprocessProfFirstName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnWorkprocessProfFirstName, replyMarkup: buttonBackToMarket);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnWorktimeProfFirstName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnWorktimeProfFirstName, replyMarkup: buttonBackToMarket);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnTodayProfFirstName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnTodayProfFirstName, replyMarkup: buttonBackToMarket);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;







                case learnProfThirdName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panellearnProfThirdName, replyMarkup: buttonLearnStoreKeeper);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case literalBackToProfThirdName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToProfThirdName, replyMarkup: buttonLearnStoreKeeper);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnWorkprocessProfThirdName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnWorkprocessProfThirdName, replyMarkup: buttonBackToStoreKeeper);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnWorktimeProfThirdName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnWorktimeProfThirdName, replyMarkup: buttonBackToStoreKeeper);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case learnTodayProfThirdName:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLearnTodayProfThirdName, replyMarkup: buttonBackToStoreKeeper);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case meetingProfs:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelMeetingProfsText, replyMarkup: buttonMeetingProfs);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case meetingProfsFirst:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, meetingProfsFirstName, replyMarkup: buttonMeetingLinksFirst);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case meetingProfsSecond:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, meetingProfsFourthName, replyMarkup: buttonMeetingLinksSecond);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case meetingProfsThird:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, meetingProfsThirdName, replyMarkup: buttonMeetingLinksThird);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case meetingProfsFourth:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, meetingProfsSecondName, replyMarkup: buttonMeetingLinksFourth);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case literalBackToMeetingProfs:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelMeetingProfsText, replyMarkup: buttonMeetingProfs);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;


                case meetingChat:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelMeetingChat, replyMarkup: buttonBackToMeeting);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case meetingSupport:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelSupportActivateName, replyMarkup: buttonSupportActivate);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case supportActivate:
                    var person = new TGUser(callback.Message.Chat.FirstName, callback.Message.Chat.Id);

                    Users[Users.IndexOf(person)].AddMessage($"SUPPORT ACTIVE");
                    Users[Users.IndexOf(person)].isHelping = true;
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelSupportDeactivate);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;



                case infoContact:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelInfoContact, replyMarkup: buttonBackToInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case infoLocation:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelInfoLocation, replyMarkup: buttonBackToInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case infoProduct:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelInfoProduct, replyMarkup: buttonProducts);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case firstProduct:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelInfoFirstProduct, replyMarkup: buttonBackToProducts);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case secondProduct:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelInfoSecondProduct, replyMarkup: buttonBackToProducts);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;















                case literalBackToMain:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToMain, replyMarkup: button);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case literalBackToLearn:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToLearn, replyMarkup: buttonLearnProffesions);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case literalBackToMeeting:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToMeeting, replyMarkup: buttonMeeting);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case literalBackToInfo:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToInfo, replyMarkup: buttonInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case literalBackToProduct:
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, panelLiteralBackToProduct, replyMarkup: buttonProducts);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                default:
                    await bot.SendTextMessageAsync(callback.Message.Chat.Id, panelIDK);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;


            }
            return;
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
