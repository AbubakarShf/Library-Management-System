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
    public partial class Login : Form
    {
        public static string Admin_ID;
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        Thread th;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBoxloginpassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnloginsignup_Click(object sender, EventArgs e)
        {
        }

        public void opensignupform(object ob)
        {
            Application.Run(new SignUp());
        }

        private void btnloginforgetpassword_Click(object sender, EventArgs e)
        {
        }

        public void openforgetpassform(object ob)
        {
            Application.Run(new ForgetPassword());
        }

        private void insertintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'" + textBoxloginusername.Text + " is Logged in.')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnloging_Click(object sender, EventArgs e)
        {
        }

        private void openloginform()
        {
            Application.Run(new Login());
        }
        public void openloadingform(object ob)
        {
            Application.Run(new Loading());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxloginusername.Text == "")
            {
                MessageBox.Show("Please Enter Username!");
            }
            else if (textBoxloginpassword.Text == "")
            {
                MessageBox.Show("Please Enter Password!");
            }
            else
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * From Admin_Table where Admin_ID='" + textBoxloginusername.Text + "'";
                cmd.ExecuteNonQuery();
                ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
                if (dt.Rows.Count == 1)
                {
                    insertintoactivitylog();
                    Admin_ID = textBoxloginusername.Text;
                    this.Close();
                    th = new Thread(openloadingform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!");
                    this.Close();
                    th = new Thread(openloginform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opensignupform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openforgetpassform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}
