using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavixBot
{
    public enum GiveawayStateEnum { zero, wait_for_start, wait_for_twitter, wait_for_discord, wait_for_ethaddress, wait_for_task, wait_for_confirmation, confirmed };

    public class GiveawayItem
    {
        public string Twitter { get; set; }
        public string Discord { get; set; }
        public string EthAddress { get; set; }

        GiveawayStateEnum state;
        public GiveawayStateEnum State
        {
            get
            {
                return state;
            }
            set
            {
                UserController.HasChanged = true;
                state = value;
            }
        }

    }
}
