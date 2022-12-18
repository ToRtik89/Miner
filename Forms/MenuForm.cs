using System;
using System.Windows.Forms;

namespace Miner
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Hide();
            FormClass.Settings = new SettingsForm();
            FormClass.Settings.Show();
        }

        private void LeadersBtn_Click(object sender, EventArgs e)
        {
            Hide();
            FormClass.Leaders = new LeadersForm();
            FormClass.Leaders.Show();
        }

        private void GameBtn_Click(object sender, EventArgs e)
        {
            FormClass.Game = new GameForm();

            Hide();
            FormClass.Game.ShowDialog();
        }
    }
}
