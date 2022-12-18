using System.IO;
using System.Windows.Forms;

namespace Miner
{
    public partial class InputUserNameForm : Form
    {
        private int score = 0;

        private string name;

        public int Score
        {
            set { score = value; }

            get { return score; }
        }

        public string Name
        {
            private set { name = value; }

            get { return name; }
        }

        public InputUserNameForm()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, System.EventArgs e)
        {
            name = inputNameTextBox.Text;

            if (name == "")
            {
                MessageBox.Show("Вы не ввели имя!");
                return;
            }

            WriteLeaderBoardUser();

            FormClass.InputUserName.Hide();
            FormClass.Menu.Show();
        }

        private async void WriteLeaderBoardUser()
        {
            string path = "leaders.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync($"{name};{score}");
            }
        }

        private void InputUserName_FormClosed(object sender, FormClosedEventArgs e) => FormClass.Menu.Show();

        
    }
}
