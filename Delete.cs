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
    public partial class Delete : Form
    {
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        Thread th;
        public Delete()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opendelform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void opendelform()
        {
            Application.Run(new Delete());
        }

        private void checkstudata()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Student_Table where Registration_Number='" + textBoxstuid.Text + "'";
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            con.Close();
        }

        private void checkissuebook()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from IssueBook_Table where Registration_Number='" + textBoxstuid.Text + "'";
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt1);
            con.Close();
        }

        private void deletestudata()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from Student_Table where Registration_Number='" + textBoxstuid.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxstuid.Text == "")
            {
                MessageBox.Show("Please Enter Registration Number!");
            }
            else
            {
                checkstudata();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Student Exists with this Registration No!");
                }
                else
                {
                    DialogResult dr=MessageBox.Show("Registration No:\t" + dt.Rows[0]["Registration_Number"].ToString() + "\nFirst Name:\t" + dt.Rows[0]["First_Name"].ToString() + "\nLast Name:\t" + dt.Rows[0]["Second_Name"].ToString() + "\nPhone No:\t" + dt.Rows[0]["Phone_Number"].ToString() + "\nAddress:\t\t" + dt.Rows[0]["Student_Address"].ToString() + "\nDepartment:\t" + dt.Rows[0]["Department"].ToString() + "\nCNIC:\t\t" + dt.Rows[0]["CNIC"].ToString() + "\n\nDo You Want to Delete this Student?", "Delete a Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        checkissuebook();
                        if (dt1.Rows.Count != 0)
                        {
                            DialogResult dr1=MessageBox.Show("Some books are Issued to this Student.\nDo you want to return it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr1 == DialogResult.Yes)
                            {
                                deleted();
                            }
                            else
                            {
                                this.Close();
                                th = new Thread(opendelform);
                                th.SetApartmentState(ApartmentState.STA);
                                th.Start();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Book Issued to this Student.");
                            deleted();
                        }
                    }
                    else
                    {
                        this.Close();
                        th = new Thread(opendelform);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();
                    }
                }
            }
        }

        private void deleted()
        {
            deletestudata();
            MessageBox.Show("Student Deleted Successfully!");
            insertintoactivitylog();
            this.Close();
            th = new Thread(opendelform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void insertintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'Student with Registration Number(" + textBoxstuid.Text + ") is Deleted.')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
