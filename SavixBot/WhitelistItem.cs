using System;
using System.Collections.Generic;
using System.Linq;

namespace SavixBot
{
    public class WhitelistItem
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EthAddress { get; set; }
        public string EthBalance { get; set; }
        public double Contribution { get; set; }
        public string LanguageCode { get; set; }
        public DateTime FirstMessageDate { get; set; }
        public DateTime LastMessageDate { get; set; }

        WhiteListStateEnum state;
        public WhiteListStateEnum State
        {
            get
            {
                return state;
            }
            set
            {
                Whitelist.HasChanged = true;
                state = value;
            }
        }

        public List<string> ChatProtocol { get; set; }

    }
}
