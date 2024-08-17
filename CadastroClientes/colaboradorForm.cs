using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace CadastroClientes
{
    public partial class colaboradorForm : Form
    {
        public colaboradorForm()
        {
            InitializeComponent();

            //Options to ComboBox
            access_Level.Items.Add("Nível de acesso 0");
            access_Level.Items.Add("Nível de acesso 1");
            access_Level.SelectedIndex = 0;
            btnClearIMG.Visible = false;

            //Handle Languages
            if (GlobalVariables.Language == "en")
            {
                label1.Text = "Full Name";
                label2.Text = "Email";
                label3.Text = "Username";
                label4.Text = "Password";
                label5.Text = "Confirm Password";
                label6.Text = "Access Level";
                access_Level.Text = "Access Level";
                access_Level.Items[0] = "Access Level 0";
                access_Level.Items[1] = "Access Level 1";
                btnRegisterColab.Text = "Register";
                btnSelectIMG.Text = "Select Image";
                btnClearIMG.Text = "Remove Image";
                this.Text = "Colaborator Registration";
            }
            else
            {
                label1.Text = "Nome Completo";
                label2.Text = "Email";
                label3.Text = "Usuário";
                label4.Text = "Senha";
                label5.Text = "Confirmar Senha";
                label6.Text = "Nível de Acesso";
                access_Level.Text = "Nível de Acesso";
                access_Level.Items[0] = "Nível de Acesso 0";
                access_Level.Items[1] = "Nível de Acesso 1";
                btnRegisterColab.Text = "Cadastrar";
                btnSelectIMG.Text = "Selecionar Imagem";
                btnClearIMG.Text = "Remover Imagem";
                this.Text = "Cadastrar Colaborador";
            }
        }

        //Logical to send Datas of User
        private void btnRegisterColab_Click(object sender, EventArgs e)
        {

            //Fields to Send
            string name = txtFullName.Text.ToUpper();
            string email = txtEmail.Text.ToUpper();
            string user = txtUser.Text.ToUpper();
            string password = txtPassword.Text;
            string confirm_pass = txtConfirmPassword.Text;
            string imgPathName = imgPath.Text;
            byte[] photo = null;
            int user_acces_Level = access_Level.SelectedIndex;

            //Validates
            if (!emptyFields())
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Have some empty fields", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Um ou mais campos estão vazios", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (!confirmUserPass())
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Passwords are different check again", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("As senhas não coincidem tente novamente", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (existsUser())
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("User exists", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Usuário existente", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            else
            {
                if (!(imgPathName == "imgPath"))
                {
                    try
                    {
                        photo = File.ReadAllBytes(imgPathName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Erro ao ler a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }


                passwordHash passHash = new passwordHash();

                string pass_hash = passHash.RetornarMD5(password);


                //Path DB
                string pathDB = Application.StartupPath + @"\db\DBSQLite.db";

                //Connection string
                string strConection = @"Data Source = " + pathDB + "; Version = 3";

                //Instance of Connection
                SQLiteConnection connection = new SQLiteConnection(strConection);

                try
                {
                    string query = @"
                    INSERT INTO colaborators (name, email, user, password_hash, access_level, photo)
                    VALUES (@name, @email, @user, @password_hash, @access_level, @photo);
                    ";



                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {

                        connection.Open();

                        // Adicionando parâmetros
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@password_hash", pass_hash);
                        cmd.Parameters.AddWithValue("@access_level", user_acces_Level);
                        cmd.Parameters.AddWithValue("@photo", photo);

                        // Executando o comando
                        cmd.ExecuteNonQuery();

                        if (GlobalVariables.Language == "en")
                        {
                            MessageBox.Show("New Colaborator Created", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Novo colaborador criado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

                this.Close();
            }

        }



        //Logicals to Select and Clear Images, Validates in General and confirm Pass Check
        private void btnSelectIMG_Click(object sender, EventArgs e)
        {

            DialogResult result = openDialogIMG.ShowDialog();

            if (result == DialogResult.OK)
            {
                imgPath.Text = openDialogIMG.FileName;
                Image originalImage = Image.FromFile(imgPath.Text);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                //Define width and height to be resize
                //Math and scaleFactor to decreased resolution to fill picturebox
                int pictureBoxWidth = pictureBox.Width;
                int pictureBoxHeight = pictureBox.Height;
                float scaleWidth = (float)pictureBoxWidth / (float)originalImage.Width;
                float scaleHeight = (float)pictureBoxHeight / (float)originalImage.Height;

                float scaleFactor = Math.Min(scaleWidth, scaleHeight);

                int newWidth = (int)(originalImage.Width * scaleFactor);
                int newHeight = (int)(originalImage.Height * scaleFactor);

                Image resizedImage = new Bitmap(originalImage, newWidth, newHeight);

                pictureBox.Image = resizedImage;
                btnClearIMG.Visible = true;
            }
        }

        private bool existsUser()
        {

            string user = txtUser.Text.ToUpper();
            string email = txtEmail.Text.ToUpper();

            //Path DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";

            //Connection string
            string strConnection = @"Data Source = " + pathDB + "; Version = 3";

            using (SQLiteConnection connection = new SQLiteConnection(strConnection))
            {
                try
                {
                    string query = @"
                    SELECT COUNT(*) 
                    FROM colaborators
                    WHERE user = @user
                    OR email = @userEmail
            ";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        // Adicionando parâmetros
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@userEmail", email);

                        connection.Open();

                        // Executando o comando e lendo o resultado
                        long count = (long)cmd.ExecuteScalar();

                        // Se count for maior que 0, então há um registro que corresponde ao usuário ou email fornecido
                        bool exists = count > 0;

                        return exists;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        private bool emptyFields()
        {
            return !string.IsNullOrWhiteSpace(txtFullName.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtUser.Text) &&
                   !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                   !string.IsNullOrWhiteSpace(txtConfirmPassword.Text);
        }

        private bool confirmUserPass()
        {
            string pass1 = txtPassword.Text;
            string pass2 = txtConfirmPassword.Text;

            if(pass1 == pass2)
            {
                return true;
            }

            return false;
        }

        private void btnClearIMG_Click(object sender, EventArgs e)
        {
            imgPath.Text = "imgPath";
            pictureBox.Image = null;
        }
    }
}
