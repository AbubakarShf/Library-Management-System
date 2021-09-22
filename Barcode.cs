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

namespace Library_Management_Syatem
{
    public partial class Barcode : Form
    {
        Thread th;
        public Barcode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string barcode = AddStudent.regnum;
            Zen.Barcode.Code128BarcodeDraw brcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox1.Image = brcode.Draw(barcode, 40);
            button2.Visible = true;
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog savefiledialog = new SaveFileDialog() { Filter = "PNG|*.png" })
            {
                if (savefiledialog.ShowDialog() == DialogResult.OK)
                    pictureBox1.Image.Save(savefiledialog.FileName);
            }
            MessageBox.Show("Barcode is generated.");
            this.Close();
            th = new Thread(openhomeform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void openhomeform()
        {
            Application.Run(new Home());
        }

        private void Barcode_Load(object sender, EventArgs e)
        {
            label2.Text = AddStudent.regnum;
        }
    }
}
