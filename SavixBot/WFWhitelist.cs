using Argos.Base.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SavixBot
{
    public class Workflow
    {
        static TelegramBotClient _bot;
        public static Char lf = (char)10;


        public static void SetBot(TelegramBotClient bot)
        {
            _bot = bot;
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

        static async public void SendImage(Telegram.Bot.Types.Message message, string url, string caption)
        {
            try
            {
                Message returnmsg = await _bot.SendPhotoAsync(message.From.Id, photo: url, caption: caption, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            }
            catch (Exception e)
            {
                SendMessage(message.From.Id, null, "Can't inititate conversation, please enter into a private chat with @SavixRobot and type /giveaway");
            }
        }

        static async public void SendImage(long chatId, string url, string caption, Telegram.Bot.Types.Enums.ParseMode parsemode = Telegram.Bot.Types.Enums.ParseMode.Markdown)
        {
            try
            {
                Message returnmsg = await _bot.SendPhotoAsync(chatId, url, caption, parsemode);
            }
            catch (Exception e)
            {
            }
        }

        static async public void SendMessage(long chatId, User user, string text)
        {
            try
            {
                if (user != null)
                    user.ChatProtocol.Add("bot: " + text);
                await _bot.SendTextMessageAsync((Telegram.Bot.Types.ChatId)chatId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
            } catch (Exception e)
            {
            }
        }

        static async public void SendMessage(Telegram.Bot.Types.Message message, User user, string text)
        {
            try
            {
                if (user != null)
                    user.ChatProtocol.Add("bot: " + text);
                await _bot.SendTextMessageAsync(message.From.Id, text, Telegram.Bot.Types.Enums.ParseMode.Html);
            }
            catch (Exception e)
            {
            }
        }
    }

    public class WFWhitelist : Workflow
    {
        static bool presaleStarted = false;
        const double MIN_CONTRIBUTION = 0.5;
        const double MAX_CONTRIBUTION = 15;


        static void ProcessStateZero(User user, Telegram.Bot.Types.Message message)
        {
            if (message.Text.StartsWith("/start"))
            {
                if (!presaleStarted)
                {
                    SendMessage(message, user, "Thank you for your interest in Savix !" + lf + @"Our whitelist process did not start yet" + lf + @"Please head over to our channel @savix_org for latest news" + lf + @"Also visit our <a href=""https://savix.org"">Website</a> or check out our <a href=""https://savix.org/wp-content/uploads/2020/11/SAVIX_Whitepaper.pdf"">Whitepaper</a>");
                    return;
                }

                user.Workflow = "whitelist";
                SendMessage(message, user, "You are about to apply for a spot in our whitelist. Only whitelisted members are able to contribute to our presale!" + lf + lf +
                 "In order to minimize the possibility for participants to \"cheat\" the system and contribute more than 15 ETH, a whitelist system based on telegram accounts has been implemented in combination with ethereum contribution addresses stored in the Savix presale contract." + lf + lf +
                "Since telegram requires a phone number, it is much harder to obtain multiple accounts compared to E-Mail addresses or the like. You need the following pre-requisites to participate in the Savix presale:" + lf + lf +
                "<i><b>- Ethereum Wallet</b></i> used for ether contribution and to receive Savix." + lf + lf +
                "<i><b>- Metamask</b></i>" + lf + lf + "please type <b><i>yes</i></b> if you are ready to start the whitelist process ?");
                user.WhitelistItem.State = WhiteListStateEnum.wait_for_start;
            }
            return;
        }

        static void ProcessStateWaitForStart(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().ToLower();
            if (msg == "no")
            {
                user.WhitelistItem.State = WhiteListStateEnum.zero;
                user.WhitelistItem.EthAddress = null;
                user.WhitelistItem.Contribution = 0;
                user.WhitelistItem.EthBalance = null;
                SendMessage(message, user, "Sorry to hear that, if you want, you can restart the whitelist process at any time.");
                return;
            }
            else if (msg != "yes")
            {
                SendMessage(message, user, "You are about to apply for a spot in our whitelist. Only whitelisted members are able to contribute to our presale!" + lf + lf +
                 "In order to minimize the possibility for participants to \"cheat\" the system and contribute more than 15 ETH, a whitelist system based on telegram accounts has been implemented in combination with ethereum contribution addresses stored in the Savix presale contract." + lf + lf +
                "Since telegram requires a phone number, it is much harder to obtain multiple accounts compared to E-Mail addresses or the like. You need the following pre-requisites to participate in the Savix presale:" + lf + lf +
                "<i><b>- Ethereum Wallet</b></i> used for ether contribution and to receive Savix." + lf + lf +
                "<i><b>- Metamask</b></i>" + lf + lf + "please type <b><i>yes</i></b> if you are ready to start the whitelist process ?");
                return;
            }

            SendMessage(message, user, "Please enter your ETH address that you will be sending the funds from (format 0x)");
            user.WhitelistItem.State = WhiteListStateEnum.wait_for_address;
            return;
        }

        static void ProcessStatePresaleWaitForAddress(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim();
            if (!msg.StartsWith("0x"))
            {
                SendMessage(message, user, "Your ETH address must start with 0x, please try again...");
                return;
            }
            else if (msg.Length != 42)
            {
                SendMessage(message, user, "Wrong length of ETH address, please try again...");
                return;
            }

            SendMessage(message, user, "Verifying your ETH address on the blockchain (can take a few seconds)");
            string value = VerifyEthAddress(msg);
            if (value == null)
            {
                SendMessage(message, user, "Unknown or Invalid ETH address, please try again...");
            }

            user.WhitelistItem.EthAddress = msg;
            user.WhitelistItem.EthBalance = value;
            user.WhitelistItem.State = WhiteListStateEnum.wait_for_amount;
            SendMessage(message, user, String.Format("How much ETH do you want to contribute ?" + lf + "(0.5 ETH minimum, 15 ETH maximum)", message.Chat.FirstName));
            return;
        }

        static void ProcessStatePresaleWaitForAmount(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().Replace(".", ",");
            double amount = 0;
            try
            {
                amount = Convert.ToDouble(msg);
            }
            catch (Exception e)
            {
                SendMessage(message, user, "Incorrect number format, please try again...");
                SendMessage(message, user, String.Format("How much ETH do you want to contribute ?" + lf + "(0.5 ETH minimum, 15 ETH maximum)", message.Chat.FirstName));
                return;
            }
            if (amount < MIN_CONTRIBUTION)
            {
                SendMessage(message, user, "That's not enough, minimum is 0.5 ETH");
                return;
            }
            else if (amount > MAX_CONTRIBUTION)
            {
                SendMessage(message, user, "That's too much, maximum is 15 ETH");
                return;
            }

            user.WhitelistItem.Contribution = amount;
            user.WhitelistItem.State = WhiteListStateEnum.wait_for_terms;
            SendMessage(message, user, @"Please type <b>yes</b> if you agree to our <a href=""https://savix.org/terms"">Terms and Conditions</a>");
            return;
        }

        static void ProcessStatePresaleWaitForTerms(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().ToLower();
            if (msg == "no")
            {
                user.WhitelistItem.State = WhiteListStateEnum.zero;
                user.WhitelistItem.EthAddress = null;
                user.WhitelistItem.Contribution = 0;
                user.WhitelistItem.EthBalance = null;
                SendMessage(message, user, "Sorry to hear that, if you want, you can restart the whitelist process at any time.");
                return;
            }
            else if (msg != "yes")
            {
                SendMessage(message, user, @"Please type <b>yes</b> if you agree to our <a href=""https://savix.org/terms"">Terms and Conditions</a>");
                return;
            }

            user.WhitelistItem.State = WhiteListStateEnum.wait_for_approval;
            SendMessage(message, user, String.Format("Thanks {0}, whitelist application successful." + lf + "I will send you a message after your approval", message.Chat.FirstName));
            user.Workflow = null;
            return;
        }

        static void ProcessStatePresaleWaitForApproval(User user, Telegram.Bot.Types.Message message)
        {
            SendMessage(message, user, "Your whitelist approval is pending" + lf + "Please be patient, i will send you a message");
        }

        static void ProcessStatePresaleApproved(User user, Telegram.Bot.Types.Message message)
        {
            SendMessage(message, user, "Your whitelist application got approved." + lf + "You are ready to participate in the presale." + lf + "Kindly head over to our <a href=\"http://savix.org/presale/dapp\">Presale DApp</a> to contribute");
        }

        public static void DoWhitelist(User user, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (user.WhitelistItem == null)
                user.WhitelistItem = new WhitelistItem();

            switch (user.WhitelistItem.State)
            {
                case WhiteListStateEnum.zero:
                    ProcessStateZero(user, e.Message);
                    break;
                case WhiteListStateEnum.wait_for_start:
                    ProcessStateWaitForStart(user, e.Message);
                    break;
                case WhiteListStateEnum.wait_for_address:
                    ProcessStatePresaleWaitForAddress(user, e.Message);
                    break;
                case WhiteListStateEnum.wait_for_amount:
                    ProcessStatePresaleWaitForAmount(user, e.Message);
                    break;
                case WhiteListStateEnum.wait_for_terms:
                    ProcessStatePresaleWaitForTerms(user, e.Message);
                    break;
                case WhiteListStateEnum.wait_for_approval:
                    ProcessStatePresaleWaitForApproval(user, e.Message);
                    break;
                case WhiteListStateEnum.approved:
                    ProcessStatePresaleApproved(user, e.Message);
                    break;
            }
        }
    }
}
