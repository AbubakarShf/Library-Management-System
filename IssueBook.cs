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
    public partial class IssueBook : Form
    {
        Thread th;
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        public IssueBook()
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

        private void openhomeform()
        {
            Application.Run(new Home());
        }

        private void btnissuestudentsearch_Click(object sender, EventArgs e)
        {
            CheckStu();
            if (dt.Rows.Count == 1)
            {
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                textBoxissuestudentfname.Visible = true;
                textBoxissuestudentlname.Visible = true;
                textBoxissuestudentphoneno.Visible = true;
                textBoxissuestudentaddress.Visible = true;
                textBoxissuestudentdepartment.Visible = true;
                textBoxissuestudentcnic.Visible = true;
                textBoxissuestudentfname.Text = dt.Rows[0]["First_Name"].ToString();
                textBoxissuestudentlname.Text = dt.Rows[0]["Second_Name"].ToString();
                textBoxissuestudentphoneno.Text = dt.Rows[0]["Phone_Number"].ToString();
                textBoxissuestudentaddress.Text = dt.Rows[0]["Student_Address"].ToString();
                textBoxissuestudentdepartment.Text = dt.Rows[0]["Department"].ToString();
                textBoxissuestudentcnic.Text = dt.Rows[0]["CNIC"].ToString();
            }
            else
            {
                MessageBox.Show("No Student Exists with this ID");
                this.Close();
                th = new Thread(openissueform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        private void CheckStu()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Student_Table where Registration_Number='" + textBoxissuestudentid.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            con.Close();
        }

        private void CheckBook()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Book_Table where Book_ISBN='" + textBoxissuebookisbn.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt1);
            con.Close();
        }

        private void btnissuestudentbook_Click(object sender, EventArgs e)
        {
            CheckBook();
            if (dt1.Rows.Count == 1)
            {
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                labelretdate.Visible = true;
                textBoxretdate.Visible = true;
                textBoxissuebooktitle.Visible = true;
                textBoxissuebookauthor1.Visible = true;
                textBoxissuebookpages.Visible = true;
                textBoxissuebookprice.Visible = true;
                textBoxissuebookcopies.Visible = true;
                textBoxissuebooktitle.Text = dt1.Rows[0]["Book_Title"].ToString();
                textBoxissuebookauthor1.Text = dt1.Rows[0]["Author_Name"].ToString();
                textBoxissuebookpages.Text = dt1.Rows[0]["Pages"].ToString();
                textBoxissuebookprice.Text = dt1.Rows[0]["Price"].ToString();
                textBoxissuebookcopies.Text = dt1.Rows[0]["Copies"].ToString();
            }
            else
            {
                MessageBox.Show("No Book available with this ISBN");
                this.Close();
                th = new Thread(openissueform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        private void deccopies()
        {
            con.Open();
            con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Book_Table set copies=" + (int.Parse(dt1.Rows[0]["Copies"].ToString()) - 1) + " where Book_ISBN='" + dt1.Rows[0]["Book_ISBN"].ToString() + "'";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void Alreadyassignedcheck()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From IssueBook_Table where Book_ISBN='" + textBoxissuebookisbn.Text + "' and Registration_Number='" + textBoxissuestudentid.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt2);
            con.Close();
        }

        private void chechlimit()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From IssueBook_Table where Registration_Number='" + textBoxissuestudentid.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt4);
            con.Close();
        }

        private void btnissue_Click(object sender, EventArgs e)
        {
            if (textBoxretdate.Enabled == false)
            {
                MessageBox.Show("Plz Enter Return Date");
            }
            else
            {
                chechlimit();
                Alreadyassignedcheck();
                if (dt2.Rows.Count == 1)
                {
                    MessageBox.Show("This book already issued to this student");
                }
                else if (int.Parse(dt1.Rows[0]["Copies"].ToString()) == 1)
                {
                    MessageBox.Show("This book is not available right now");
                }
                else if (dt4.Rows.Count == 3)
                {
                    MessageBox.Show("This Student cannot issue more books");
                }
                else
                {
                    DialogResult dr;
                    if (checkfine() == true)
                    {
                        dr = MessageBox.Show("You have to Pay Fine First", "Fine", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            delfromfinetable();
                            deccopies();
                            insertintoissuebooktable();
                            insertintoactivitylog();
                            MessageBox.Show("Congratulations!!! Book is Issued Successfully.");
                        }
                    }
                    else
                    {
                        deccopies();
                        insertintoissuebooktable();
                        insertintoactivitylog();
                        MessageBox.Show("Congratulations!!! Book is Issued Successfully.");
                    }
                }
            }
            this.Close();
            th = new Thread(openissueform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void openissueform()
        {
            Application.Run(new IssueBook());
        }

        private void delfromfinetable()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Fine_Table where Registration_Number='"+textBoxissuestudentid.Text+"'";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private bool checkfine()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Fine_Table where Registration_Number='" + textBoxissuestudentid.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt3);
            con.Close();
            if (dt3.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void insertintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'Book with ISBN(" + textBoxissuebookisbn.Text + ") is Issued to Student with Registration_No("+textBoxissuestudentid.Text+").')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void insertintoissuebooktable()
        {
            con.Open();
            con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into IssueBook_Table values('" + textBoxissuestudentid.Text + "','" + textBoxissuebookisbn.Text + "','" + Login.Admin_ID + "',Default,'" + textBoxretdate.Value + "')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void IssueBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
                th = new Thread(openhomeform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }
    }
}
