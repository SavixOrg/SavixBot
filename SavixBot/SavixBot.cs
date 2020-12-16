using Argos.Base.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;

namespace SavixBot
{
    public static class SavixBot
    {
        static TelegramBotClient bot;

        static public bool Paused = false;

        static Char lf = (char)10;

        private static bool IsMember(int userId, long chatId)
        {
            var t = bot.GetChatMemberAsync((Telegram.Bot.Types.ChatId)chatId, userId);
            if (t.Result.Status.ToString() == "Left")
                return false;
            return true;
        }

        public static async Task StartBot()
        {
            string key = File.ReadAllText("key.txt").Trim();
            bot = new TelegramBotClient(key);
            Workflow.SetBot(bot);
            bot.OnMessage += BotClient_OnMessage;
            bot.OnUpdate += Bot_OnUpdate;
            var me = await bot.GetMeAsync();
            bot.StartReceiving();
        }

        static DateTime lastWhitelistMsgDate = DateTime.MinValue;

        private static void Bot_OnUpdate(object sender, Telegram.Bot.Args.UpdateEventArgs e)
        {
            if (e.Update.ChannelPost == null)
                return;

            if (e.Update.ChannelPost.Text == null)
                return;

            string channelMsg = e.Update.ChannelPost.Text.ToLower().Trim();

            if (channelMsg.Contains("presale") || channelMsg.Contains("whitelist"))
            {
                if (DateTime.Now > lastWhitelistMsgDate.AddMinutes(3))
                {
                    //bot.SendTextMessageAsync(e.Update.ChannelPost.Chat, "in order to join our presale you have to chat with @SavixRobot and type /start to begin whitelist procedure", Telegram.Bot.Types.Enums.ParseMode.Html);
                    bot.SendTextMessageAsync(e.Update.ChannelPost.Chat, "our whitelist procedure for the presale will start soon, please watch for the relevant announcement", Telegram.Bot.Types.Enums.ParseMode.Html);
                    lastWhitelistMsgDate = DateTime.Now;
                }
            }
        }


        static void ParseCommand(User user, Telegram.Bot.Args.MessageEventArgs e)
        {
            switch (e.Message.Text.ToLower())
            {
                case "/ann":
                    if (e.Message.From.Username == "mcopper" || e.Message.From.Username == "Anatol93" || e.Message.From.Username == "novaoffice")
                        SendANN();
                    break;
                case "/start":
                    WFWhitelist.DoWhitelist(user, e);
                    break;
                case "/giveaway":
                    WFGiveaway.DoGiveaway(user, e);
                    break;
                default:
                    // print commands
                    break;
            }
        }

        public static void DoChannelMessage(Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message == null || e.Message.Text == null)
                return;

            if (e.Message.Text.Contains("presale") || e.Message.Text.Contains("whitelist"))
            {
                if (DateTime.Now > lastWhitelistMsgDate.AddMinutes(3))
                {
                    //bot.SendTextMessageAsync(e.Update.ChannelPost.Chat, "in order to join our presale you have to chat with @SavixRobot and type /start to begin whitelist procedure", Telegram.Bot.Types.Enums.ParseMode.Html);
                    Workflow.SendMessage(e.Message, null, "our whitelist procedure for the presale will start soon, please watch for the relevant announcement");
                    lastWhitelistMsgDate = DateTime.Now;
                }
            }
            // message from group
            return;
        }

        public static bool CheckMembership(long groupId, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                if (!IsMember(e.Message.From.Id, -1001354512306))
                {
                    Workflow.SendMessage(e.Message, null, "You must be a member of the Savix group (@savix_org) to continue" + lf + "Please join....");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Workflow.SendMessage(e.Message, null, "Error: " + ex.Message + lf + "Please try again...");
                return false;
            }
            return true;
        }

