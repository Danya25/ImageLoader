namespace ImageLoader
{
    partial class ImageLoader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageLoader));
            this.loadCsv_btn = new System.Windows.Forms.Button();
            this.links_label = new System.Windows.Forms.Label();
            this.numberLinks_textbox = new System.Windows.Forms.TextBox();
            this.startUpload_btn = new System.Windows.Forms.Button();
            this.proc_pb = new System.Windows.Forms.ProgressBar();
            this.proxyLoad_btn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.countProxy_txtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.clearCSV_btn = new System.Windows.Forms.Button();
            this.proxyClear_btn = new System.Windows.Forms.Button();
            this.puaseLoad_btn = new System.Windows.Forms.Button();
            this.resumeProc_btn = new System.Windows.Forms.Button();
            this.imgur_checkBox = new System.Windows.Forms.CheckBox();
            this.postimages_checkBox = new System.Windows.Forms.CheckBox();
            this.log_txtBox = new System.Windows.Forms.RichTextBox();
            this.clientId_txtBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.clientIdPostImages_txtBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ftpLoad_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // loadCsv_btn
            // 
            this.loadCsv_btn.Location = new System.Drawing.Point(12, 57);
            this.loadCsv_btn.Name = "loadCsv_btn";
            this.loadCsv_btn.Size = new System.Drawing.Size(165, 29);
            this.loadCsv_btn.TabIndex = 0;
            this.loadCsv_btn.Text = "Browse";
            this.loadCsv_btn.UseVisualStyleBackColor = true;
            this.loadCsv_btn.Click += new System.EventHandler(this.loadCsv_btn_Click);
            // 
            // links_label
            // 
            this.links_label.AutoSize = true;
            this.links_label.Location = new System.Drawing.Point(12, 15);
            this.links_label.Name = "links_label";
            this.links_label.Size = new System.Drawing.Size(59, 13);
            this.links_label.TabIndex = 1;
            this.links_label.Text = "Count links";
            // 
            // numberLinks_textbox
            // 
            this.numberLinks_textbox.Location = new System.Drawing.Point(12, 31);
            this.numberLinks_textbox.Name = "numberLinks_textbox";
            this.numberLinks_textbox.ReadOnly = true;
            this.numberLinks_textbox.Size = new System.Drawing.Size(165, 20);
            this.numberLinks_textbox.TabIndex = 2;
            // 
            // startUpload_btn
            // 
            this.startUpload_btn.Location = new System.Drawing.Point(432, 31);
            this.startUpload_btn.Name = "startUpload_btn";
            this.startUpload_btn.Size = new System.Drawing.Size(78, 29);
            this.startUpload_btn.TabIndex = 3;
            this.startUpload_btn.Text = "Start Upload";
            this.startUpload_btn.UseVisualStyleBackColor = true;
            this.startUpload_btn.Click += new System.EventHandler(this.startUpload_btn_Click);
            // 
            // proc_pb
            // 
            this.proc_pb.ForeColor = System.Drawing.Color.Lime;
            this.proc_pb.Location = new System.Drawing.Point(5, 378);
            this.proc_pb.Name = "proc_pb";
            this.proc_pb.Size = new System.Drawing.Size(833, 23);
            this.proc_pb.TabIndex = 4;
            this.proc_pb.Visible = false;
            // 
            // proxyLoad_btn
            // 
            this.proxyLoad_btn.Location = new System.Drawing.Point(711, 57);
            this.proxyLoad_btn.Name = "proxyLoad_btn";
            this.proxyLoad_btn.Size = new System.Drawing.Size(122, 29);
            this.proxyLoad_btn.TabIndex = 14;
            this.proxyLoad_btn.Text = "Load proxy";
            this.proxyLoad_btn.UseVisualStyleBackColor = true;
            this.proxyLoad_btn.Click += new System.EventHandler(this.proxyLoad_btn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(740, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Count proxy";
            // 
            // countProxy_txtBox
            // 
            this.countProxy_txtBox.Location = new System.Drawing.Point(711, 31);
            this.countProxy_txtBox.Name = "countProxy_txtBox";
            this.countProxy_txtBox.ReadOnly = true;
            this.countProxy_txtBox.Size = new System.Drawing.Size(122, 20);
            this.countProxy_txtBox.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(508, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Control progress";
            // 
            // clearCSV_btn
            // 
            this.clearCSV_btn.Location = new System.Drawing.Point(12, 93);
            this.clearCSV_btn.Name = "clearCSV_btn";
            this.clearCSV_btn.Size = new System.Drawing.Size(165, 23);
            this.clearCSV_btn.TabIndex = 22;
            this.clearCSV_btn.Text = "Clear CSV file";
            this.clearCSV_btn.UseVisualStyleBackColor = true;
            this.clearCSV_btn.Click += new System.EventHandler(this.clearCSV_btn_Click);
            // 
            // proxyClear_btn
            // 
            this.proxyClear_btn.Location = new System.Drawing.Point(711, 93);
            this.proxyClear_btn.Name = "proxyClear_btn";
            this.proxyClear_btn.Size = new System.Drawing.Size(122, 23);
            this.proxyClear_btn.TabIndex = 23;
            this.proxyClear_btn.Text = "Clear proxy lists";
            this.proxyClear_btn.UseVisualStyleBackColor = true;
            this.proxyClear_btn.Click += new System.EventHandler(this.proxyClear_btn_Click);
            // 
            // puaseLoad_btn
            // 
            this.puaseLoad_btn.Location = new System.Drawing.Point(516, 31);
            this.puaseLoad_btn.Name = "puaseLoad_btn";
            this.puaseLoad_btn.Size = new System.Drawing.Size(75, 29);
            this.puaseLoad_btn.TabIndex = 24;
            this.puaseLoad_btn.Text = "Pause";
            this.puaseLoad_btn.UseVisualStyleBackColor = true;
            this.puaseLoad_btn.Click += new System.EventHandler(this.puaseLoad_btn_Click);
            // 
            // resumeProc_btn
            // 
            this.resumeProc_btn.Location = new System.Drawing.Point(597, 31);
            this.resumeProc_btn.Name = "resumeProc_btn";
            this.resumeProc_btn.Size = new System.Drawing.Size(75, 29);
            this.resumeProc_btn.TabIndex = 25;
            this.resumeProc_btn.Text = "Resume";
            this.resumeProc_btn.UseVisualStyleBackColor = true;
            this.resumeProc_btn.Click += new System.EventHandler(this.resumeProc_btn_Click);
            // 
            // imgur_checkBox
            // 
            this.imgur_checkBox.AutoSize = true;
            this.imgur_checkBox.Location = new System.Drawing.Point(206, 31);
            this.imgur_checkBox.Name = "imgur_checkBox";
            this.imgur_checkBox.Size = new System.Drawing.Size(73, 17);
            this.imgur_checkBox.TabIndex = 26;
            this.imgur_checkBox.Text = "       Imgur";
            this.imgur_checkBox.UseVisualStyleBackColor = true;
            // 
            // postimages_checkBox
            // 
            this.postimages_checkBox.AutoSize = true;
            this.postimages_checkBox.Location = new System.Drawing.Point(208, 80);
            this.postimages_checkBox.Name = "postimages_checkBox";
            this.postimages_checkBox.Size = new System.Drawing.Size(99, 17);
            this.postimages_checkBox.TabIndex = 27;
            this.postimages_checkBox.Text = "      PostImages";
            this.postimages_checkBox.UseVisualStyleBackColor = true;
            // 
            // log_txtBox
            // 
            this.log_txtBox.Location = new System.Drawing.Point(15, 168);
            this.log_txtBox.Name = "log_txtBox";
            this.log_txtBox.ReadOnly = true;
            this.log_txtBox.Size = new System.Drawing.Size(828, 204);
            this.log_txtBox.TabIndex = 28;
            this.log_txtBox.Text = "";
            // 
            // clientId_txtBox
            // 
            this.clientId_txtBox.Location = new System.Drawing.Point(208, 54);
            this.clientId_txtBox.Name = "clientId_txtBox";
            this.clientId_txtBox.Size = new System.Drawing.Size(155, 20);
            this.clientId_txtBox.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(285, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "ClientId for Imgur";
            // 
            // clientIdPostImages_txtBox
            // 
            this.clientIdPostImages_txtBox.Location = new System.Drawing.Point(208, 103);
            this.clientIdPostImages_txtBox.Name = "clientIdPostImages_txtBox";
            this.clientIdPostImages_txtBox.Size = new System.Drawing.Size(155, 20);
            this.clientIdPostImages_txtBox.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(317, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "ClientId for PostImages";
            // 
            // ftpLoad_btn
            // 
            this.ftpLoad_btn.Location = new System.Drawing.Point(12, 139);
            this.ftpLoad_btn.Name = "ftpLoad_btn";
            this.ftpLoad_btn.Size = new System.Drawing.Size(165, 23);
            this.ftpLoad_btn.TabIndex = 33;
            this.ftpLoad_btn.Text = "Start";
            this.ftpLoad_btn.UseVisualStyleBackColor = true;
            this.ftpLoad_btn.Click += new System.EventHandler(this.ftpLoad_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "FTP Loader";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(224, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(224, 79);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 36;
            this.pictureBox2.TabStop = false;
            // 
            // ImageLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 413);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ftpLoad_btn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.clientIdPostImages_txtBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.clientId_txtBox);
            this.Controls.Add(this.log_txtBox);
            this.Controls.Add(this.postimages_checkBox);
            this.Controls.Add(this.imgur_checkBox);
            this.Controls.Add(this.resumeProc_btn);
            this.Controls.Add(this.puaseLoad_btn);
            this.Controls.Add(this.proxyClear_btn);
            this.Controls.Add(this.clearCSV_btn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.countProxy_txtBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.proxyLoad_btn);
            this.Controls.Add(this.proc_pb);
            this.Controls.Add(this.startUpload_btn);
            this.Controls.Add(this.numberLinks_textbox);
            this.Controls.Add(this.links_label);
            this.Controls.Add(this.loadCsv_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageLoader";
            this.Text = "ImageLoader";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadCsv_btn;
        private System.Windows.Forms.Label links_label;
        private System.Windows.Forms.TextBox numberLinks_textbox;
        private System.Windows.Forms.Button startUpload_btn;
        private System.Windows.Forms.ProgressBar proc_pb;
        private System.Windows.Forms.Button proxyLoad_btn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox countProxy_txtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button clearCSV_btn;
        private System.Windows.Forms.Button proxyClear_btn;
        private System.Windows.Forms.Button puaseLoad_btn;
        private System.Windows.Forms.Button resumeProc_btn;
        private System.Windows.Forms.CheckBox imgur_checkBox;
        private System.Windows.Forms.CheckBox postimages_checkBox;
        private System.Windows.Forms.RichTextBox log_txtBox;
        private System.Windows.Forms.TextBox clientId_txtBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox clientIdPostImages_txtBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button ftpLoad_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

