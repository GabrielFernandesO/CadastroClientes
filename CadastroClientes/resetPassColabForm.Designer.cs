namespace CadastroClientes
{
    partial class resetPassColabForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(resetPassColabForm));
            this.txtSearchColab = new System.Windows.Forms.TextBox();
            this.txtUserColab = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnRedefinePass = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.ColaboratorList = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.txtConfirmPass = new System.Windows.Forms.TextBox();
            this.btnSearchColab = new System.Windows.Forms.Button();
            this.btnShowPass = new System.Windows.Forms.Button();
            this.btnShowConfirmPass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSearchColab
            // 
            this.txtSearchColab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchColab.Location = new System.Drawing.Point(167, 35);
            this.txtSearchColab.Name = "txtSearchColab";
            this.txtSearchColab.Size = new System.Drawing.Size(426, 31);
            this.txtSearchColab.TabIndex = 0;
            // 
            // txtUserColab
            // 
            this.txtUserColab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserColab.Location = new System.Drawing.Point(167, 210);
            this.txtUserColab.Name = "txtUserColab";
            this.txtUserColab.Size = new System.Drawing.Size(426, 31);
            this.txtUserColab.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(167, 290);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(426, 31);
            this.txtPass.TabIndex = 2;
            // 
            // btnRedefinePass
            // 
            this.btnRedefinePass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedefinePass.Location = new System.Drawing.Point(280, 431);
            this.btnRedefinePass.Name = "btnRedefinePass";
            this.btnRedefinePass.Size = new System.Drawing.Size(189, 46);
            this.btnRedefinePass.TabIndex = 4;
            this.btnRedefinePass.Text = "Redefinir";
            this.btnRedefinePass.UseVisualStyleBackColor = true;
            this.btnRedefinePass.Click += new System.EventHandler(this.btnRedefinePass_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(162, 9);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(260, 23);
            this.lblSearch.TabIndex = 5;
            this.lblSearch.Text = "Procurar Colaborador";
            // 
            // ColaboratorList
            // 
            this.ColaboratorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colUser,
            this.colEmail});
            this.ColaboratorList.HideSelection = false;
            this.ColaboratorList.Location = new System.Drawing.Point(167, 89);
            this.ColaboratorList.Name = "ColaboratorList";
            this.ColaboratorList.Size = new System.Drawing.Size(426, 68);
            this.ColaboratorList.TabIndex = 6;
            this.ColaboratorList.UseCompatibleStateImageBehavior = false;
            this.ColaboratorList.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Nome Completo";
            this.colName.Width = 142;
            // 
            // colUser
            // 
            this.colUser.Text = "Usuário";
            this.colUser.Width = 142;
            // 
            // colEmail
            // 
            this.colEmail.Text = "Email";
            this.colEmail.Width = 142;
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(162, 174);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(260, 23);
            this.lblUser.TabIndex = 7;
            this.lblUser.Text = "Usuário";
            // 
            // lblPass
            // 
            this.lblPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.Location = new System.Drawing.Point(162, 254);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(260, 23);
            this.lblPass.TabIndex = 8;
            this.lblPass.Text = "Senha";
            // 
            // lblConfirmPass
            // 
            this.lblConfirmPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPass.Location = new System.Drawing.Point(162, 334);
            this.lblConfirmPass.Name = "lblConfirmPass";
            this.lblConfirmPass.Size = new System.Drawing.Size(260, 23);
            this.lblConfirmPass.TabIndex = 10;
            this.lblConfirmPass.Text = "Confirmar Senha";
            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPass.Location = new System.Drawing.Point(167, 370);
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.PasswordChar = '*';
            this.txtConfirmPass.Size = new System.Drawing.Size(426, 31);
            this.txtConfirmPass.TabIndex = 9;
            // 
            // btnSearchColab
            // 
            this.btnSearchColab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchColab.Location = new System.Drawing.Point(608, 35);
            this.btnSearchColab.Name = "btnSearchColab";
            this.btnSearchColab.Size = new System.Drawing.Size(119, 31);
            this.btnSearchColab.TabIndex = 11;
            this.btnSearchColab.Text = "Procurar";
            this.btnSearchColab.UseVisualStyleBackColor = true;
            this.btnSearchColab.Click += new System.EventHandler(this.btnSearchColab_Click);
            // 
            // btnShowPass
            // 
            this.btnShowPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowPass.Location = new System.Drawing.Point(555, 294);
            this.btnShowPass.Name = "btnShowPass";
            this.btnShowPass.Size = new System.Drawing.Size(36, 23);
            this.btnShowPass.TabIndex = 12;
            this.btnShowPass.UseVisualStyleBackColor = true;
            this.btnShowPass.Click += new System.EventHandler(this.btnShowPass_Click);
            // 
            // btnShowConfirmPass
            // 
            this.btnShowConfirmPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowConfirmPass.Location = new System.Drawing.Point(555, 374);
            this.btnShowConfirmPass.Name = "btnShowConfirmPass";
            this.btnShowConfirmPass.Size = new System.Drawing.Size(36, 23);
            this.btnShowConfirmPass.TabIndex = 13;
            this.btnShowConfirmPass.UseVisualStyleBackColor = true;
            this.btnShowConfirmPass.Click += new System.EventHandler(this.btnShowConfirmPass_Click);
            // 
            // resetPassColabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(754, 500);
            this.Controls.Add(this.btnShowConfirmPass);
            this.Controls.Add(this.btnShowPass);
            this.Controls.Add(this.btnSearchColab);
            this.Controls.Add(this.lblConfirmPass);
            this.Controls.Add(this.txtConfirmPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.ColaboratorList);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.btnRedefinePass);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUserColab);
            this.Controls.Add(this.txtSearchColab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "resetPassColabForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Redefinição de Senha";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchColab;
        private System.Windows.Forms.TextBox txtUserColab;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnRedefinePass;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ListView ColaboratorList;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colUser;
        private System.Windows.Forms.ColumnHeader colEmail;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.TextBox txtConfirmPass;
        private System.Windows.Forms.Button btnSearchColab;
        private System.Windows.Forms.Button btnShowPass;
        private System.Windows.Forms.Button btnShowConfirmPass;
    }
}