        public static void SendANN()
        {
            string cap = "‼️ Our first **GIVEAWAY** just started ‼️" + lf + lf +
                         "⚡️ We airdrop **250 SVX** (**7500$** estimated value)" + lf +
                         "⚡️ **0.5 SVX (15$)** for each participant." + lf + lf +
                         "⚡️ Only **" + (WFGiveaway.MaxCount-WFGiveaway.Count).ToString() + "/" + WFGiveaway.MaxCount + "** spots left !" + lf + lf +
                        "🔹 **You need a TWITTER and DISCORD account**" + lf +
                        "🔹 **And complete 3 simple tasks to participate.**" + lf + lf +
                        "👉 To Begin: Start a conversation with @SavixRobot" + lf+
                        "👉 then type /giveaway" + lf;

            //Workflow.SendImage(-482596285, "https://savix.org/wp-content/uploads/2020/11/savix_giveaway2.jpg", cap);
            Workflow.SendImage(-1001354512306, "https://savix.org/wp-content/uploads/2020/11/savix_giveaway2.jpg", cap);
        }

        static void ProcessGroupMessage(Telegram.Bot.Args.MessageEventArgs e)
        {   // test group -482596285
            if (e.Message.Text.ToLower().StartsWith("/ann"))
            {
                if (e.Message.From.Username == "mcopper" || e.Message.From.Username == "Anatol93" || e.Message.From.Username == "novaoffice")
                {
                    SendANN();
                }
            }
            switch (e.Message.Text)
            {
                case "/giveaway@SavixRobot":
                    if (String.IsNullOrEmpty(e.Message.From.Username))
                    {
                        Workflow.SendMessage(e.Message, null, "Please set a username to use this bot!" + lf + "Go to Settings > Edit Profile > Set Username");
                        return;
                    }
                    WFGiveaway.DoGiveaway(UserController.GetOrAddItem(e.Message.From.Id, e.Message.From.FirstName, e.Message.From.LastName,
                    e.Message.Date, e.Message.From.Username, e.Message.From.LanguageCode), e);
                    break;
                case "/start@SavixRobot":
                    if (String.IsNullOrEmpty(e.Message.From.Username))
                    {
                        Workflow.SendMessage(e.Message, null, "Please set a username to use this bot!" + lf + "Go to Settings > Edit Profile > Set Username");
                        return;
                    }
                    WFWhitelist.DoWhitelist(UserController.GetOrAddItem(e.Message.From.Id, e.Message.From.FirstName, e.Message.From.LastName,
                    e.Message.Date, e.Message.From.Username, e.Message.From.LanguageCode), e);
                    break;
            }
        }

        static void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            // ignore other bots
            if (e.Message.From.IsBot)
                return;

            if (Paused)
            {
                Workflow.SendMessage(e.Message, null, "Bot is in maintenance mode, please try again later...");
                return;
            }

            if (e.Message.Text == null)
                return;

            try
            {
                if (!IsMember(e.Message.From.Id, -1001354512306))
                {
                    Workflow.SendMessage(e.Message, null, "You must be a member of the Savix group (@savix_org) to continue" + lf + "Please join....");
                    return;
                }
            }
            catch (Exception ex)
            {
                Workflow.SendMessage(e.Message, null, "Error: " + ex.Message + lf + "Please try again...");
                return;
            }
            
            if (e.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Group || e.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Supergroup)
            {
                ProcessGroupMessage(e);
                return;
            }

            if (String.IsNullOrEmpty(e.Message.From.Username))
            {
                Workflow.SendMessage(e.Message, null, "Please set a username to use this bot!" + lf + "Go to Settings > Edit Profile > Set Username");
                return;
            }

            User user = UserController.GetOrAddItem(e.Message.Chat.Id, e.Message.Chat.FirstName, e.Message.Chat.LastName,
                    e.Message.Date, e.Message.Chat.Username, e.Message.From.LanguageCode);

            user.ChatProtocol.Add(e.Message.Text);

                       
            if (e.Message.Text.StartsWith("/"))
            {
                ParseCommand(user, e);
            }
            else
            {
                switch (user.Workflow)
                {
                    case "whitelist":
                        WFWhitelist.DoWhitelist(user, e);
                        break;
                    case "giveaway":
                        WFGiveaway.DoGiveaway(user, e);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}