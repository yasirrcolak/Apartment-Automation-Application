using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Apartment_Automation_App
{
    public partial class MemberPage : Form
    {
        // This string array contains Users Details.
        private string[] userDetails = File.ReadAllLines(@"..\..\userDetails.txt");

        // This string contains user details database path.
        string userDetailsTxtPath = @"..\..\userDetails.txt";

        public MemberPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Member Page Loaded method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemberPage_Load(object sender, EventArgs e)
        {
            // Controlling database.
            for (int i = 0; i < userDetails.Length; i = i + 7)
            {
                // Print to screen if true.
                if (userDetails[i] == Welcome.currentUserName)
                {
                    userName_TextBox.Text = userDetails[i];
                    registrationDate_TextBox.Text = userDetails[i + 1];
                    rent_TextBox1.Text = userDetails[i + 2];
                    dues_TextBox2.Text = userDetails[i + 3];
                    isDuesPaid_textBox3.Text = userDetails[i + 4];
                    debt_TextBox4.Text = userDetails[i + 5];
                    loginPassword_textBox1.Text = userDetails[i + 6];
                }
            }

            // if the dues have not been paid.
            if (isDuesPaid_textBox3.Text == "hayır")
            {
                notifyIcon1.Icon = SystemIcons.Warning;
                notifyIcon1.BalloonTipTitle = "Uyarı!";
                notifyIcon1.BalloonTipText = "Ödenmemiş aidat borcunuz var!";
                notifyIcon1.ShowBalloonTip(500);
            }

            // if dues have been paid.
            if (isDuesPaid_textBox3.Text == "evet")
            {
                payDues_Button1.Enabled = false;
            }

        }

        /// <summary>
        /// Quit Button method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quit_Button_Click(object sender, EventArgs e)
        {
            Welcome welcomePage = new Welcome();
            welcomePage.Show();
            this.Dispose();
        }

        /// <summary>
        /// Pay dues button method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void payDues_Button1_Click(object sender, EventArgs e)
        {

            isDuesPaid_textBox3.Text = "evet";


            // delete all user all details.
            File.Delete(userDetailsTxtPath);
            FileStream filecreate = new FileStream(userDetailsTxtPath, FileMode.OpenOrCreate, FileAccess.Write);
            filecreate.Close();

            // write all users except the current user to the database.
            for (int i = 0; i < userDetails.Length; i++)
            {

                if ((userDetails[i] == userName_TextBox.Text))
                {
                    i = i + 7;
                }

                if (i < userDetails.Length)
                {
                    File.AppendAllText(userDetailsTxtPath, userDetails[i] + "\n", Encoding.UTF8);
                }
            }

            // write current user to the database.
            File.AppendAllText(userDetailsTxtPath, userName_TextBox.Text + "\n", Encoding.UTF8);
            File.AppendAllText(userDetailsTxtPath, registrationDate_TextBox.Text + "\n", Encoding.UTF8);
            File.AppendAllText(userDetailsTxtPath, rent_TextBox1.Text + "\n", Encoding.UTF8);
            File.AppendAllText(userDetailsTxtPath, dues_TextBox2.Text + "\n", Encoding.UTF8);
            File.AppendAllText(userDetailsTxtPath, isDuesPaid_textBox3.Text + "\n", Encoding.UTF8);
            File.AppendAllText(userDetailsTxtPath, debt_TextBox4.Text + "\n", Encoding.UTF8);
            File.AppendAllText(userDetailsTxtPath, loginPassword_textBox1.Text, Encoding.UTF8);

            MessageBox.Show("Ödeme Başarılı.");

            payDues_Button1.Enabled = false;

        }
    }
}
