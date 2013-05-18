namespace Pexeso
{
    partial class Pexeso
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
            this.pinit = new System.Windows.Forms.Panel();
            this.title = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.groupSize = new System.Windows.Forms.GroupBox();
            this.zamichat = new System.Windows.Forms.CheckBox();
            this.size3 = new System.Windows.Forms.RadioButton();
            this.size2 = new System.Windows.Forms.RadioButton();
            this.size1 = new System.Windows.Forms.RadioButton();
            this.win = new System.Windows.Forms.Panel();
            this.again = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.flipCountLabel = new System.Windows.Forms.Label();
            this.clickCountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.titleWin = new System.Windows.Forms.Label();
            this.pinit.SuspendLayout();
            this.groupSize.SuspendLayout();
            this.win.SuspendLayout();
            this.SuspendLayout();
            // 
            // pinit
            // 
            this.pinit.Controls.Add(this.title);
            this.pinit.Controls.Add(this.start);
            this.pinit.Controls.Add(this.groupSize);
            this.pinit.Location = new System.Drawing.Point(12, 12);
            this.pinit.Name = "pinit";
            this.pinit.Size = new System.Drawing.Size(260, 237);
            this.pinit.TabIndex = 1;
            // 
            // title
            // 
            this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(260, 69);
            this.title.TabIndex = 2;
            this.title.Text = "Pexeso";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // start
            // 
            this.start.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start.Location = new System.Drawing.Point(0, 197);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(260, 40);
            this.start.TabIndex = 1;
            this.start.Text = "Začít hrát";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // groupSize
            // 
            this.groupSize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupSize.Controls.Add(this.zamichat);
            this.groupSize.Controls.Add(this.size3);
            this.groupSize.Controls.Add(this.size2);
            this.groupSize.Controls.Add(this.size1);
            this.groupSize.Location = new System.Drawing.Point(55, 72);
            this.groupSize.Name = "groupSize";
            this.groupSize.Size = new System.Drawing.Size(150, 116);
            this.groupSize.TabIndex = 0;
            this.groupSize.TabStop = false;
            this.groupSize.Text = "Vyber velikost";
            // 
            // zamichat
            // 
            this.zamichat.AutoSize = true;
            this.zamichat.Location = new System.Drawing.Point(6, 88);
            this.zamichat.Name = "zamichat";
            this.zamichat.Size = new System.Drawing.Size(108, 17);
            this.zamichat.TabIndex = 3;
            this.zamichat.Text = "zamíchat při 25%";
            this.zamichat.UseVisualStyleBackColor = true;
            // 
            // size3
            // 
            this.size3.AutoSize = true;
            this.size3.Location = new System.Drawing.Point(6, 65);
            this.size3.Name = "size3";
            this.size3.Size = new System.Drawing.Size(60, 17);
            this.size3.TabIndex = 2;
            this.size3.Tag = "16";
            this.size3.Text = "16 x 16";
            this.size3.UseVisualStyleBackColor = true;
            // 
            // size2
            // 
            this.size2.AutoSize = true;
            this.size2.Checked = true;
            this.size2.Location = new System.Drawing.Point(6, 42);
            this.size2.Name = "size2";
            this.size2.Size = new System.Drawing.Size(48, 17);
            this.size2.TabIndex = 1;
            this.size2.TabStop = true;
            this.size2.Tag = "8";
            this.size2.Text = "8 x 8";
            this.size2.UseVisualStyleBackColor = true;
            // 
            // size1
            // 
            this.size1.AutoSize = true;
            this.size1.Location = new System.Drawing.Point(6, 19);
            this.size1.Name = "size1";
            this.size1.Size = new System.Drawing.Size(48, 17);
            this.size1.TabIndex = 0;
            this.size1.Tag = "4";
            this.size1.Text = "4 x 4";
            this.size1.UseVisualStyleBackColor = true;
            // 
            // win
            // 
            this.win.Controls.Add(this.again);
            this.win.Controls.Add(this.exit);
            this.win.Controls.Add(this.flipCountLabel);
            this.win.Controls.Add(this.clickCountLabel);
            this.win.Controls.Add(this.label2);
            this.win.Controls.Add(this.label1);
            this.win.Controls.Add(this.titleWin);
            this.win.Location = new System.Drawing.Point(12, 12);
            this.win.Name = "win";
            this.win.Size = new System.Drawing.Size(260, 237);
            this.win.TabIndex = 3;
            this.win.Visible = false;
            // 
            // again
            // 
            this.again.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.again.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.again.Location = new System.Drawing.Point(0, 157);
            this.again.Name = "again";
            this.again.Size = new System.Drawing.Size(260, 40);
            this.again.TabIndex = 8;
            this.again.Text = "Nová hra";
            this.again.UseVisualStyleBackColor = true;
            this.again.Click += new System.EventHandler(this.again_Click);
            // 
            // exit
            // 
            this.exit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.Location = new System.Drawing.Point(0, 197);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(260, 40);
            this.exit.TabIndex = 7;
            this.exit.Text = "Ukončit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // flipCountLabel
            // 
            this.flipCountLabel.AutoSize = true;
            this.flipCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flipCountLabel.Location = new System.Drawing.Point(136, 112);
            this.flipCountLabel.Name = "flipCountLabel";
            this.flipCountLabel.Size = new System.Drawing.Size(24, 25);
            this.flipCountLabel.TabIndex = 6;
            this.flipCountLabel.Text = "#";
            // 
            // clickCountLabel
            // 
            this.clickCountLabel.AutoSize = true;
            this.clickCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clickCountLabel.Location = new System.Drawing.Point(136, 86);
            this.clickCountLabel.Name = "clickCountLabel";
            this.clickCountLabel.Size = new System.Drawing.Size(24, 25);
            this.clickCountLabel.TabIndex = 5;
            this.clickCountLabel.Text = "#";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Počet otočení";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Počet kliknutí";
            // 
            // titleWin
            // 
            this.titleWin.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleWin.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleWin.Location = new System.Drawing.Point(0, 0);
            this.titleWin.Name = "titleWin";
            this.titleWin.Size = new System.Drawing.Size(260, 69);
            this.titleWin.TabIndex = 2;
            this.titleWin.Text = "Výhra!!!";
            this.titleWin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pexeso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.win);
            this.Controls.Add(this.pinit);
            this.Name = "Pexeso";
            this.Text = "Pexeso";
            this.pinit.ResumeLayout(false);
            this.groupSize.ResumeLayout(false);
            this.groupSize.PerformLayout();
            this.win.ResumeLayout(false);
            this.win.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pinit;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.GroupBox groupSize;
        private System.Windows.Forms.RadioButton size3;
        private System.Windows.Forms.RadioButton size2;
        private System.Windows.Forms.RadioButton size1;
        private System.Windows.Forms.Panel win;
        private System.Windows.Forms.Label titleWin;
        private System.Windows.Forms.Label flipCountLabel;
        private System.Windows.Forms.Label clickCountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox zamichat;
        private System.Windows.Forms.Button again;
        private System.Windows.Forms.Button exit;



    }
}

