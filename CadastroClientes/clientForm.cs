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

namespace CadastroClientes
{
    public partial class clientForm : Form
    {
        public clientForm()
        {
            InitializeComponent();

            //Initialize options to ComboBox
            txtGender.Items.Add("Masculino");
            txtGender.Items.Add("Feminino");
            txtGender.Items.Add("Outro");
            btnClearIMG.Visible = false;
            txtGender.SelectedIndex = 0;

            //Define Limit to TimePicker, min and max
            DateTime actualDate = DateTime.Now;
            txtDate.Value = DateTime.Now;
            txtDate.MaxDate = DateTime.Now;
            txtDate.MinDate = actualDate.AddYears(-130);

            //Handle Language
            if (GlobalVariables.Language == "en")
            {
                this.Text = "Client Registration";
            }
            else
            {
                this.Text = "Cadastrar Cliente";
            }

            if(GlobalVariables.Language == "en")
            {
                btnClearIMG.Text = "Remove Image";
                btnSelectIMG.Text = "Select Image";
                btnRegisterColab.Text = "Register";

                label1.Text = "Full Name";
                label4.Text = "Birth Date";
                label6.Text = "Gender";
            }
            else
            {
                btnClearIMG.Text = "Remover Imagem";
                btnSelectIMG.Text = "Selecionar Imagem";
                btnRegisterColab.Text = "Cadastrar";

                label1.Text = "Nome Completo";
                label4.Text = "Data de Nascimento";
                label6.Text = "Gênero";
            }

        }

        //Logical to Register
        private void btnRegisterColab_Click(object sender, EventArgs e)
        {
            //Fields to send
            string name = txtFullName.Text.ToUpper();
            string email = txtEmail.Text.ToUpper();
            int age = DateTime.Now.Year - txtDate.Value.Year;
            DateTime birth_date = txtDate.Value;
            string imgPathName = imgPath.Text;
            byte[] photo = null;
            string user_gender = "male";

            switch (txtGender.SelectedIndex)
            {
                case 0:
                    user_gender = "male";
                    break;

                case 1:
                    user_gender = "female";
                    break;
                case 2:
                    user_gender = "other";
                    break;
            }

            //Validates
            if (!validateFields())
            {
                return;
            }
            //Check if client exists before
            if (checkClientExists())
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Client already exists", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Cliente já existe", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            //Check if user put any image
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

            //Path DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";

            //Connection string
            string strConection = @"Data Source = " + pathDB + "; Version = 3";

            //Instance of Connection
            SQLiteConnection connection = new SQLiteConnection(strConection);

            try
            {
                string query = @"
                    INSERT INTO clients (full_name, email, age, birthdate, gender, photo)
                    VALUES (@name, @email, @age, @birthdate, @gender, @photo);
                    ";



                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {

                    connection.Open();

                    // Adicionando parâmetros
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@birthdate", birth_date);
                    cmd.Parameters.AddWithValue("@gender", user_gender);
                    cmd.Parameters.AddWithValue("@photo", photo);

                    // Executando o comando
                    cmd.ExecuteNonQuery();

                    if (GlobalVariables.Language == "en")
                    {
                        MessageBox.Show("New Client Created", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Novo Cliente criado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            this.Close();

        }

        //Logicals, Validates, Checks if Exists, And Clear Images, and Handle If IMages

        private bool validateFields()
        {

            //Empty Fields
            if (txtFullName.Text == "" || txtEmail.Text == "")
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Have some empty fields", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    MessageBox.Show("Há Campos Vazios", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            DateTime birth_date = txtDate.Value;
            DateTime actualDate = DateTime.Now.Date;
            int age = actualDate.Year - birth_date.Year;

            if (birth_date == actualDate)
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Invalid Date", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    MessageBox.Show("Data invalida", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (age == 0 || age < 18 || age > 130)
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Client must be 18 years old", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    MessageBox.Show("Cliente deverá possuir 18 anos", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            else if (age < 18)
            {
                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Birth Date invalid, Client must be over 17 year old", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    MessageBox.Show("Data de nascimento inválida, Cliente deve possuir idade maior que 17 anos", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private bool checkClientExists()
        {
            string user = txtFullName.Text.ToUpper();
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
                    FROM clients
                    WHERE full_name = @user
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

        private void btnClearIMG_Click(object sender, EventArgs e)
        {
            imgPath.Text = "imgPath";
            pictureBox.Image = null;
        }
    }
}
