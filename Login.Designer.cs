namespace Library_Management_Syatem
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.labelloginpassword = new System.Windows.Forms.Label();
            this.labelloginusername = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxloginusername = new System.Windows.Forms.TextBox();
            this.textBoxloginpassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelloginpassword
            // 
            this.labelloginpassword.AutoSize = true;
            this.labelloginpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelloginpassword.Location = new System.Drawing.Point(513, 193);
            this.labelloginpassword.Name = "labelloginpassword";
            this.labelloginpassword.Size = new System.Drawing.Size(78, 20);
            this.labelloginpassword.TabIndex = 23;
            this.labelloginpassword.Text = "Password";
            // 
            // labelloginusername
            // 
            this.labelloginusername.AutoSize = true;
            this.labelloginusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelloginusername.Location = new System.Drawing.Point(513, 131);
            this.labelloginusername.Name = "labelloginusername";
            this.labelloginusername.Size = new System.Drawing.Size(104, 20);
            this.labelloginusername.TabIndex = 0;
            this.labelloginusername.Text = "Username/ID";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button1.Location = new System.Drawing.Point(629, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "Login";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button2.Location = new System.Drawing.Point(730, 238);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 27);
            this.button2.TabIndex = 4;
            this.button2.Text = "SignUp";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(648, 271);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 30);
            this.button3.TabIndex = 5;
            this.button3.Text = "Forget Password";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxloginusername
            // 
            this.textBoxloginusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxloginusername.Location = new System.Drawing.Point(629, 128);
            this.textBoxloginusername.Name = "textBoxloginusername";
            this.textBoxloginusername.Size = new System.Drawing.Size(178, 26);
            this.textBoxloginusername.TabIndex = 1;
            // 
            // textBoxloginpassword
            // 
            this.textBoxloginpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxloginpassword.Location = new System.Drawing.Point(629, 193);
            this.textBoxloginpassword.Name = "textBoxloginpassword";
            this.textBoxloginpassword.PasswordChar = '*';
            this.textBoxloginpassword.Size = new System.Drawing.Size(178, 26);
            this.textBoxloginpassword.TabIndex = 2;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1037, 571);
            this.Controls.Add(this.textBoxloginpassword);
            this.Controls.Add(this.textBoxloginusername);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelloginpassword);
            this.Controls.Add(this.labelloginusername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelloginpassword;
        private System.Windows.Forms.Label labelloginusername;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxloginusername;
        private System.Windows.Forms.TextBox textBoxloginpassword;
    }
}