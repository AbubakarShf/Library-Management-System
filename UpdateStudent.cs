using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Library_Management_Syatem
{
    public partial class UpdateStudent : Form
    {
        Thread th;
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        public UpdateStudent()
        {
            InitializeComponent();
        }

        private void CheckStu()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Student_Table where Registration_Number='" + textBoxstuid.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckStu();
            if (dt.Rows.Count == 1)
            {
                DialogResult dr = MessageBox.Show("Registration NO:\t" + dt.Rows[0]["Registration_Number"].ToString() + "\nFirst Name:\t" + dt.Rows[0]["First_Name"].ToString() + "\nLast Name:\t" + dt.Rows[0]["Second_Name"].ToString() + "\nPhone No:\t" + dt.Rows[0]["Phone_Number"].ToString() + "\nAddress:\t\t" + dt.Rows[0]["Student_Address"].ToString() + "\nDepartment:\t" + dt.Rows[0]["Department"].ToString() + "\nCNIC:\t\t" + dt.Rows[0]["CNIC"].ToString() + "\n\nDou You Want to Update?", "Update a Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    labelstuiddata.Text = dt.Rows[0]["Registration_Number"].ToString();
                    button1.Visible = false;
                    textBoxstuid.Visible = false;
                    labelfname.Visible = true;
                    labellname.Visible = true;
                    labelphone.Visible = true;
                    labeladd.Visible = true;
                    labeldept.Visible = true;
                    textBoxfname.Visible = true;
                    textBoxfname.Text = dt.Rows[0]["First_Name"].ToString();
                    textBoxlname.Visible = true;
                    textBoxlname.Text = dt.Rows[0]["Second_Name"].ToString();
                    textBoxphone.Visible = true;
                    textBoxphone.Text = dt.Rows[0]["Phone_Number"].ToString();
                    textBoxadd.Visible = true;
                    textBoxadd.Text = dt.Rows[0]["Student_Address"].ToString();
                    textBoxdept.Visible = true;
                    textBoxdept.Text = dt.Rows[0]["Department"].ToString();
                    btnupdate.Visible = true;
                }
                else
                {
                    MessageBox.Show("No Student exists with this id");
                    this.Close();
                    th = new Thread(openupdateform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
            }
        }

        private void openupdateform()
        {
            Application.Run(new UpdateStudent());
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (textBoxfname.Text == "")
            {
                MessageBox.Show("Please Enetr First Name!");
            }
            else if (textBoxphone.Text == "")
            {
                MessageBox.Show("Please Enter Phone Number!");
            }
            else if (textBoxfname.Text.Length > 15)
            {
                MessageBox.Show("First Name Length must be <=15");
            }
            else if (textBoxlname.Text.Length > 15)
            {
                MessageBox.Show("Last Name Length must be <=15");
            }
            else if (textBoxphone.Text.Length != 11)
            {
                MessageBox.Show("Phone No must Consist of 11 Digit");
            }
            else if (textBoxadd.Text.Length > 40)
            {
                MessageBox.Show("Address Length must be <=40");
            }
            else
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Student_Table set First_Name='" + textBoxfname.Text + "',Second_Name='" + textBoxlname.Text + "',Phone_Number='" + textBoxphone.Text + "',Student_Address='" + textBoxadd.Text + "',Department='" + textBoxdept.Text + "' where Registration_Number='" + textBoxstuid.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                insertintoactivitylog();
                MessageBox.Show("Student Updated Successfully");
                this.Close();
                th = new Thread(openupdateform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        private void insertintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'Student with ID(" + textBoxstuid.Text + ") is Updated.')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openhomeform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }


        private void openhomeform()
        {
            Application.Run(new Home());
        }
    }
}
