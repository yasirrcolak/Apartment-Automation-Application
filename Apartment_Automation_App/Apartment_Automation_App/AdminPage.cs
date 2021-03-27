using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Apartment_Automation_App
{
    public partial class AdminPage : Form
    {
        // This string contains user details database path.
        string userDetailsTxtPath = @"..\..\userDetails.txt";

        // This string contains user information database path.
        string userNameAndPasswordTxtPath = @"..\..\userNameAndPassword.txt";

        // This string array contains Users Details.
        private string[] userDetails = File.ReadAllLines(@"..\..\userDetails.txt");

        // This string array contains Users login information.        
        private string[] userNameAndPassword = File.ReadAllLines(@"..\..\userNameAndPassword.txt");

        public AdminPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Admin page loaded method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminPage_Load(object sender, EventArgs e)
        {
            // Add all usernames to userNames listbox.   
            for (int i = 0; i < userDetails.Length; i = i + 7)
            {
                userNames_ListBox1.Items.Add(userDetails[i]);
            }
        }

        /// <summary>
        /// Selected(double click) user method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            // user details string array is reset.
            userDetails = File.ReadAllLines(userDetailsTxtPath);

            // if selected item is not null or empty.
            if (!string.IsNullOrEmpty(userNames_ListBox1.SelectedItem.ToString()))
            {
                // contol database.
                for (int i = 0; i < userDetails.Length; i = i + 7)
                {
                    // Print to screen if true.
                    if (userDetails[i] == userNames_ListBox1.SelectedItem.ToString())
                    {
                        userName_TextBox.Text = userDetails[i];
                        registrationDate_TextBox.Text = userDetails[i + 1];
                        rent_TextBox1.Text = userDetails[i + 2];
                        dues_TextBox2.Text = userDetails[i + 3];
                        isDuesPaid_textBox3.Text = userDetails[i + 4];
                        debt_TextBox4.Text = userDetails[i + 5];
                        loginPassword_TextBox1.Text = userDetails[i + 6];

                    }
                }
            }

        }

        /// <summary>
        /// Clear Button(Clear all textbox) method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_Button2_Click(object sender, EventArgs e)
        {
            userName_TextBox.Clear();
            registrationDate_TextBox.Clear();
            rent_TextBox1.Clear();
            dues_TextBox2.Clear();
            isDuesPaid_textBox3.Clear();
            debt_TextBox4.Clear();
            loginPassword_TextBox1.Clear();
        }

        /// <summary>
        /// Add or Update button method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_Button1_Click(object sender, EventArgs e)
        {

            // control all textboxs.
            if (string.IsNullOrEmpty(userName_TextBox.Text))
            {
                MessageBox.Show("Kullanıcı adı boş olamaz!");
            }
            else if (string.IsNullOrEmpty(registrationDate_TextBox.Text))
            {
                MessageBox.Show("Kayıt tarihi boş olamaz!");
            }
            else if (string.IsNullOrEmpty(rent_TextBox1.Text))
            {
                MessageBox.Show("Kira bedeli boş olamaz!\nKiracı değilse 'kiracı değil' yazabilirsiniz.");
            }
            else if (string.IsNullOrEmpty(dues_TextBox2.Text))
            {
                MessageBox.Show("Aidat miktarı boş olamaz!");
            }
            else if (string.IsNullOrEmpty(isDuesPaid_textBox3.Text))
            {
                MessageBox.Show("Aidat ödendi mi? boş olamaz!\nAidat ödendi mi bilgisi 'evet' ya da 'hayır' olabilir.");
            }
            else if (!(isDuesPaid_textBox3.Text == "evet" || isDuesPaid_textBox3.Text == "hayır"))
            {
                MessageBox.Show("Aidat ödendi mi bilgisi 'evet' ya da 'hayır' olabilir.");
            }
            else if (string.IsNullOrEmpty(debt_TextBox4.Text))
            {
                MessageBox.Show("Biriken borç boş olamaz!\nBorç yoksa '0' yazabilirsiniz.");
            }
            else if (string.IsNullOrEmpty(loginPassword_TextBox1.Text))
            {
                MessageBox.Show("Giriş Şifresi boş olamaz.");
            }
            else
            {
                // user details string array is reset.
                userDetails = File.ReadAllLines(userDetailsTxtPath);
                // user login information string array is reset.
                userNameAndPassword = File.ReadAllLines(userNameAndPasswordTxtPath);

                // Check if there are users in the database
                for (int i = 0; i < userDetails.Length; i = i + 7)
                {
                    // user details database reset
                    File.Delete(userDetailsTxtPath);
                    FileStream filecreate = new FileStream(userDetailsTxtPath, FileMode.OpenOrCreate, FileAccess.Write);
                    filecreate.Close();

                    // user login information database reset.
                    File.Delete(userNameAndPasswordTxtPath);
                    FileStream filecreate2 = new FileStream(userNameAndPasswordTxtPath, FileMode.OpenOrCreate, FileAccess.Write);
                    filecreate2.Close();

                    // delete if any.
                    for (int j = 0; j < userDetails.Length; j++)
                    {

                        if ((userDetails[j] == userName_TextBox.Text) && (j + 7 <= userDetails.Length))
                        {
                            j = j + 7;
                        }

                        if (j < userDetails.Length)
                        {
                            File.AppendAllText(userDetailsTxtPath, userDetails[j] + "\n", Encoding.UTF8);
                        }

                    }

                    for (int k = 0; k < userNameAndPassword.Length; k++)
                    {
                        if ((userNameAndPassword[k] == userName_TextBox.Text) && (k + 2 <= userNameAndPassword.Length))
                        {
                            k = k + 2;
                        }

                        if (k < userNameAndPassword.Length)
                        {
                            File.AppendAllText(userNameAndPasswordTxtPath, userNameAndPassword[k] + "\n", Encoding.UTF8);
                        }

                    }

                    // add to database.
                    File.AppendAllText(userDetailsTxtPath, userName_TextBox.Text + "\n", Encoding.UTF8);
                    File.AppendAllText(userDetailsTxtPath, registrationDate_TextBox.Text + "\n", Encoding.UTF8);
                    File.AppendAllText(userDetailsTxtPath, rent_TextBox1.Text + "\n", Encoding.UTF8);
                    File.AppendAllText(userDetailsTxtPath, dues_TextBox2.Text + "\n", Encoding.UTF8);
                    File.AppendAllText(userDetailsTxtPath, isDuesPaid_textBox3.Text + "\n", Encoding.UTF8);
                    File.AppendAllText(userDetailsTxtPath, debt_TextBox4.Text + "\n", Encoding.UTF8);
                    File.AppendAllText(userDetailsTxtPath, loginPassword_TextBox1.Text, Encoding.UTF8);

                    File.AppendAllText(userNameAndPasswordTxtPath, userName_TextBox.Text + "\n", Encoding.UTF8);
                    File.AppendAllText(userNameAndPasswordTxtPath, loginPassword_TextBox1.Text, Encoding.UTF8);

                }

                MessageBox.Show("Güncellendi.");

            }

            // listbox clear.
            userNames_ListBox1.Items.Clear();
            userDetails = File.ReadAllLines(userDetailsTxtPath);
            for (int i = 0; i < userDetails.Length; i = i + 7)
            {
                userNames_ListBox1.Items.Add(userDetails[i]);
            }


        }

        /// <summary>
        /// Quit button method.
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
        /// Delete button method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_Button1_Click(object sender, EventArgs e)
        {
            // user details string array is reset.
            userDetails = File.ReadAllLines(userDetailsTxtPath);
            // user login information string array is reset.
            userNameAndPassword = File.ReadAllLines(userNameAndPasswordTxtPath);

            // user details database reset
            File.Delete(userDetailsTxtPath);
            FileStream filecreate = new FileStream(userDetailsTxtPath, FileMode.OpenOrCreate, FileAccess.Write);
            filecreate.Close();

            // user login information database reset.
            File.Delete(userNameAndPasswordTxtPath);
            FileStream filecreate2 = new FileStream(userDetailsTxtPath, FileMode.OpenOrCreate, FileAccess.Write);
            filecreate2.Close();

            // delete selected user in user details.
            for (int i = 0; i < userDetails.Length; i++)
            {

                if ((userDetails[i] == userName_TextBox.Text) /*&& (i + 7 < userDetails.Length) */)
                {
                    i = i + 7;
                }

                if (i < userDetails.Length)
                {
                    File.AppendAllText(userDetailsTxtPath, userDetails[i] + "\n", Encoding.UTF8);
                }

            }

            // delete selected user in user login information.
            for (int i = 0; i < userNameAndPassword.Length; i++)
            {
                if ((userNameAndPassword[i] == userName_TextBox.Text)/* && (i + 2 < userNameAndPassword.Length) */ )
                {
                    i = i + 2;
                }

                if (i < userNameAndPassword.Length)
                {
                    File.AppendAllText(userNameAndPasswordTxtPath, userNameAndPassword[i] + "\n", Encoding.UTF8);
                }

            }

            // listbox clear.
            userNames_ListBox1.Items.Clear();
            userDetails = File.ReadAllLines(userDetailsTxtPath);
            for (int i = 0; i < userDetails.Length; i = i + 7)
            {
                userNames_ListBox1.Items.Add(userDetails[i]);
            }

        }

    }
}
