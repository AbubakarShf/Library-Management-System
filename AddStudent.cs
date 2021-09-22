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
    public partial class AddStudent : Form
    {
        public static string regnum, fname, sname, phoneno, address, cnic;
        public static int deptid;
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        Thread th;
        public AddStudent()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openhomeform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            fillcombobox();
        }

        private void fillcombobox()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Department_Table";
            SqlDataAdapter ad1 = new SqlDataAdapter(cmd);
            DataTable dtuser = new DataTable();
            ad1.Fill(dtuser);
            int i = dtuser.Rows.Count;
            for (int j = 0; j < i; j++)
            {
                comboBox1.Items.Add(dtuser.Rows[j]["Department_Name"].ToString());
            }
            con.Close();
        }

        private void openhomeform()
        {
            Application.Run(new Home());
        }

        private void Check()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Student_Table where Registration_Number='" + textBoxaddstudentid.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            con.Close();
        }

        private void insertintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'New Student with ID(" + textBoxaddstudentid.Text + ") is Added.')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxaddstudentid.Text == "")
            {
                MessageBox.Show("Please Enter Registartion Number!");
            }
            else if (textBoxaddstudentfname.Text == "")
            {
                MessageBox.Show("Please Enetr First Name!");
            }
            else if (textBoxaddstudentphoneno.Text == "")
            {
                MessageBox.Show("Please Enter Phone Number!");
            }
            else if (textBoxaddstudentcnic.Text == "")
            {
                MessageBox.Show("Please Enter CNIC!");
            }
            else if (textBoxaddstudentid.Text.Length > 15)
            {
                MessageBox.Show("Registration Number Length must be <=15");
            }
            else if (textBoxaddstudentfname.Text.Length > 15)
            {
                MessageBox.Show("First Name Length must be <=15");
            }
            else if (textBoxaddstudentlname.Text.Length > 15)
            {
                MessageBox.Show("Last Name Length must be <=15");
            }
            else if (textBoxaddstudentphoneno.Text.Length != 11)
            {
                MessageBox.Show("Phone No must Consist of 11 Digit");
            }
            else if (textBoxaddstudentaddress.Text.Length > 40)
            {
                MessageBox.Show("Address Length must be <=40");
            }
            else if (textBoxaddstudentcnic.Text.Length != 15)
            {
                MessageBox.Show("CNIC Length must be = 15");
            }
            else
            {
                Check();
                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("Registration is already allocated");
                    this.Close();
                    th = new Thread(openstuform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    regnum = textBoxaddstudentid.Text;
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Student_Table values('" + textBoxaddstudentid.Text + "','" + textBoxaddstudentfname.Text + "','" + textBoxaddstudentlname.Text + "','" + textBoxaddstudentphoneno.Text + "','" + textBoxaddstudentaddress.Text + "','" + comboBox1.Text + "','" + textBoxaddstudentcnic.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    insertintoactivitylog();
                    MessageBox.Show("Student Added Successfully");
                    this.Close();
                    th = new Thread(openbarcodeform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
            }
        }

        private void openbarcodeform()
        {
            Application.Run(new Barcode());
        }

        private void openstuform()
        {
            Application.Run(new AddStudent());
        }
    }
}
