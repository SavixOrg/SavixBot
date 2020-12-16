using Argos.Base.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SavixBot
{
    public static class UserController
    {
        static Dictionary<long, User> _dict = new Dictionary<long, User>();
        static List<User> _list = new List<User>();

        static object lockObjDict = new object();
        static object lockObjList = new object();

        public static bool HasChanged { get; set; }

        static HashSet<string> _ethHash = new HashSet<string>();

        public static void AddEthAddress(string addr)
        {
            _ethHash.Add(addr);
        }

        public static bool IsEthAddressNew(string addr)
        {
            return !_ethHash.Contains(addr);
        }


        public static User GetOrAddItem(long id, string firstName, string lastName, DateTime date, string username, string language)
        {
            User item;
            lock (lockObjDict)
            {
                if (_dict.ContainsKey(id))
                {
                    item = _dict[id];
                    item.LastMessageDate = date;
                    return item;
                }
            }

            item = new User()
            {
                Id = id,
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

        public static List<User> GetItems()
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
                    File.AppendAllText(filename, JsonObject.FromObject(item).ToString() + Environment.NewLine);
            }
        }

        public static void ClearChatlogs()
        {
            foreach (var user in _list)
            {
                user.ChatProtocol.Clear();
            }
            HasChanged = true;
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

                User item = JsonObject.FromString(line).ToObject<User>();
                _dict.Add(item.Id, item);
                _list.Add(item);
                if (item.WhitelistItem != null && !String.IsNullOrEmpty(item.WhitelistItem.EthAddress))
                    _ethHash.Add(item.WhitelistItem.EthAddress);
                UserController.HasChanged = false;
            }
        }
    }
}
