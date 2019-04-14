namespace Client
{
    partial class Form2
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
            this.textServerName = new System.Windows.Forms.TextBox();
            this.buttonConn = new System.Windows.Forms.Button();
            this.labelServerName = new System.Windows.Forms.Label();
            this.textUserName = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textServerName
            // 
            this.textServerName.Location = new System.Drawing.Point(16, 81);
            this.textServerName.Name = "textServerName";
            this.textServerName.Size = new System.Drawing.Size(256, 20);
            this.textServerName.TabIndex = 0;
            // 
            // buttonConn
            // 
            this.buttonConn.Location = new System.Drawing.Point(94, 129);
            this.buttonConn.Name = "buttonConn";
            this.buttonConn.Size = new System.Drawing.Size(92, 23);
            this.buttonConn.TabIndex = 1;
            this.buttonConn.Text = "Подключиться";
            this.buttonConn.UseVisualStyleBackColor = true;
            this.buttonConn.Click += new System.EventHandler(this.buttonConn_Click);
            // 
            // labelServerName
            // 
            this.labelServerName.AutoSize = true;
            this.labelServerName.Location = new System.Drawing.Point(13, 65);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(44, 13);
            this.labelServerName.TabIndex = 2;
            this.labelServerName.Text = "Сервер";
            // 
            // textUserName
            // 
            this.textUserName.Location = new System.Drawing.Point(16, 29);
            this.textUserName.Name = "textUserName";
            this.textUserName.Size = new System.Drawing.Size(256, 20);
            this.textUserName.TabIndex = 3;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(13, 13);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(103, 13);
            this.labelUserName.TabIndex = 4;
            this.labelUserName.Text = "Имя пользователя";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 164);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.textUserName);
            this.Controls.Add(this.labelServerName);
            this.Controls.Add(this.buttonConn);
            this.Controls.Add(this.textServerName);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервер";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textServerName;
        private System.Windows.Forms.Button buttonConn;
        private System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.TextBox textUserName;
        private System.Windows.Forms.Label labelUserName;
    }
}