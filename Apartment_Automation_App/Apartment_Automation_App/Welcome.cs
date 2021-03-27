using System;
using System.IO;
using System.Windows.Forms;

namespace Apartment_Automation_App
{
    public partial class Welcome : Form
    {
        // This string array contains Users Name and Password. 
        private string[] usersNameAndPassword = File.ReadAllLines(@"..\..\userNameAndPassword.txt");

        // This string is contains logged in user.
        public static string currentUserName = null;

        AdminPage adminPage = new AdminPage();
        MemberPage memberPage = new MemberPage();

        public Welcome()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Login Button method.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_Button_Click(object sender, EventArgs e)
        {

            // if logging user is admin
            if (userName_TextBox.Text == "admin" && userPassword_TextBox.Text == "admin")
            {
                currentUserName = "admin";
                adminPage.Show();
                this.Hide();
            }
            else
            {
                // control database.
                for (int i = 0; i < usersNameAndPassword.Length; i++)
                {
                    // if true
                    if (userName_TextBox.Text == usersNameAndPassword[i] && userPassword_TextBox.Text == usersNameAndPassword[i + 1])
                    {
                        currentUserName = usersNameAndPassword[i];

                        memberPage.Show();
                        this.Hide(); // dispose
                        break;
                    }
                }

                // if else 
                if (currentUserName == null)
                {
                    MessageBox.Show("Hatalı giriş yaptınız. \nLütfen tekrar deneyiniz.");
                    userName_TextBox.Clear();
                    userPassword_TextBox.Clear();
                }

            }

        }

    }
}
