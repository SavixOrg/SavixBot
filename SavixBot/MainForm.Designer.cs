namespace SavixBot
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelGiveaway = new System.Windows.Forms.Label();
            this.simpleButtonApprove = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonExit = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonStop = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonStart = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlWhitelist = new DevExpress.XtraGrid.GridControl();
            this.gridViewWhitelist = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnUsername = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnLanguage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnWorkflow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFirstMessage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnLastMessage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEthAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEthBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Contribution = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTwitter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDiscord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEthAdrGiveaway = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnStateGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnStatePS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.timerWhitelist = new System.Windows.Forms.Timer(this.components);
            this.simpleButtonResetGiveaway = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonClearChatlogs = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlWhitelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWhitelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButtonClearChatlogs);
            this.layoutControl1.Controls.Add(this.simpleButtonResetGiveaway);
            this.layoutControl1.Controls.Add(this.labelGiveaway);
            this.layoutControl1.Controls.Add(this.simpleButtonApprove);
            this.layoutControl1.Controls.Add(this.simpleButtonExit);
            this.layoutControl1.Controls.Add(this.simpleButtonStop);
            this.layoutControl1.Controls.Add(this.simpleButtonStart);
            this.layoutControl1.Controls.Add(this.gridControlWhitelist);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(893, 340, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(945, 418);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelGiveaway
            // 
            this.labelGiveaway.Location = new System.Drawing.Point(12, 38);
            this.labelGiveaway.Name = "labelGiveaway";
            this.labelGiveaway.Size = new System.Drawing.Size(921, 20);
            this.labelGiveaway.TabIndex = 9;
            this.labelGiveaway.Text = "label1";
            // 
            // simpleButtonApprove
            // 
            this.simpleButtonApprove.Location = new System.Drawing.Point(220, 12);
            this.simpleButtonApprove.Name = "simpleButtonApprove";
            this.simpleButtonApprove.Size = new System.Drawing.Size(105, 22);
            this.simpleButtonApprove.StyleController = this.layoutControl1;
            this.simpleButtonApprove.TabIndex = 8;
            this.simpleButtonApprove.Text = "Approve";
            this.simpleButtonApprove.Click += new System.EventHandler(this.simpleButtonApprove_Click);
            // 
            // simpleButtonExit
            // 
            this.simpleButtonExit.Location = new System.Drawing.Point(836, 12);
            this.simpleButtonExit.Name = "simpleButtonExit";
            this.simpleButtonExit.Size = new System.Drawing.Size(97, 22);
            this.simpleButtonExit.StyleController = this.layoutControl1;
            this.simpleButtonExit.TabIndex = 7;
            this.simpleButtonExit.Text = "Exit";
            this.simpleButtonExit.Click += new System.EventHandler(this.simpleButtonExit_Click);
            // 
            // simpleButtonStop
            // 
            this.simpleButtonStop.Location = new System.Drawing.Point(120, 12);
            this.simpleButtonStop.Name = "simpleButtonStop";
            this.simpleButtonStop.Size = new System.Drawing.Size(96, 22);
            this.simpleButtonStop.StyleController = this.layoutControl1;
            this.simpleButtonStop.TabIndex = 6;
            this.simpleButtonStop.Text = "Stop Bot";
            this.simpleButtonStop.Click += new System.EventHandler(this.simpleButtonStop_Click);
            // 
            // simpleButtonStart
            // 
            this.simpleButtonStart.Location = new System.Drawing.Point(12, 12);
            this.simpleButtonStart.Name = "simpleButtonStart";
            this.simpleButtonStart.Size = new System.Drawing.Size(94, 22);
            this.simpleButtonStart.StyleController = this.layoutControl1;
            this.simpleButtonStart.TabIndex = 5;
            this.simpleButtonStart.Text = "Start Bot";
            this.simpleButtonStart.Click += new System.EventHandler(this.simpleButtonStart_Click);
            // 
            // gridControlWhitelist
            // 
            this.gridControlWhitelist.Location = new System.Drawing.Point(12, 62);
            this.gridControlWhitelist.MainView = this.gridViewWhitelist;
            this.gridControlWhitelist.Name = "gridControlWhitelist";
            this.gridControlWhitelist.Size = new System.Drawing.Size(921, 344);
            this.gridControlWhitelist.TabIndex = 4;
            this.gridControlWhitelist.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewWhitelist});
            // 
            // gridViewWhitelist
            // 
            this.gridViewWhitelist.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnId,
            this.gridColumnUsername,
            this.gridColumnFirstName,
            this.gridColumnLastName,
            this.gridColumnLanguage,
            this.gridColumnWorkflow,
            this.gridColumnFirstMessage,
            this.gridColumnLastMessage,
            this.gridColumnEthAddress,
            this.gridColumnEthBalance,
            this.Contribution,
            this.gridColumnTwitter,
            this.gridColumnDiscord,
            this.gridColumnEthAdrGiveaway,
            this.gridColumnStateGA,
            this.gridColumnStatePS});
            this.gridViewWhitelist.GridControl = this.gridControlWhitelist;
            this.gridViewWhitelist.Name = "gridViewWhitelist";
            this.gridViewWhitelist.OptionsBehavior.Editable = false;
            this.gridViewWhitelist.OptionsSelection.MultiSelect = true;
            // 
            // gridColumnId
            // 
            this.gridColumnId.Caption = "Id";
            this.gridColumnId.FieldName = "Id";
            this.gridColumnId.Name = "gridColumnId";
            this.gridColumnId.Visible = true;
            this.gridColumnId.VisibleIndex = 0;
            // 
            // gridColumnUsername
            // 
            this.gridColumnUsername.Caption = "Username";
            this.gridColumnUsername.FieldName = "Username";
            this.gridColumnUsername.Name = "gridColumnUsername";
            this.gridColumnUsername.Visible = true;
            this.gridColumnUsername.VisibleIndex = 1;
            // 
            // gridColumnFirstName
            // 
            this.gridColumnFirstName.Caption = "FirstName";
            this.gridColumnFirstName.FieldName = "FirstName";
            this.gridColumnFirstName.Name = "gridColumnFirstName";
            this.gridColumnFirstName.Visible = true;
            this.gridColumnFirstName.VisibleIndex = 2;
            // 
            // gridColumnLastName
            // 
            this.gridColumnLastName.Caption = "LastName";
            this.gridColumnLastName.FieldName = "LastName";
            this.gridColumnLastName.Name = "gridColumnLastName";
            this.gridColumnLastName.Visible = true;
            this.gridColumnLastName.VisibleIndex = 3;
            // 
            // gridColumnLanguage
            // 
            this.gridColumnLanguage.Caption = "Language";
            this.gridColumnLanguage.FieldName = "LanguageCode";
            this.gridColumnLanguage.Name = "gridColumnLanguage";
            this.gridColumnLanguage.Visible = true;
            this.gridColumnLanguage.VisibleIndex = 4;
            // 
            // gridColumnWorkflow
            // 
            this.gridColumnWorkflow.Caption = "Workflow";
            this.gridColumnWorkflow.FieldName = "Workflow";
            this.gridColumnWorkflow.Name = "gridColumnWorkflow";
            this.gridColumnWorkflow.Visible = true;
            this.gridColumnWorkflow.VisibleIndex = 5;
            // 
            // gridColumnFirstMessage
            // 
            this.gridColumnFirstMessage.Caption = "FirstMessage";
            this.gridColumnFirstMessage.FieldName = "FirstMessageDate";
            this.gridColumnFirstMessage.Name = "gridColumnFirstMessage";
            this.gridColumnFirstMessage.Visible = true;
            this.gridColumnFirstMessage.VisibleIndex = 6;
            // 
            // gridColumnLastMessage
            // 
            this.gridColumnLastMessage.Caption = "LastMessage";
            this.gridColumnLastMessage.FieldName = "LastMessageDate";
            this.gridColumnLastMessage.Name = "gridColumnLastMessage";
            this.gridColumnLastMessage.Visible = true;
            this.gridColumnLastMessage.VisibleIndex = 7;
            // 
            // gridColumnEthAddress
            // 
            this.gridColumnEthAddress.Caption = "EthAddress";
            this.gridColumnEthAddress.FieldName = "WhitelistItem.EthAddress";
            this.gridColumnEthAddress.Name = "gridColumnEthAddress";
            this.gridColumnEthAddress.Visible = true;
            this.gridColumnEthAddress.VisibleIndex = 9;
            // 
            // gridColumnEthBalance
            // 
            this.gridColumnEthBalance.Caption = "EthBalance";
            this.gridColumnEthBalance.FieldName = "WhitelistItem.EthBalance";
            this.gridColumnEthBalance.Name = "gridColumnEthBalance";
            this.gridColumnEthBalance.Visible = true;
            this.gridColumnEthBalance.VisibleIndex = 12;
            // 
            // Contribution
            // 
            this.Contribution.Caption = "Contribution";
            this.Contribution.FieldName = "WhitelistItem.Contribution";
            this.Contribution.Name = "Contribution";
            this.Contribution.Visible = true;
            this.Contribution.VisibleIndex = 10;
            // 
            // gridColumnTwitter
            // 
            this.gridColumnTwitter.Caption = "Twitter";
            this.gridColumnTwitter.FieldName = "GiveawayItem.Twitter";
            this.gridColumnTwitter.Name = "gridColumnTwitter";
            this.gridColumnTwitter.Visible = true;
            this.gridColumnTwitter.VisibleIndex = 13;
            // 
            // gridColumnDiscord
            // 
            this.gridColumnDiscord.Caption = "Discord";
            this.gridColumnDiscord.FieldName = "GiveawayItem.Discord";
            this.gridColumnDiscord.Name = "gridColumnDiscord";
            this.gridColumnDiscord.Visible = true;
            this.gridColumnDiscord.VisibleIndex = 14;
            // 
            // gridColumnEthAdrGiveaway
            // 
            this.gridColumnEthAdrGiveaway.Caption = "EthAdrGiveaway";
            this.gridColumnEthAdrGiveaway.FieldName = "GiveawayItem.EthAddress";
            this.gridColumnEthAdrGiveaway.Name = "gridColumnEthAdrGiveaway";
            this.gridColumnEthAdrGiveaway.Visible = true;
            this.gridColumnEthAdrGiveaway.VisibleIndex = 15;
            // 
            // gridColumnStateGA
            // 
            this.gridColumnStateGA.Caption = "StateGA";
            this.gridColumnStateGA.FieldName = "GiveawayItem.State";
            this.gridColumnStateGA.Name = "gridColumnStateGA";
            this.gridColumnStateGA.Visible = true;
            this.gridColumnStateGA.VisibleIndex = 11;
            // 
            // gridColumnStatePS
            // 
            this.gridColumnStatePS.Caption = "StatePS";
            this.gridColumnStatePS.FieldName = "WhitelistItem.State";
            this.gridColumnStatePS.Name = "gridColumnStatePS";
            this.gridColumnStatePS.Visible = true;
            this.gridColumnStatePS.VisibleIndex = 8;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(945, 418);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlWhitelist;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(925, 348);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonStart;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonStop;
            this.layoutControlItem3.Location = new System.Drawing.Point(108, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(568, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(256, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(98, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(10, 0);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonExit;
            this.layoutControlItem4.Location = new System.Drawing.Point(824, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(101, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonApprove;
            this.layoutControlItem5.Location = new System.Drawing.Point(208, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(109, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.labelGiveaway;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(925, 24);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // timerWhitelist
            // 
            this.timerWhitelist.Interval = 1000;
            this.timerWhitelist.Tick += new System.EventHandler(this.timerWhitelist_Tick);
            // 
            // simpleButtonResetGiveaway
            // 
            this.simpleButtonResetGiveaway.Location = new System.Drawing.Point(329, 12);
            this.simpleButtonResetGiveaway.Name = "simpleButtonResetGiveaway";
            this.simpleButtonResetGiveaway.Size = new System.Drawing.Size(118, 22);
            this.simpleButtonResetGiveaway.StyleController = this.layoutControl1;
            this.simpleButtonResetGiveaway.TabIndex = 10;
            this.simpleButtonResetGiveaway.Text = "Reset Giveaway";
            this.simpleButtonResetGiveaway.Click += new System.EventHandler(this.simpleButtonResetGiveaway_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.simpleButtonResetGiveaway;
            this.layoutControlItem7.Location = new System.Drawing.Point(317, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(122, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // simpleButtonClearChatlogs
            // 
            this.simpleButtonClearChatlogs.Location = new System.Drawing.Point(451, 12);
            this.simpleButtonClearChatlogs.Name = "simpleButtonClearChatlogs";
            this.simpleButtonClearChatlogs.Size = new System.Drawing.Size(125, 22);
            this.simpleButtonClearChatlogs.StyleController = this.layoutControl1;
            this.simpleButtonClearChatlogs.TabIndex = 11;
            this.simpleButtonClearChatlogs.Text = "Clear Chatlogs";
            this.simpleButtonClearChatlogs.Click += new System.EventHandler(this.simpleButtonClearChatlogs_Click);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.simpleButtonClearChatlogs;
            this.layoutControlItem8.Location = new System.Drawing.Point(439, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(129, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 418);
            this.Controls.Add(this.layoutControl1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Savix Bot";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlWhitelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWhitelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControlWhitelist;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewWhitelist;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.Timer timerWhitelist;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
        private DevExpress.XtraEditors.SimpleButton simpleButtonStop;
        private DevExpress.XtraEditors.SimpleButton simpleButtonStart;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonApprove;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsername;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFirstName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLanguage;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnWorkflow;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFirstMessage;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastMessage;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEthAddress;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEthBalance;
        private DevExpress.XtraGrid.Columns.GridColumn Contribution;
        private System.Windows.Forms.Label labelGiveaway;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTwitter;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDiscord;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEthAdrGiveaway;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStateGA;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatePS;
        private DevExpress.XtraEditors.SimpleButton simpleButtonResetGiveaway;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClearChatlogs;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}

