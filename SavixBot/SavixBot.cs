using Argos.Base.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;

namespace SavixBot
{
    public static class SavixBot
    {
        static string key = "*";
        static TelegramBotClient bot = new TelegramBotClient(key);

        const double MIN_CONTRIBUTION = 0.5;
        const double MAX_CONTRIBUTION = 10;

        static public bool Paused = false;

        static Char lf = (char)10;


        private static bool IsMember(int userId, long chatId)
        {
            var t = bot.GetChatMemberAsync((Telegram.Bot.Types.ChatId)chatId, userId);
            if (t.Result.Status.ToString() == "Left")
                return false;
            return true;
        }

        public static string VerifyEthAddress(string addr)
        {
            HttpParameter param = new HttpParameter();
            param.Url = "https://etherscan.io/address/" + addr;
            var result = HttpClient.GetPage(param);

            Regex rx = new Regex(@"\d+(<b>.</b>\d+)?\sEther");

            Regex rxInvalid = new Regex(@"Ethereum\sAccount\s\(Invalid\sAddress\)");
            if (rxInvalid.IsMatch(result.RawData))
                return null;

            if (rx.IsMatch(result.RawData))
                return rx.Match(result.RawData).Value.Replace("<b>", "").Replace("</b>", "").Replace("Ether", "").Trim();

            return null;
        }

