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
    public partial class ReturnBook : Form
    {
        Thread th;
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void insertintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'Student with Registration_No(" + textBoxstuid.Text + ") Returned a Book with ISBN(" + textBoxbookisbn.Text + ").')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void BookData()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From Book_Table where Book_ISBN='" + textBoxbookisbn.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt2);
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxstuid.Text == "")
            {
                MessageBox.Show("plz enter student ID");
            }
            else if (textBoxbookisbn.Text == "")
            {
                MessageBox.Show("plz enter Book ISBN");
            }
            else
            {
                checkissue();
                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show(dt.Rows[0]["Registration_Number"].ToString() + "\n" + dt.Rows[0]["Book_ISBN"].ToString() + "\n" + dt.Rows[0]["Book_Issue_Date"].ToString() + "\n" + dt.Rows[0]["Return_Book_Date"].ToString(),"Issued Detail");
                    btnret.Visible = true;
                }
                else
                {
                    MessageBox.Show("This book is not issued to this Student");
                    this.Close();
                    th = new Thread(openretform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
            }
        }

        private void openretform()
        {
            Application.Run(new ReturnBook());
        }

        private void checkissue()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From IssueBook_Table where Registration_Number='" + textBoxstuid.Text + "' and Book_ISBN='" + textBoxbookisbn.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            con.Close();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openhomeform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void openhomeform()
        {
            Application.Run(new Home());
        }

        private void updatebookcopy()
        {
            BookData();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Book_Table set Copies=" + (int.Parse(dt2.Rows[0]["Copies"].ToString()) + 1) + " where Book_ISBN='"+textBoxbookisbn.Text+"'";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnret_Click(object sender, EventArgs e)
        {
            checkdate();
            DateTime d1 = Convert.ToDateTime(dt3.Rows[0]["Return_Book_Date"].ToString());
            DateTime d2 = DateTime.Now;
            int res = DateTime.Compare(d2, d1);
            if (res<=0)
            {
                MessageBox.Show("You Returned On Time");
            }
            else
            {
                MessageBox.Show("You Returned Late You have to Pay Fine");
                insertintofinetable();
            }
            delfromissuebook();
            updatebookcopy();
            MessageBox.Show("Book Returned Sucessfully");
            insertintoactivitylog();
            this.Close();
            th = new Thread(openretform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void checkdate()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From IssueBook_Table where Registration_Number='" + textBoxstuid.Text + "' and Book_ISBN='" + textBoxbookisbn.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt3);
            con.Close();
        }

        private void delfromissuebook()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete From IssueBook_Table where Registration_Number='" + textBoxstuid.Text + "' and Book_ISBN='" + textBoxbookisbn.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
        }


        private void insertintofinetable()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Fine_Table values('" + dt.Rows[0]["Registration_Number"] + "','" + dt.Rows[0]["Book_ISBN"] + "','" + dt.Rows[0]["Admin_ID"] + "',100,'" + dt.Rows[0]["Book_Issue_Date"] + "','" + dt.Rows[0]["Return_Book_Date"] + "')";
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
