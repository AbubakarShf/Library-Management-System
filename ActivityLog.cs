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
    public partial class ActivityLog : Form
    {
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        Thread th;
        public ActivityLog()
        {
            InitializeComponent();
        }

        private void ActivityLog_Load(object sender, EventArgs e)
        {
            getdata();
        }

        private void getdata()
        {
            dt.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from ActivityLog_Table";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void ActivityLog_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void openhomeform()
        {
            Application.Run(new Home());
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape || e.KeyChar == (char)Keys.Back)
            {
                this.Close();
                th = new Thread(openhomeform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openhomeform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void btnall_Click(object sender, EventArgs e)
        {
            getdata();
        }

        private void btnissued_Click(object sender, EventArgs e)
        {
            getissueddata();
        }

        private void getissueddata()
        {
            dt.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from ActivityLog_Table where Activity_Performed like '%issued%'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void btnret_Click(object sender, EventArgs e)
        {
            getreturnddata();
        }

        private void getreturnddata()
        {
            dt.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from ActivityLog_Table where Activity_Performed like '%Returned%'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            getselectedddata();
        }

        private void getselectedddata()
        {
            dt.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from ActivityLog_Table where Activity_Date >= '" + datepicerstart.Value + "' and Activity_Date <= '" + datepicerend.Value + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No Activity is Performed Between " + datepicerstart.Value + " and " + datepicerend.Value + "");
            }
            else
            {
                dataGridView1.DataSource = dt;
            }
            con.Close();
        }
    }
}
