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
    public partial class Book_Stu : Form
    {
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        Thread th;
        public Book_Stu()
        {
            InitializeComponent();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void btnstusearch_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter Student ID");
            }
            else
            {
                dt.Clear();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from IssueBook_Table where Registration_Number='" + textBox1.Text + "'";
                ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Book is Issued to this Student.");
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void btnbooksearch_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please Enter Student ID");
            }
            else
            {
                dt.Clear();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from IssueBook_Table where Book_ISBN='" + textBox2.Text + "'";
                ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("This Book is not Issued to any Student");
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }
    }
}
