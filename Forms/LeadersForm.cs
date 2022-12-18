using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Miner
{
    public partial class LeadersForm : Form
    {
        private List<string> Users = new List<string>();

        private List<Label> Labels = new List<Label>();


        public LeadersForm()
        {
            InitializeComponent();

            ReadLeadersFile();
            addLabels();

            FillGrid();
        }

        private void ReadLeadersFile()
        {
            try
            {

                StreamReader f = new StreamReader("leaders.txt");
                while (!f.EndOfStream)
                {
                    string s = f.ReadLine();
                    Users.Add(s);
                }
                f.Close();

                Users.Sort(delegate (string x, string y)
                {
                    return int.Parse(y.Split(';')[1]).CompareTo(int.Parse(x.Split(';')[1]));
                });
            }

            catch (Exception)
            {
                MessageBox.Show("Файл пуст");
                return;
            }
        }

        private void addLabels()
        {
            Labels.Add(label10);
            Labels.Add(label11);
            Labels.Add(label12);
            Labels.Add(label13);
            Labels.Add(label14);
            Labels.Add(label15);
            Labels.Add(label16);
            Labels.Add(label17);
            Labels.Add(label18);
            Labels.Add(label19);
        }

        private async void FillGrid()
        {
            List<string> data = new List<string>();

            foreach (var item in Users)
                data.AddRange(item.Split(';'));


            for (int i = 0; i < data.Count; i++)
            {
                try
                {
                    Labels[i].Text = data[i];
                }
                catch (Exception)
                {
                    return;
                }

            }

        }

        private void buttonCloseLeaderBoard_Click(object sender, EventArgs e) => FormClass.Leaders.Close();

        private void Leaders_FormClosed(object sender, FormClosedEventArgs e) => FormClass.Menu.Show();
    }
}
