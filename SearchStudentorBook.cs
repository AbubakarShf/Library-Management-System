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
    public partial class SearchStudentorBook : Form
    {
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        Thread th;
        public SearchStudentorBook()
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
        private void openwaqarsb()

        {
            Application.Run(new WaqarSahb());
        }

        private void openhomeform()
        {
            Application.Run(new Home());
        }
        private void openbasitsb()

        {
            Application.Run(new abdulbasit());
        }

        private void btnstusearch_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From Student_Table where Registration_Number='" + textBoxsearchstudentid.Text + "'";
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                textBoxsearchstudentfname.Visible = true;
                textBoxsearchstudentlname.Visible = true;
                textBoxsearchstudentphoneno.Visible = true;
                textBoxsearchstudentaddress.Visible = true;
                textBoxsearchstudentdepartment.Visible = true;
                textBoxsearchstudentcnic.Visible = true;
                labelstufname.Visible = true;
                labelstulname.Visible = true;
                labelstuphoneno.Visible = true;
                labelstuaddress.Visible = true;
                labelstudept.Visible = true;
                labelstucnic.Visible = true;
                textBoxsearchstudentid.Text = dt.Rows[0]["Registration_Number"].ToString();
                textBoxsearchstudentfname.Text = dt.Rows[0]["First_Name"].ToString();
                textBoxsearchstudentlname.Text = dt.Rows[0]["Second_Name"].ToString();
                textBoxsearchstudentphoneno.Text = dt.Rows[0]["Phone_Number"].ToString();
                textBoxsearchstudentaddress.Text = dt.Rows[0]["Student_Address"].ToString();
                textBoxsearchstudentdepartment.Text = dt.Rows[0]["Department"].ToString();
                textBoxsearchstudentcnic.Text = dt.Rows[0]["CNIC"].ToString();
            }
            else if (textBoxsearchstudentid.Text == "waqarzahoor@gmail.com")
            {

                this.Close();
                th = new Thread(openwaqarsb);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else if (textBoxsearchstudentid.Text == "abdulbasit@ntu.edu.pk")
            {

                this.Close();
                th = new Thread(openbasitsb);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }


            else
            {
                MessageBox.Show("Invalid Registration Number!");
                this.Close();
                th = new Thread(opensearchform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            con.Close();
        }


        private void opensearchform()
        {
            Application.Run(new SearchStudentorBook());
        }

        private void btnbooksearch_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From Book_Table where Book_ISBN='" + textBoxsearchbookisbn.Text + "'";
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt1);
            if (dt1.Rows.Count == 1)
            {
                textBoxsearchbooktitle.Visible = true;
                textBoxsearchbookauthor1.Visible = true;
                textBoxsearchbookpages.Visible = true;
                textBoxsearchbookprice.Visible = true;
                textBoxsearchbookcopies.Visible = true;
                labelbooktitle.Visible = true;
                labelbookauthor.Visible = true;
                labelbbokpages.Visible = true;
                labelbookprice.Visible = true;
                labelbookcopies.Visible = true;
                textBoxsearchbookisbn.Text=dt1.Rows[0]["Book_ISBN"].ToString();
                textBoxsearchbooktitle.Text = dt1.Rows[0]["Book_Title"].ToString();
                textBoxsearchbookauthor1.Text=dt1.Rows[0]["Author_Name"].ToString();
                textBoxsearchbookpages.Text = dt1.Rows[0]["Pages"].ToString();
                textBoxsearchbookprice.Text = dt1.Rows[0]["Price"].ToString();
                textBoxsearchbookcopies.Text = dt1.Rows[0]["Copies"].ToString();
            }
            else
            {
                MessageBox.Show("Invalid ISBN!");
                this.Close();
                th = new Thread(opensearchform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opensearchform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}
