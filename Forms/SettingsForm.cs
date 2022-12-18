using System.IO;
using System.Windows.Forms;

namespace Miner
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e) => FormClass.Menu.Show();

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (difficultComboBox.SelectedIndex == -1) return;

            SetupSettings(difficultComboBox.SelectedIndex);

            Hide();
            FormClass.Menu.Show();
        }

        private async void SetupSettings(int index)
        {
            string settingsStroke = "";

            if (index == 0)
            {
                settingsStroke = "easy";
            }

            if (index == 1)
            {
                settingsStroke = "normal";
            }

            if (index == 2)
            {
                settingsStroke = "hard";
            }

            using (StreamWriter writer = new StreamWriter("settings.txt", false))
            {
                await writer.WriteLineAsync(settingsStroke);
            }
        }
    }
}
