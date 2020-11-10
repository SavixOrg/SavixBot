using Argos.Base.Json;
using Argos.Base.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SavixBot
{
    public enum WhiteListStateEnum { zero, wait_for_address, wait_for_amount, wait_for_terms, wait_for_approval, approved  };
    public static class Whitelist
    {
        static Dictionary<long, WhitelistItem> _dict = new Dictionary<long, WhitelistItem>();
        static List<WhitelistItem> _list = new List<WhitelistItem>();

        static HashSet<string> _ethHash = new HashSet<string>();

        static object lockObjDict = new object();
        static object lockObjList = new object();
        static string _lastHash = "";

        public static bool HasChanged { get; set; }

        public static void AddEthAddress(string addr)
        {
            _ethHash.Add(addr);
        }

        public static bool IsEthAddressNew(string addr)
        {
            return !_ethHash.Contains(addr);
        }

        public static WhitelistItem GetOrAddItem(long id, string firstName, string lastName, DateTime date, string username, string language)
        {
            WhitelistItem item;
            lock (lockObjDict)
            {
                if (_dict.ContainsKey(id))
                {
                    item = _dict[id];
                    item.LastMessageDate = date;
                    return item;
                }
            }

            item = new WhitelistItem()
            {
                Id = id,
                State = WhiteListStateEnum.zero,
                FirstName = firstName,
                LastName = lastName,
                FirstMessageDate = date,
                LastMessageDate = date,
                Username = username,
                LanguageCode = language,
                ChatProtocol = new List<string>()
            };

            lock (lockObjList)
                _list.Add(item);

            lock (lockObjDict)
                _dict.Add(id, item);

            return item;
        }

        public static List<WhitelistItem> GetItems()
        {
            return _list;
        }

        public static void Save(string filename)
        {
            lock (lockObjList)
            {
                if (File.Exists(filename))
                    File.Delete(filename);

                foreach (var item in _list)
                    File.AppendAllText(filename,JsonObject.FromObject(item).ToString()+Environment.NewLine);
            }
        }

        public static void Load(string filename)
        {
            if (!File.Exists(filename))
                return;

            var lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                if (String.IsNullOrEmpty(line))
                    continue;

                WhitelistItem item = JsonObject.FromString(line).ToObject<WhitelistItem>();
                _dict.Add(item.Id, item);
                _list.Add(item);
                if (!String.IsNullOrEmpty(item.EthAddress))
                    _ethHash.Add(item.EthAddress);
                Whitelist.HasChanged = false;
            }
        }
    }
}
