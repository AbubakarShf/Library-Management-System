using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Library_Management_Syatem
{
    public partial class SignUp : Form
    {
        SqlCommand cmd;
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        Thread th;
        private string id;
        private string name;
        private string password;
        private string address;
        private string phoneno;
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnsignupback_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openloginform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void openloginform(object ob)
        {
            Application.Run(new Login());
        }

        private void insertintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'" + textBoxsignupusername.Text + " is Singed Up.')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnsignup_Click(object sender, EventArgs e)
        {
            if (textBoxsignupusername.Text == "")
            {
                MessageBox.Show("Please Enter Username!");
            }
            else if (textBoxsignupusername.Text.Length < 6 || textBoxsignupusername.Text.Length > 50)
            {
                MessageBox.Show("Username length must be between 6-50");
            }
            else if (textBoxsignupname.Text == "")
            {
                MessageBox.Show("Please Enter Name!");
            }
            else if (textBoxsignupname.Text.Length > 50)
            {
                MessageBox.Show("Name length must be between 1-50");
            }
            else if (textBoxsignuppassword.Text == "")
            {
                MessageBox.Show("Please Enter Password!");
            }
            else if (textBoxsignuppassword.Text.Length > 20 || textBoxsignuppassword.Text.Length < 6)
            {
                MessageBox.Show("Password length must be between 6-20");
            }
            else if (textBoxsignupphoneno.Text == "")
            {
                MessageBox.Show("Please Enter Phone Number!");
            }
            else if (textBoxsignupphoneno.Text.Length != 11)
            {
                MessageBox.Show("Phone Number must be of 11 didgit");
            }
            else if (textBoxsignupaddress.Text.Length > 50)
            {
                MessageBox.Show("Address length must be <=50");
            }
            else
            {

                id = textBoxsignupusername.Text;
                CheckUsername(id);
                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("Username already Registered!");
                }
                else
                {
                    name = textBoxsignupname.Text;
                    password = textBoxsignuppassword.Text;
                    address = textBoxsignupaddress.Text;
                    phoneno = textBoxsignupphoneno.Text;
                    DialogResult dr = MessageBox.Show("Password = " + textBoxsignuppassword.Text + "\nPlease Confirm Your Password.", "Password Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into Admin_Table values('" + id + "','" + name + "','" + password + "','" + address + "','" + phoneno + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        insertintoactivitylog();
                        MessageBox.Show("Admin Registered Successfully.");
                    }
                }
            }
        }

        private void CheckUsername(string id)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From Admin_Table where Admin_ID='"+id+"'";
            cmd.ExecuteNonQuery();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            con.Close();
        }
    }
}
