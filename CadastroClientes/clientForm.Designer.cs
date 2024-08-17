namespace CadastroClientes
{
    partial class clientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(clientForm));
            this.btnClearIMG = new System.Windows.Forms.Button();
            this.imgPath = new System.Windows.Forms.Label();
            this.btnSelectIMG = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.ComboBox();
            this.btnRegisterColab = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
            this.openDialogIMG = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClearIMG
            // 
            this.btnClearIMG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearIMG.Location = new System.Drawing.Point(22, 311);
            this.btnClearIMG.Name = "btnClearIMG";
            this.btnClearIMG.Size = new System.Drawing.Size(191, 28);
            this.btnClearIMG.TabIndex = 40;
            this.btnClearIMG.Text = "Remover Imagem";
            this.btnClearIMG.UseVisualStyleBackColor = true;
            this.btnClearIMG.Click += new System.EventHandler(this.btnClearIMG_Click);
            // 
            // imgPath
            // 
            this.imgPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgPath.Location = new System.Drawing.Point(19, 254);
            this.imgPath.Name = "imgPath";
            this.imgPath.Size = new System.Drawing.Size(194, 20);
            this.imgPath.TabIndex = 39;
            this.imgPath.Text = "imgPath";
            this.imgPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectIMG
            // 
            this.btnSelectIMG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectIMG.Location = new System.Drawing.Point(22, 277);
            this.btnSelectIMG.Name = "btnSelectIMG";
            this.btnSelectIMG.Size = new System.Drawing.Size(191, 28);
            this.btnSelectIMG.TabIndex = 38;
            this.btnSelectIMG.Text = "Selecionar Imagem";
            this.btnSelectIMG.UseVisualStyleBackColor = true;
            this.btnSelectIMG.Click += new System.EventHandler(this.btnSelectIMG_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(231, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 23);
            this.label6.TabIndex = 37;
            this.label6.Text = "Gênero";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGender
            // 
            this.txtGender.DisplayMember = "0";
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.FormattingEnabled = true;
            this.txtGender.Location = new System.Drawing.Point(235, 310);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(313, 33);
            this.txtGender.TabIndex = 36;
            // 
            // btnRegisterColab
            // 
            this.btnRegisterColab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegisterColab.Location = new System.Drawing.Point(190, 425);
            this.btnRegisterColab.Name = "btnRegisterColab";
            this.btnRegisterColab.Size = new System.Drawing.Size(206, 55);
            this.btnRegisterColab.TabIndex = 35;
            this.btnRegisterColab.Text = "Cadastrar";
            this.btnRegisterColab.UseVisualStyleBackColor = true;
            this.btnRegisterColab.Click += new System.EventHandler(this.btnRegisterColab_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(235, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 23);
            this.label4.TabIndex = 33;
            this.label4.Text = "Data de Nascimento";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(235, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 23);
            this.label2.TabIndex = 28;
            this.label2.Text = "Email";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(235, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 23);
            this.label1.TabIndex = 27;
            this.label1.Text = "Nome Completo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox.Location = new System.Drawing.Point(22, 57);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(191, 194);
            this.pictureBox.TabIndex = 26;
            this.pictureBox.TabStop = false;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(235, 167);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(313, 31);
            this.txtEmail.TabIndex = 25;
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(235, 91);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(313, 31);
            this.txtFullName.TabIndex = 24;
            // 
            // txtDate
            // 
            this.txtDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Location = new System.Drawing.Point(235, 243);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(313, 22);
            this.txtDate.TabIndex = 42;
            // 
            // sqLiteCommand1
            // 
            this.sqLiteCommand1.CommandText = null;
            // 
            // openDialogIMG
            // 
            this.openDialogIMG.FileName = "openFileDialog1";
            // 
            // clientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(590, 568);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.btnClearIMG);
            this.Controls.Add(this.imgPath);
            this.Controls.Add(this.btnSelectIMG);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtGender);
            this.Controls.Add(this.btnRegisterColab);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtFullName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "clientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "clientForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClearIMG;
        private System.Windows.Forms.Label imgPath;
        private System.Windows.Forms.Button btnSelectIMG;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox txtGender;
        private System.Windows.Forms.Button btnRegisterColab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
        private System.Windows.Forms.OpenFileDialog openDialogIMG;
    }
}