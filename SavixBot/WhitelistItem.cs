using System;
using System.Collections.Generic;
using System.Linq;

namespace SavixBot
{
    public enum WhiteListStateEnum { zero, wait_for_start, wait_for_address, wait_for_amount, wait_for_terms, wait_for_approval, approved};
    public class WhitelistItem
    {
        public string EthAddress { get; set; }
        public string EthBalance { get; set; }
        public double Contribution { get; set; }

        WhiteListStateEnum state;
        public WhiteListStateEnum State
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
