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
    public partial class ForgetPassword : Form
    {
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dtuser = new DataTable();
        public static string username,name,password,address,phoneno;
        Thread th;
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void btnforgetpasswordback_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openloginform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void openloginform()
        {
            Application.Run(new Login());
        }

        private void btnforgetsearch_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText= "select * from Admin_Table where Admin_ID='" + textBoxforgetusername.Text + "'";
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dtuser);
            if (dtuser.Rows.Count == 1)
            {
                username = dtuser.Rows[0]["Admin_ID"].ToString();
                name = dtuser.Rows[0]["Admin_Name"].ToString();
                password = dtuser.Rows[0]["Admin_Password"].ToString();
                address = dtuser.Rows[0]["Admin_Address"].ToString();
                phoneno = dtuser.Rows[0]["Phone_Number"].ToString();
                textBoxforgetname.Visible = true;
                textBoxforgetpassword.Visible = true;
                textBoxforgetaddress.Visible = true;
                textBoxforgetphoneno.Visible = true;
                labelforgetaddress.Visible = true;
                labelforgetname.Visible = true;
                labelforgetpassword.Visible = true;
                labelforgetphoneno.Visible = true;
                textBoxforgetusername.Text = username;
                textBoxforgetname.Text = name;
                textBoxforgetpassword.Text = password;
                textBoxforgetaddress.Text = address;
                textBoxforgetphoneno.Text = phoneno;
                MessageBox.Show("Password: " + password);
            }
            else 
            {
                MessageBox.Show("Invalid Username!");
            }
            con.Close();
        }
    }
}
