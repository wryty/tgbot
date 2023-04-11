using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace myproj
{
    class Program
    {
        static Message menu;
        static void Main()
        {
            var bot = new TelegramBotClient("6286955732:AAEUOG2_l5LLqui1WyQSzxIdJte1kcjYnKk");
            bot.StartReceiving(MessageUpdate, Error);
            Console.ReadLine();
        }

        private static async Task MessageUpdate(ITelegramBotClient bot, Update update, CancellationToken cts)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery && update.CallbackQuery != null)
            {
                await HandCallBackQuerry(bot, update.CallbackQuery);
            }

            if (update.Message == null) { return; };
            var id = update.Message.Chat.Id;
            var message = update.Message.Text;

            var button = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Обучение", @"/learn")
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData("Знакомство", @"/meeting"),
                    InlineKeyboardButton.WithCallbackData("Информация о компании", @"/info")
                }
            });
            switch (message)
            {
                case @"/start":
                    menu = await bot.SendTextMessageAsync(id, "Здравствуйте, уважаемый пользователь. Меня зовут Куль, я универсальный бот компании Leopold, созданный группой программистов Brilliance. Моя задача - помогать вам во всех задачах компании и направлять вас в нужное русло.", replyMarkup: button);
                    break;
                default:
                    await bot.SendTextMessageAsync(id, "Не правильно!");
                    break;
            }
        }



        private static async Task HandCallBackQuerry(ITelegramBotClient bot, CallbackQuery callback)
        {
            if (callback.Message == null) return;
            var button = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Обучение", @"/learn")
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData("Знакомство", @"/meeting"),
                    InlineKeyboardButton.WithCallbackData("Информация о компании", @"/info")
                }
            });
            var buttonLearn = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Основы рабочего процесса", @"/learn.workprocess")
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData("План на сегодня", @"/learn.today")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("🕰 Графики работы", @"/learn.worktime")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад в меню", @"/back.tomain")
                }
            });

            var buttonMeeting = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Начальник отдела", @"/meeting.teamlead")
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData("Менеджер", @"/meeting.manager")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Помощник", @"/meeting.helper")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад в меню", @"/back.tomain")
                }
            });


            var buttonInfo = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Контакты", @"/info.contact")
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData("Расположение", @"/info.location")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Продукция", @"/info.product")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад в меню", @"/back.tomain")
                }
            });

            var buttonBackToLearn = new InlineKeyboardMarkup(new[]
            {
                new[] {
                InlineKeyboardButton.WithCallbackData("Назад", @"/back.tolearn")
                }
            });
            var buttonBackToMeeting = new InlineKeyboardMarkup(new[]
            {
                new[] {
                InlineKeyboardButton.WithCallbackData("Назад", @"/back.tomeeting")
                }
            });
            var buttonBackToInfo = new InlineKeyboardMarkup(new[]
            {
                new[] {
                InlineKeyboardButton.WithCallbackData("Назад", @"/back.toinfo")
                }
            });
            //await bot.SendTextMessageAsync(callback.Message.Chat.Id, $"Вы выбрали {callback.Data}");
            switch (callback.Data)
            {
                case @"/learn":
                    //await bot.SendTextMessageAsync(callback.Message.Chat.Id, "Добро пожаловать в нашу IT компанию - __Альцеймер__! Мы рады, что вы присоединились к нашей команде и желаем вам успехов в работе с нами.Наша команда состоит из талантливых и опытных профессионалов, которые готовы поделиться своими знаниями и помочь вам в адаптации. Мы уверены, что ваше присутствие в нашей компании будет приятным и продуктивным опытом. Добро пожаловать в нашу команду!)", replyMarkup: buttonLearn);
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Наша команда состоит из талантливых и опытных профессионалов, которые готовы поделиться своими знаниями и помочь вам в адаптации. Добро пожаловать в нашу IT компанию - Leopold!\r\nМы рады, что вы присоединились к нашей команде и желаем вам успехов в работе с нами.", replyMarkup: buttonLearn);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/meeting":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Вы ща будете знакомиться с новыми людьми", replyMarkup: buttonMeeting);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/info":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "О компании Leopold и первых ее продуктах мир узнал в 2006 году – именно тогда этот бренд был основан.\r\nОфициальное представительство данной организации находится в Кояне (Южная Корея). \r\n\r\nБренд Leopold – торговая марка, специалисты которой создают компьютерные периферийные устройства, в том числе игровые клавиатуры. \r\nСпециалисты этой южнокорейской компании разрабатывают и сами производят девайсы и сопутствующие товары, уделяя максимум внимания их качеству, технологичности и добротности.\r\nПриобрести продукцию от Leopold, отличающуюся стильным дизайном, отличными рабочими характеристиками и умеренной ценой, вы сможете в нашем магазине.\r\n\r\nЕсли вас интересует вопрос, где заказать Leopold клавиатуры, качественные и стильные, ответ на него прост – вы их сможете приобрести в нашем интернет-магазине. \r\nМы рады предложить вам широкий ассортимент новых брендовых моделей по привлекательной цене – выбирайте и покупайте!", replyMarkup: buttonInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/learn.workprocess":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Каждый день наши аналики и специалисты мониторят рынок и ищут:-Изучение рынка и рыночных тенденций(Анализ рынка – это процесс оценки, моделирования и выявления типичных особенностей рыночных тенденций, где выводы влияют на развитие бизнеса.)\r\n-Изучение поведения потребителей(Поведение потребителей — это междисциплинарная социальная наука, которая сочетает в себе элементы психологии, социологии, социальной антропологии, антропологии, этнографии, маркетинга и экономики (особенно поведенческой).Эта наука исследует, как эмоции, отношения и предпочтения влияют на покупательское поведение.)\r\n-Выбор целевого рынка(Выбор целевых рынков - это процесс оценки привлекательности каждого сегмента рынка и выбор одного или нескольких сегментов для освоения.Прежде чем отобрать целевой рынок, фирма должна оценить его размеры и спрогнозировать потенциал роста, а также возможную прибыль.)\r\n-Разработка конкурентного преимущества(Конкурентное преимущество для бизнеса обеспечивает: перспективы долгосрочного роста; стабильность работы; получение большей норм: прибыли с продажи товаров; создание барьеров для новых игроков при выходе на рынок.)-Утверждение стратегии развития товара(Стратегия развития продукта необходима для защиты предприятия от потери прибыли.)\r\n-Контроль и анализ результатов работ", replyMarkup: buttonBackToLearn);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/learn.worktime":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "График работы:\r\n1 смена: С 8:00 до 16:00;\r\n2 смена: С 16:00 до 00:00;\r\n3 смена: С 00:00 до 8:00;", replyMarkup: buttonBackToLearn);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/learn.today":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Брифинг-короткая пресс-конференция информативного характера.(17 апреля 2023 года в главном офисе, о подробностях обращайтесь к менеджеру)", replyMarkup: buttonBackToLearn);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;

                case @"/meeting.teamlead":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Тимлид", replyMarkup: buttonBackToMeeting);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;

                case @"/meeting.manager":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Менеджер", replyMarkup: buttonBackToMeeting);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/meeting.helper":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Помощнки", replyMarkup: buttonBackToMeeting);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;

                case @"/info.contact":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "CONTACTS \r\nLeopold.co.,Ltd. \r\nTel: 82-31-926-7701Fax: 82-31-926-7704", replyMarkup: buttonBackToInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/info.location":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Leopold.co.,Ltd. \r\nOffice: # B-dong306,Daebang triplaon,1682, Jungsan-Dong,Ilsan-Donggu,Goyang-Si,Gyeonggi-Do,Korea 10355", replyMarkup: buttonBackToInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/info.product":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "ПРОДУКТ", replyMarkup: buttonBackToInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;















                case @"/back.tomain":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Здравствуйте, уважаемый пользователь. Меня зовут Куль, я универсальный бот компании Leopold, созданный группой программистов Brilliance. Моя задача - помогать вам во всех задачах компании и направлять вас в нужное русло.", replyMarkup: button);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/back.tolearn":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Наша команда состоит из талантливых и опытных профессионалов, которые готовы поделиться своими знаниями и помочь вам в адаптации. Добро пожаловать в нашу IT компанию - Leopold!\r\nМы рады, что вы присоединились к нашей команде и желаем вам успехов в работе с нами.", replyMarkup: buttonLearn);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/back.tomeeting":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "Вы ща будете знакомиться с новыми людьми", replyMarkup: buttonMeeting);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                case @"/back.toinfo":
                    await bot.EditMessageTextAsync(callback.Message.Chat.Id, menu.MessageId, "О компании Leopold и первых ее продуктах мир узнал в 2006 году – именно тогда этот бренд был основан.\r\nОфициальное представительство данной организации находится в Кояне (Южная Корея). \r\n\r\nБренд Leopold – торговая марка, специалисты которой создают компьютерные периферийные устройства, в том числе игровые клавиатуры. \r\nСпециалисты этой южнокорейской компании разрабатывают и сами производят девайсы и сопутствующие товары, уделяя максимум внимания их качеству, технологичности и добротности.\r\nПриобрести продукцию от Leopold, отличающуюся стильным дизайном, отличными рабочими характеристиками и умеренной ценой, вы сможете в нашем магазине.\r\n\r\nЕсли вас интересует вопрос, где заказать Leopold клавиатуры, качественные и стильные, ответ на него прост – вы их сможете приобрести в нашем интернет-магазине. \r\nМы рады предложить вам широкий ассортимент новых брендовых моделей по привлекательной цене – выбирайте и покупайте!", replyMarkup: buttonInfo);
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;
                default:
                    await bot.SendTextMessageAsync(callback.Message.Chat.Id, "Я не знаю что вы выбрали");
                    await bot.AnswerCallbackQueryAsync(callback.Id, "");
                    break;

            }
            return;
        }


        private static async Task Error(ITelegramBotClient bot, Exception e, CancellationToken cts)
        {
            Console.Write(e.Message);
            return;
        }
    }
}