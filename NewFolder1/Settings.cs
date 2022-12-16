using System.Windows.Forms;

namespace Miner
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e) => FormClass.Menu.Show();
    }
}
