using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SavixBot
{
    public class WFGiveaway : Workflow
    {
        static Int32 _count = 0;
        public static Int32 Count
        {
            get
            {
                return _count;
            }
        }

        static string _file = "count.txt";

        object lockFile = new object();

        static Int32 _maxCount = 500;
        public static Int32 MaxCount
        {
            get
            {
                return _maxCount;
            }

        }

        static Regex discordRx = new Regex(@".*?\#\d+");

        static string _welcome = "Welcome {0} to our first Savix Giveaway." + lf +
                     "You can earn <b>0.5 SVX</b> (<b>15$</b>)"+lf+lf+"... just by completing a few simple tasks!" + lf + lf +
                    "In order to be eligible, you need:" + lf + lf +
                    "<i><b>- Twitter account</b></i>" + lf + lf +
                    "<i><b>- Discord account</b></i>" + lf + lf +
                    "<i><b>- ETH wallet to receive your SVX</b></i>" + lf + lf +
                    "Ready ? Type <b><i>yes</i></b> to start the giveaway...";

        public static void LoadCount()
        {
            string file = Path.Combine(Environment.CurrentDirectory, _file);
            if (!System.IO.File.Exists(file))
                return;

            _count = Convert.ToInt32(System.IO.File.ReadAllText(file));
        }

        static void SaveCount()
        {
            string file = Path.Combine(Environment.CurrentDirectory, _file);
            System.IO.File.WriteAllText(file, _count.ToString());
        }



        static void ProcessStateZero(User user, Telegram.Bot.Types.Message message)
        {
            if (message.Text.StartsWith("/giveaway"))
            {
                if (_count < _maxCount)
                {
                    user.Workflow = "giveaway";
                    SendImage(message, "https://savix.org/wp-content/uploads/2020/11/savix_giveaway2.jpg", String.Format(_welcome, message.Chat.FirstName));
                    user.GiveawayItem.State = GiveawayStateEnum.wait_for_start;
                }
                else
                {
                    string caption = "I am sorry, you came too late." + lf + "Our first Savix giveaway already ended!"+
                        " This will not be our last giveaway. Watch out for first announcements on one of our social channels, be sure to join them:"+lf+lf+
                        "-Twitter: https://twitter.com/savix_org" + lf+"-Telegram: @savix_org"+lf+ "-Discord: https://discord.gg/22bxBnP4GD";
                    SendImage(message, "https://savix.org/wp-content/uploads/2020/11/savix_giveaway2.jpg", caption);
                }
            }
            return;
        }

        static void ProcessStateWaitForStart(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().ToLower();
            if (msg == "no")
            {
                user.GiveawayItem.State = GiveawayStateEnum.zero;
                user.GiveawayItem.EthAddress = null;
                user.GiveawayItem.Twitter = null;
                user.GiveawayItem.Discord = null;
                SendMessage(message, user, "Sorry to hear that, if you want, you can restart the giveaway at any time.");
                return;
            }
            else if (msg != "yes")
            {
                SendImage(message, "https://savix.org/wp-content/uploads/2020/11/savix_giveaway2.jpg", String.Format(_welcome, message.Chat.FirstName));
                return;
            }

            SendMessage(message, user, "Please enter your twitter account name which starts with a @ ?");
            user.GiveawayItem.State = GiveawayStateEnum.wait_for_twitter;
            return;
        }

        static void ProcessStateWaitForTwitter(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().ToLower();
            if (!msg.StartsWith("@"))
            {
                SendMessage(message, user, "Your Twitter name must start with a @, please try again");
                return;
            }

            user.GiveawayItem.Twitter = msg;
            SendMessage(message, user, "What is your discord account name, please enter the full name with # and number");
            user.GiveawayItem.State = GiveawayStateEnum.wait_for_discord;
            return;
        }
        static void ProcessStateWaitForDiscord(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().ToLower();
            if (!msg.Contains("#"))
            {
                SendMessage(message, user, "Your Discord name must include # followed by a number");
                SendImage(message, "https://savix.org/wp-content/uploads/2020/11/discord_name.jpg", "<b>Picture shows you how to access your discord name</b>"+lf+"Please try again...");
                return;
            }

            if (!discordRx.IsMatch(msg))
            {
                SendMessage(message, user, "Not a valid discord name, your name must have the format: yourname#123");
                SendImage(message, "https://savix.org/wp-content/uploads/2020/11/discord_name.jpg", "<b>Picture shows you how to access your discord name</b>" + lf + "Please try again...");
                return;
            }

            if (msg == "savix#6028")
            {
                SendMessage(message, user, "This is our discord name, we want your name, please try again...");
                return;
            }


            user.GiveawayItem.Discord = msg;
            SendMessage(message, user, "Please enter your ETH wallet address, we need it to send you the SVX (format 0x)");
            user.GiveawayItem.State = GiveawayStateEnum.wait_for_ethaddress;
            return;
        }

        static void ProcessStateWaitForAddress(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim();
            if (!msg.StartsWith("0x"))
            {
                SendMessage(message, user, "Your ETH wallet address must start with 0x, please try again...");
                return;
            }
            else if (msg.Length != 42)
            {
                SendMessage(message, user, "Wrong length of ETH address, please try again...");
                return;
            }

            SendMessage(message, user, "Verifying your ETH wallet address on the blockchain (can take a few seconds)");
            string value = VerifyEthAddress(msg);
            if (value == null)
            {
                SendMessage(message, user, "Unknown or Invalid ETH address, please try again...");
            }

            user.GiveawayItem.EthAddress = msg;
            user.GiveawayItem.State = GiveawayStateEnum.wait_for_task;
            SendMessage(message, user, "You have to complete the following small tasks to receive the giveaway:"+lf+lf+ "1. <b>Follow</b> our twitter account: https://twitter.com/savix_org"+lf+
            "2. <b>Retweet</b> one of our tweets"+lf+"3. <b>Join</b> our discord server: https://discord.gg/22bxBnP4GD"+lf+lf+
            "Type <b><i>done</i></b>, if you did the above tasks."+lf+lf+
            "No need to hurry, you can come back to the bot at any time.");
            return;
        }

        static void ProcessStateWaitForTask(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().ToLower();
            if (msg == "no" || msg=="quit")
            {
                user.GiveawayItem.State = GiveawayStateEnum.zero;
                user.GiveawayItem.EthAddress = null;
                user.GiveawayItem.Twitter = null;
                user.GiveawayItem.Discord = null;
                SendMessage(message, user, "Sorry to hear that, if you want, you can restart the giveaway at any time.");
                return;
            }
            else if (msg != "done")
            {
                SendMessage(message, user, "You have to complete the following small tasks to receive the giveaway:" + lf + lf + "1. <b>Follow</b> our twitter account: https://twitter.com/savix_org" + lf +
                "2. <b>Retweet</b> one of our tweets" + lf + "3. <b>Join</b> our discord server: https://discord.gg/22bxBnP4GD" + lf + lf +
                "Type <b><i>done</i></b>, if you did the above tasks." + lf + lf +
                "No need to hurry, you can come back to the bot at any time.");
                return;
            }

            SendMessage(message, user, "Well done, as a last step please confirm that we got your information right:"+lf+lf+
                "Twitter: " + user.GiveawayItem.Twitter+lf+"Discord:"+user.GiveawayItem.Discord+lf+
                "Eth Address: " + user.GiveawayItem.EthAddress+lf+lf+"Please type <b><i>yes</i></b> if the above information is correct");
            user.GiveawayItem.State = GiveawayStateEnum.wait_for_confirmation;
            return;
        }

        static void ProcessStateWaitForConfirmation(User user, Telegram.Bot.Types.Message message)
        {
            string msg = message.Text.Trim().ToLower();
            if (msg == "no" || msg == "quit")
            {
                user.GiveawayItem.State = GiveawayStateEnum.zero;
                user.GiveawayItem.EthAddress = null;
                user.GiveawayItem.Twitter = null;
                user.GiveawayItem.Discord = null;
                SendMessage(message, user, "Sorry to hear that, if you want, you can restart the giveaway at any time.");
                return;
            }
            else if (msg != "yes")
            {
                SendMessage(message, user, "As a last step please confirm that we got your information right." + lf + lf +
                    "Twitter: " + user.GiveawayItem.Twitter + ", " + "Discord: " + user.GiveawayItem.Discord + lf +
                    "Eth Address: " + user.GiveawayItem.EthAddress + lf + lf + "Please type <b><i>yes</i></b> if the above information is correct");
                return;
            }
            SendMessage(-1001354512306, user, String.Format("Congratulations {0}! You joined our giveaway. You will receive 0.5 SVX (15$) after our presale.", user.FirstName));
            SendMessage(message, user, "Congratulations! You will automatically receive 0.5 SVX 48-72 hours after our presale ends."+lf+"Please stay in the channels until the presale ends.");
            user.GiveawayItem.State = GiveawayStateEnum.confirmed;
            _count++;
            SaveCount();
            return;
        }

        static void ProcessStateConfirmed(User user, Telegram.Bot.Types.Message message)
        {
            SendMessage(message, user, "Congratulations! You will receive 0.5 SVX 48-72 hours after our presale ends." + lf + "Please stay in the channels until the presale ends.");
            return;
        }
        public static void DoGiveaway(User user, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (user.GiveawayItem == null)
                user.GiveawayItem = new GiveawayItem();

            switch (user.GiveawayItem.State)
            {
                case GiveawayStateEnum.zero:
                    ProcessStateZero(user, e.Message);
                    break;
                case GiveawayStateEnum.wait_for_start:
                    ProcessStateWaitForStart(user, e.Message);
                    break;
                case GiveawayStateEnum.wait_for_twitter:
                    ProcessStateWaitForTwitter(user, e.Message);
                    break;
                case GiveawayStateEnum.wait_for_discord:
                    ProcessStateWaitForDiscord(user, e.Message);
                    break;
                case GiveawayStateEnum.wait_for_ethaddress:
                    ProcessStateWaitForAddress(user, e.Message);
                    break;
                case GiveawayStateEnum.wait_for_task:
                    ProcessStateWaitForTask(user, e.Message);
                    break;
                case GiveawayStateEnum.wait_for_confirmation:
                    ProcessStateWaitForConfirmation(user, e.Message);
                    break;
                case GiveawayStateEnum.confirmed:
                    ProcessStateConfirmed(user, e.Message);
                    break;
            }
        }
    }
}
