using System;
using System.Windows.Forms;

namespace Miner
{
    public partial class Menu : Form
    {
        public Menu()
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
            FormClass.Settings = new Settings();
            FormClass.Settings.Show();
        }

        private void LeadersBtn_Click(object sender, EventArgs e)
        {
            Hide();
            FormClass.Leaders = new Leaders();
            FormClass.Leaders.Show();
        }

        private void GameBtn_Click(object sender, EventArgs e)
        {
            FormClass.Game = new Game();

            Hide();
            FormClass.Game.ShowDialog();
        }
    }
}
