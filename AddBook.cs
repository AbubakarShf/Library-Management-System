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
    public partial class AddBook : Form
    {
        DataTable dt1 = new DataTable();
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9CRFK3A;Initial Catalog=Library_Management_System;Integrated Security=True");
        DataTable dt = new DataTable();
        Thread th;
        public AddBook()
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

        public void openhomeform()
        {
            Application.Run(new Home());
        }

        private void updatecopies()
        {
            int copy = int.Parse(dt1.Rows[0]["Copies"].ToString()) + int.Parse(textBoxaddbookcopies.Text);
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update Book_Table set Copies=" + copy + " where Book_ISBN='" + textBoxaddbookisbn.Text + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Book Copies Updated Successfully!");
            con.Close();
        }

        private void insertupdateintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'More Copies of Book with ISBN(" + textBoxaddbookisbn.Text + ") is Added by Vendor (" + textBoxvendorname.Text + ").')";
            cmd.ExecuteNonQuery();
            con.Close();
        }


        private void btnaddbook_Click(object sender, EventArgs e)
        {
            if (textBoxaddbookisbn.Text == "")
            {
                MessageBox.Show("Please Enter Book ISBN");
            }
            else if (textBoxaddbooktitle.Text == "")
            {
                MessageBox.Show("Please Enter Book Title");
            }
            else if (textBoxaddbookauthor1.Text == "")
            {
                MessageBox.Show("Please Enter Author Name");
            }
            else if (textBoxaddbookpages.Text == "")
            {
                MessageBox.Show("Please Enter Number of Pages");
            }
            else if (textBoxaddbookprice.Text == "")
            {
                MessageBox.Show("Please Enter Book Price");
            }
            else if (textBoxaddbookcopies.Text == "")
            {
                MessageBox.Show("Please Enter Number of Copies");
            }
            else if (textBoxaddbookisbn.Text.Length > 50 || textBoxaddbookisbn.Text.Length < 3)
            {
                MessageBox.Show("ISBN length must be between 3-50");
            }
            else if (textBoxaddbooktitle.Text.Length > 50)
            {
                MessageBox.Show("Title length must be <50");
            }
            else if (textBoxaddbookauthor1.Text.Length > 50)
            {
                MessageBox.Show("Author Name length must be <50");
            }
            else if (int.Parse(textBoxaddbookcopies.Text) <= 2)
            {
                MessageBox.Show("Copies must be > 2");
            }
            else
            {
                Check();
                if (dt1.Rows.Count == 1)
                {
                    DialogResult dr=MessageBox.Show("ISBN is allocated to another Book!\n\nDo You Want to Increase its Copies?","Alert",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        updatecopies();
                        insertupdateintoactivitylog();
                        this.Close();
                        th = new Thread(openaddbookform);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();
                    }
                    else
                    {
                        this.Close();
                        th = new Thread(openaddbookform);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();
                    }
                }
                else
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Book_Table values('" + textBoxaddbookisbn.Text + "','" + textBoxaddbooktitle.Text + "','" + textBoxaddbookauthor1.Text + "'," + int.Parse(textBoxaddbookpages.Text) + "," + int.Parse(textBoxaddbookprice.Text) + "," + int.Parse(textBoxaddbookcopies.Text) + ")";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Book Added Successfully");
                    insertintoSupplyTable();
                    insertintoactivitylog();
                    this.Close();
                    th = new Thread(openaddbookform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
            }
        }


        private void insertintoactivitylog()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into ActivityLog_Table values(Default,'New Book with ISBN(" + textBoxaddbookisbn.Text + ") is Added by Vendor ("+textBoxvendorname.Text+").')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void Check()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Book_Table where Book_ISBN='" + textBoxaddbookisbn.Text + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt1);
            con.Close();
        }

        private void insertintoSupplyTable()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into SupplyBook_Table values(" + int.Parse(textBoxvendorid.Text) + ",'" + textBoxaddbookisbn.Text + "',Default," + int.Parse(textBoxaddbookcopies.Text) + "," + (int.Parse(textBoxaddbookcopies.Text) * int.Parse(textBoxaddbookprice.Text)) + ")";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void openaddbookform()
        {
            Application.Run(new AddBook());
        }

        private void btnvendorsearch_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * From Vendor_Table where Vendor_ID='" + int.Parse(textBoxvendorid.Text) + "'";
            cmd.ExecuteNonQuery();
            ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                DialogResult dr=MessageBox.Show("Vendor not exists\nDo you want to add new Vendor","Vendor",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    this.Close();
                    th = new Thread(openaddbookform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    AddVendor v = new AddVendor();
                    v.Show();
                }
            }
            else
            {
                btnaddbook.Visible = true;
                btnvendorsearch.Visible = false;
                textBoxvendorid.Visible = false;
                labelvendorid.Visible = false;
                textBoxvendorname.Visible = true;
                textBoxvendoraddress.Visible = true;
                textBoxvendorphoneno.Visible = true;
                labelvendorname.Visible = true;
                labelvendoraddress.Visible = true;
                labelvendorphoneno.Visible = true;
                textBoxvendorid.Text = dt.Rows[0]["Vendor_ID"].ToString();
                textBoxvendorname.Text = dt.Rows[0]["Vendor_Name"].ToString();
                textBoxvendoraddress.Text = dt.Rows[0]["Vendor_Address"].ToString();
                textBoxvendorphoneno.Text = dt.Rows[0]["Phone_Number"].ToString();
            }
            con.Close();
        }
    }
}
