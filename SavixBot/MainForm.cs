using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telegram.Bot;

namespace SavixBot
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        static string _key = "1124987934:AAGux6ybOZf9JINKbfhOmobLpAvd4xA2xXU";
        string _file = Path.Combine(Environment.CurrentDirectory, "whitelist.json");

        Char lf = (char)10;


        static TelegramBotClient bot = new TelegramBotClient(_key);
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Whitelist.Load(_file);
            gridControlWhitelist.DataSource = Whitelist.GetItems();
            SavixBot.StartBot();
            simpleButtonStart.Enabled = false;
            simpleButtonStop.Enabled = true;
            timerWhitelist.Interval = 10000;
            timerWhitelist.Start();
        }


        private void timerWhitelist_Tick(object sender, EventArgs e)
        {
            if (!Whitelist.HasChanged)
                return;

            gridControlWhitelist.DataSource = Whitelist.GetItems();
            gridControlWhitelist.RefreshDataSource();

            string file = Path.Combine(Environment.CurrentDirectory, "whitelist.json");
            Whitelist.Save(file);
            Whitelist.HasChanged = false;
        }

        private void simpleButtonStop_Click(object sender, EventArgs e)
        {
            SavixBot.Paused = true;
            simpleButtonStop.Enabled = false;
            simpleButtonStart.Enabled = true;
        }

        private void simpleButtonStart_Click(object sender, EventArgs e)
        {
            SavixBot.Paused = false;
            simpleButtonStop.Enabled = true;
            simpleButtonStart.Enabled = false;
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButtonApprove_Click(object sender, EventArgs e)
        {
            int[] rows = gridViewWhitelist.GetSelectedRows();

            if (rows == null)
                return;

            foreach (int row in rows)
            {
                if (row < 0)
                    continue;

                var item = gridViewWhitelist.GetRow(row) as WhitelistItem;

                if (item.State != WhiteListStateEnum.wait_for_approval)
                    continue;

                item.State = WhiteListStateEnum.approved;
                SavixBot.SendMessage(item.Id, item, "Your whitelist application got approved."+lf+"You are ready to participate in the presale"+lf+"Head over to our <a href=\"http://savix.org/presale\">Presale DApp</a> to contribute");
            }
        }
    }
}
