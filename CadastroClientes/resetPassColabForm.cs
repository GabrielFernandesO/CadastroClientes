using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.IO;

namespace CadastroClientes
{

    public partial class resetPassColabForm : Form
    {
        //Global Variable to select Colaborator for Query
        private string colabForQuery;
        //States to use with Buttons
        private bool passwordState = false;
        private bool passwordConfirmState = false;
        public resetPassColabForm()
        {
            InitializeComponent();

            txtUserColab.Enabled = false;

            //Handle Languages
            if (GlobalVariables.Language == "en")
            {
                lblSearch.Text = "Search Colaborator";
                btnSearchColab.Text = "Search";

                ColaboratorList.Columns[0].Text = "Full Name";
                ColaboratorList.Columns[1].Text = "User";
                ColaboratorList.Columns[2].Text = "Email";

                lblUser.Text = "User";
                lblPass.Text = "Password";
                lblConfirmPass.Text = "Confirm Password";

                btnRedefinePass.Text = "Redefine";
                this.Text = "Password reset";
            }
            else
            {
                lblSearch.Text = "Procurar Colaborador";
                btnSearchColab.Text = "Procurar";

                ColaboratorList.Columns[0].Text = "Nome Completo";
                ColaboratorList.Columns[1].Text = "Usuário";
                ColaboratorList.Columns[2].Text = "Email";

                lblUser.Text = "Usuário";
                lblPass.Text = "Senha";
                lblConfirmPass.Text = "Confirmar Senha";

                btnRedefinePass.Text = "Redefinir";
                this.Text = "Redefinição de senha";
            }

            //Default background of Two Buttons
            btnShowPass.BackgroundImage = Image.FromFile(@"icons/closedEye.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;

            btnShowConfirmPass.BackgroundImage = Image.FromFile(@"icons/closedEye.png");
            btnShowConfirmPass.BackgroundImageLayout = ImageLayout.Zoom;
        }

        //Find Colaborator
        private void btnSearchColab_Click(object sender, EventArgs e)
        {


            colabForQuery = txtSearchColab.Text.ToUpper();

            //Path DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";
            string strConnection = @"Data Source = " + pathDB + "; Version = 3";

            string query = @"
                    SELECT name, user, email 
                    FROM colaborators
                    WHERE name = @colaboratorUser
                       OR email = @colaboratorUser
                       OR user = @colaboratorUser
                    ";

            try
            {
                //Finding all data of DB to insert in ListView
                using (SQLiteConnection connection = new SQLiteConnection(strConnection))
                {
                    connection.Open();


                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@colaboratorUser", colabForQuery);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            //While Reading, add to a ListView with a new item
                            while (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string user = reader["user"].ToString();
                                string email = reader["email"].ToString();


                                ListViewItem item = new ListViewItem(name);
                                item.SubItems.Add(user);
                                item.SubItems.Add(email);

                                ColaboratorList.Items.Add(item);


                                if (ColaboratorList.SelectedItems.Count == 0)
                                {
                                    // Se não houver, seleciona automaticamente o primeiro item, se existir
                                    if (ColaboratorList.Items.Count > 0)
                                    {
                                        ColaboratorList.Items[0].Selected = true;
                                        ColaboratorList.Focus(); // Foca na lista para indicar visualmente a seleção
                                    }
                                }

                                txtUserColab.Text = user;
                                txtSearchColab.Text = "";
                            }
                        }
                    }
                }



                if (ColaboratorList.Items.Count == 0)
                {
                    if (GlobalVariables.Language == "en")
                    {
                        MessageBox.Show("Colaborator not found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Colaborador não encontrado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Update pass of Colaborator
        private void btnRedefinePass_Click(object sender, EventArgs e)
        {

            if(txtUserColab.Text == "" ||  txtSearchColab.Text == null)
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("User not selected", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Usuário não selecionado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (!verifyFields())
            {
                return;
            }

            string newpass = NewhashPassword();


            string query = @"
                UPDATE colaborators
                SET password_hash = @newPassword
                WHERE user = @userForEdit
                ";

            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";
            string strConection = @"Data Source = " + pathDB + "; Version = 3";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(strConection))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        // Adicionando parâmetros
                        cmd.Parameters.AddWithValue("@newPassword", newpass);

                        cmd.Parameters.AddWithValue("@userForEdit", colabForQuery);

                        cmd.ExecuteNonQuery();
                    }

                    txtPass.Text = "";
                    txtConfirmPass.Text = "";

                    ColaboratorList.Items.Clear();
                   
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GlobalVariables.Language == "en")
            {
                MessageBox.Show("Password Updated", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Senha Atualizada", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();

        }

        //Validate Fields
        private bool verifyFields()
        {
            string pass = txtPass.Text;
            string confirmPass = txtConfirmPass.Text;

            if((pass == "" || confirmPass == "") || (pass == null || confirmPass == null))
            {
                if(GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Empty Fields", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    MessageBox.Show("Campos em branco", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
            }

            if(pass != confirmPass)
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Passwords do not match", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    MessageBox.Show("As senhas não coincidem", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        //Building Hash for the new password
        private string NewhashPassword()
        {
            string pass = txtPass.Text;
    
            passwordHash passwordHash = new passwordHash();

            string newPassHash = passwordHash.RetornarMD5(pass);

            return  newPassHash;
        }

        //Buttons to toggle visibility of passwords
        private void btnShowPass_Click(object sender, EventArgs e)
        {


            passwordState = !passwordState;
            txtPass.PasswordChar = passwordState ? '\0' : '*';

            if(passwordState)
            {
                btnShowPass.BackgroundImage = Image.FromFile(@"icons/openedEye.png");
            }
            else
            {
                btnShowPass.BackgroundImage = Image.FromFile(@"icons/closedEye.png");
            }

            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;


        }

        private void btnShowConfirmPass_Click(object sender, EventArgs e)
        {
            passwordConfirmState = !passwordConfirmState;
            txtConfirmPass.PasswordChar = passwordConfirmState ? '\0' : '*';

            if (passwordConfirmState)
            {
                btnShowConfirmPass.BackgroundImage = Image.FromFile(@"icons/openedEye.png");
            }
            else
            {
                btnShowConfirmPass.BackgroundImage = Image.FromFile(@"icons/closedEye.png");
            }

            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;
        }
    }
}
