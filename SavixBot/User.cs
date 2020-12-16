using Argos.Base.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavixBot
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LanguageCode { get; set; }
        public string Workflow { get; set; }
        public DateTime FirstMessageDate { get;set;}
        public DateTime LastMessageDate { get; set; }
        public List<string> ChatProtocol { get; set; }

        public WhitelistItem WhitelistItem { get; set; }
        public GiveawayItem GiveawayItem { get; set; }
    }
}
