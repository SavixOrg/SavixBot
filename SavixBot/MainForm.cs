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
        string _file = Path.Combine(Environment.CurrentDirectory, "users.json");
        Char lf = (char)10;
       
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UserController.Load(_file);
            WFGiveaway.LoadCount();
            labelGiveaway.Text = "Giveaway Count: " + WFGiveaway.Count;
            gridControlWhitelist.DataSource = UserController.GetItems();
            SavixBot.StartBot();
            simpleButtonStart.Enabled = false;
            simpleButtonStop.Enabled = true;
            timerWhitelist.Interval = 10000;
            timerWhitelist.Start();
        }


        private void timerWhitelist_Tick(object sender, EventArgs e)
        {
            if (!UserController.HasChanged)
                return;

            gridControlWhitelist.DataSource = UserController.GetItems();
            gridControlWhitelist.RefreshDataSource();

            labelGiveaway.Text = "Giveaway Count: " + WFGiveaway.Count;

            string file = Path.Combine(Environment.CurrentDirectory, _file);
            UserController.Save(file);
            UserController.HasChanged = false;
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

                var user = gridViewWhitelist.GetRow(row) as User;

                if (user.WhitelistItem.State != WhiteListStateEnum.wait_for_approval)
                    continue;

                user.WhitelistItem.State = WhiteListStateEnum.approved;
                Workflow.SendMessage(user.Id, user, "Your whitelist application got approved."+lf+"You are ready to participate in the presale"+lf+"Head over to our <a href=\"http://savix.org/presale/dapp\">Presale DApp</a> to contribute");
            }
        }

        private void simpleButtonResetGiveaway_Click(object sender, EventArgs e)
        {
            var user = gridViewWhitelist.GetFocusedRow() as User;
            if (user == null)
                return;

            user.GiveawayItem = null;
            UserController.HasChanged = true;
        }

        private void simpleButtonClearChatlogs_Click(object sender, EventArgs e)
        {
            UserController.ClearChatlogs();
        }
    }
}
