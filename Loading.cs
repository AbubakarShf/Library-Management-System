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
    public partial class Loading : Form
    {
        Thread th;
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        public void openhomeform()
        {
            Application.Run(new Home());
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            this.progressBar1.Increment(5);
            if (this.progressBar1.Value == 100)
            {
                this.timer1.Stop();
                this.Close();
                th = new Thread(openhomeform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