        static async public void SendMessage(long chatId, WhitelistItem item, string text)
        {
            if (item != null)
                item.ChatProtocol.Add("bot: " + text);
            await bot.SendTextMessageAsync((Telegram.Bot.Types.ChatId)chatId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
        }

        static async void SendMessage(Telegram.Bot.Types.Message message, WhitelistItem chat, string text)
        {
            if (chat != null)
                chat.ChatProtocol.Add("bot: " + text);
            await bot.SendTextMessageAsync(message.Chat, text, Telegram.Bot.Types.Enums.ParseMode.Html);
        }


        static void ProcessStateZero(WhitelistItem chat, Telegram.Bot.Types.Message message)
        {
            if (message.Text.ToLower() == "/start")
            {
                SendMessage(message, chat, "Please enter your ETH address that you will be sending the funds from (format 0x)");
                chat.State = WhiteListStateEnum.wait_for_address;
            }
            else
            {
                SendMessage(message, chat, "Please enter /start to begin the whitelist procedure");
            }
            return;
        }

        static void ProcessStateWaitForAddress(WhitelistItem chat, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim();
            if (!msg.StartsWith("0x"))
            {
                SendMessage(message, chat, "Your ETH address must start with 0x, please try again...");
                return;
            }
            else if (msg.Length != 42)
            {
                SendMessage(message, chat, "Wrong length of ETH address, please try again...");
                return;
            }

            SendMessage(message, chat, "Verifying your ETH address on the blockchain (can take a few seconds)");
            string value = VerifyEthAddress(msg);
            if (value == null)
            {
                SendMessage(message, chat, "Unknown or Invalid ETH address, please try again...");
            }

            chat.EthAddress = msg;
            chat.EthBalance = value;
            chat.State = WhiteListStateEnum.wait_for_amount;
            SendMessage(message, chat, String.Format("How much ETH do you want to contribute ?"+lf+"(0.5 ETH minimum, 10 ETH maximum)", message.Chat.FirstName));
            return;
        }

        static void ProcessStateWaitForAmount(WhitelistItem chat, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().Replace(".",",");
            double amount = 0;
            try
            {
                amount = Convert.ToDouble(msg);
            }
            catch (Exception e)
            {
                SendMessage(message, chat, "Incorrect number format, please try again...");
                SendMessage(message, chat, String.Format("How much ETH do you want to contribute ?"+lf+"(0.5 ETH minimum, 10 ETH maximum)", message.Chat.FirstName));
                return;
            }
            if (amount < MIN_CONTRIBUTION)
            {
                SendMessage(message, chat, "That's not enough, minimum is 0.5 ETH");
                return;
            }
            else if (amount > MAX_CONTRIBUTION)
            {
                SendMessage(message, chat, "That's too much, maximum is 10 ETH");
                return;
            }

            chat.Contribution = amount;
            chat.State = WhiteListStateEnum.wait_for_terms;
            SendMessage(message, chat, @"Please type <b>yes</b> if you agree to our <a href=""https://savix.org/terms"">Terms and Conditions</a>");
            return;
        }

        static void ProcessStateWaitForTerms(WhitelistItem chat, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().ToLower();
            if (msg == "no")
            {
                chat.State = WhiteListStateEnum.zero;
                chat.EthAddress = null;
                chat.Contribution = 0;
                chat.EthBalance = null;
                SendMessage(message, chat, "Sorry to hear that, if you want, you can restart the whitelist process at any time.");
                return;
            }
            else if (msg != "yes")
            {
                SendMessage(message, chat, @"Please type <b>yes</b> if you agree to our <a href=""https://savix.org/terms"">Terms and Conditions</a>");
                return;
            }

            chat.State = WhiteListStateEnum.wait_for_approval;
            SendMessage(message, chat, String.Format("Thanks {0}, whitelist application successful."+lf+"I will send you a message after your approval", message.Chat.FirstName));
            return;
        }

        static void ProcessStateWaitForApproval(WhitelistItem chat, Telegram.Bot.Types.Message message)
        {
            SendMessage(message, chat, "Your whitelist approval is pending"+lf+"Please be patient, i will send you a message");
        }

        static void ProcessStateApproved(WhitelistItem chat, Telegram.Bot.Types.Message message)
        {
            SavixBot.SendMessage(message, chat, "Your whitelist application got approved."+lf+"You are ready to participate in the presale."+lf+"Kindly head over to our <a href=\"http://savix.org/presale\">Presale DApp</a> to contribute");
        }

        static void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            // ignore other bots
            if (e.Message.From.IsBot)
                return;

            if (Paused)
            {
                SendMessage(e.Message, null, "Bot is in pause mode, please try again later...");
                return;
            }

            if (String.IsNullOrEmpty(e.Message.From.Username))
            {
                SendMessage(e.Message, null, "Please set a username to use this bot!"+lf+"Go to Settings > Edit Profile > Set Username");
                return;
            }

            if (e.Message.Text != null)
            {
                try
                {
                    if (!IsMember(e.Message.From.Id, -1001389305639))
                    {
                        SendMessage(e.Message, null, "You must be a member of the Savix channel (@savixhq) to continue"+lf+"Please join....");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    SendMessage(e.Message, null, "Error: " + ex.Message + lf+ "Please try again...");
                    return;
                }

                WhitelistItem chat = Whitelist.GetOrAddItem(e.Message.Chat.Id, e.Message.Chat.FirstName, e.Message.Chat.LastName,
                        e.Message.Date, e.Message.Chat.Username, e.Message.From.LanguageCode);

                chat.ChatProtocol.Add(e.Message.Text);
                switch (chat.State)
                {
                    case WhiteListStateEnum.zero:
                        ProcessStateZero(chat, e.Message);
                        break;
                    case WhiteListStateEnum.wait_for_address:
                        ProcessStateWaitForAddress(chat, e.Message);
                        break;
                    case WhiteListStateEnum.wait_for_amount:
                        ProcessStateWaitForAmount(chat, e.Message);
                        break;
                    case WhiteListStateEnum.wait_for_terms:
                        ProcessStateWaitForTerms(chat, e.Message);
                        break;
                    case WhiteListStateEnum.wait_for_approval:
                        ProcessStateWaitForApproval(chat, e.Message);
                        break;
                    case WhiteListStateEnum.approved:
                        ProcessStateApproved(chat, e.Message);
                        break;
                }
            }
        }

        public static void StartBot()
        {
            bot.OnMessage += BotClient_OnMessage;
            bot.OnUpdate += Bot_OnUpdate;
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
                    bot.SendTextMessageAsync(e.Update.ChannelPost.Chat, "in order to join our presale you have to chat with @SavixRobot and type /start to begin whitelist procedure", Telegram.Bot.Types.Enums.ParseMode.Html);
                    lastWhitelistMsgDate = DateTime.Now;
                }
            }
        }
    }
}
