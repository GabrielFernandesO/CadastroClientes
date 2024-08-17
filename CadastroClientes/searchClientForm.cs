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
    public partial class searchClientForm : Form
    {
        //Global Variables
        private byte[] selectedUserImage;
        private string userUpdated;
        public searchClientForm()
        {
            InitializeComponent();

            //Pre visible and enables states
            btnClearIMG.Visible = false;
            imgPath.Visible = false;
            txtNameUser.Enabled = false;
            txtBirth.Enabled = false;
            txtEmailUser.Enabled = false;
            gender.Enabled = false;
            btnSelectIMG.Enabled = false;
            btnEditUser.Enabled = false;
            btnDeleteUser.Enabled = false;
            btnUpdate.Enabled = false;

            //Define limit of datetimerpick
            DateTime actualDate = DateTime.Now;
            txtBirth.Value = DateTime.Now;
            txtBirth.MaxDate = DateTime.Now;
            txtBirth.MinDate = actualDate.AddYears(-130);

            //Define options
            label6.Text = "Gender";
            gender.Items.Add("MALE");
            gender.Items.Add("FEMALE");
            gender.Items.Add("OTHER");


            //Handle Languages
            if (GlobalVariables.Language == "en")
            {
                btnSearchUser.Text = "Search";
                label1.Text = "Client Name";
                label7.Text = "List of Clients";
                ClientList.Columns[0].Text = "Email";
                ClientList.Columns[1].Text = "Full Name";
                ClientList.Columns[2].Text = "Birth Date";
                ClientList.Columns[3].Text = "Gender";
                btnSelectIMG.Text = "Select Image";
                label2.Text = "Full Name";
                label4.Text = "Email";
                label3.Text = "Birth Date";
                btnUpdate.Text = "Update";
                btnDeleteUser.Text = "Delete";
                btnEditUser.Text = "Edit";
                btnClearIMG.Text = "Remove Image";
                btnSelectUser.Text = "Select";
                this.Text = "Search Client";
            }
            else
            {
                btnSearchUser.Text = "Procurar";
                label1.Text = "Nome do Cliente";
                label7.Text = "Lista de Clientes";
                ClientList.Columns[0].Text = "Email";
                ClientList.Columns[1].Text = "Nome Completo";
                ClientList.Columns[2].Text = "Data de Nascimento";
                ClientList.Columns[3].Text = "Gênero";
                btnSelectIMG.Text = "Selecionar Imagem";
                label2.Text = "Nome Completo";
                label4.Text = "Email";
                label3.Text = "Data de Nascimento";
                label6.Text = "Gênero";
                gender.Items[0] = "MASCULINO";
                gender.Items[1] = "FEMININO";
                gender.Items[2] = "OUTRO";
                btnUpdate.Text = "Atualizar";
                btnDeleteUser.Text = "Deletar";
                btnEditUser.Text = "Editar";
                btnClearIMG.Text = "Remover Imagem";
                btnSelectUser.Text = "Selecionar";
                this.Text = "Procurar Cliente";
            }

            //Lock and protectd width of listview
            ClientList.ColumnWidthChanging += new ColumnWidthChangingEventHandler(listView1_ColumnWidthChanging);
        }

        //Lock and protectd width of listview
        void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = ClientList.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        //Search User
        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            btnClearIMG.Visible = false;
            imgPath.Visible = false;
            txtNameUser.Enabled = false;
            txtBirth.Enabled = false;
            txtEmailUser.Enabled = false;
            gender.Enabled = false;
            btnSelectIMG.Enabled = false;
            btnEditUser.Enabled = false;
            btnUpdate.Enabled = false;
            btnSelectIMG.Enabled = false;
            btnDeleteUser.Enabled = false;

            ClientList.Items.Clear();

            txtNameUser.Text = "";
            txtBirth.Text = "";
            txtEmailUser.Text = "";
            gender.Text = null;
            selectedUserImage = null;
            pictureUser.Image = null;

            string userForQuery;

            if (userUpdated == null)
            {
                userForQuery = txtSearchUser.Text.ToUpper();

            }
            else
            {
                userForQuery = userUpdated;
            }

            //Path DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";
            string strConnection = @"Data Source = " + pathDB + "; Version = 3";

            string query = @"
                    SELECT full_name, email, birthdate, gender, photo
                    FROM clients
                    WHERE full_name like @clientQuery
                       OR email like @clientQuery
                    ";

            try
            {
                //Finding all data of DB to insert in ListView
                using (SQLiteConnection connection = new SQLiteConnection(strConnection))
                {
                    connection.Open();


                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@clientQuery", "%" +  userForQuery + "%");
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            //While Reading, add to a ListView with a new item
                            while (reader.Read())
                            {
                                string name = reader["full_name"].ToString();
                                string email = reader["email"].ToString();
                                DateTime birthdate = Convert.ToDateTime(reader["birthdate"]);
                                string gender = reader["gender"].ToString();
                                string formattedBirthdate = birthdate.ToString("dd/MM/yyyy");
                                string displayList = string.Empty;


                                if (GlobalVariables.Language == "en")
                                {
                                    switch (gender.ToLower())
                                    {
                                        case "female":
                                            displayList = "FEMALE";
                                            break;
                                        case "male":
                                            displayList = "MALE";
                                            break;
                                        case "other":
                                            displayList = "OTHER";
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (gender.ToLower())
                                    {
                                        case "female":
                                            displayList = "FEMININO";
                                            break;
                                        case "male":
                                            displayList = "MASCULINO";
                                            break;
                                        case "other":
                                            displayList = "OUTRO";
                                            break;
                                    }
                                }

                                if (reader["photo"].ToString() != "")
                                {
                                    selectedUserImage = (byte[])reader["photo"];
                                }

                                ListViewItem item = new ListViewItem(email);
                                item.SubItems.Add(name);
                                item.SubItems.Add(formattedBirthdate);
                                item.SubItems.Add(displayList);

                                ClientList.Items.Add(item);


                                if (ClientList.SelectedItems.Count == 0)
                                {
                                    // Se não houver, seleciona automaticamente o primeiro item, se existir
                                    if (ClientList.Items.Count > 0)
                                    {
                                        ClientList.Items[0].Selected = true;
                                        ClientList.Focus(); // Foca na lista para indicar visualmente a seleção
                                    }
                                }

                            }
                        }
                    }
                }



                if (ClientList.Items.Count == 0)
                {
                    if (GlobalVariables.Language == "en")
                    {
                        MessageBox.Show("Client not found", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Cliente não encontrado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Select Image
        private void btnSelectIMG_Click(object sender, EventArgs e)
        {

            DialogResult result = openDialogIMG.ShowDialog();


            if (result == DialogResult.OK)
            {
                imgPath.Text = openDialogIMG.FileName;
                Image originalImage = Image.FromFile(imgPath.Text);
                pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;

                // Cálculo para redimensionar a imagem
                int pictureBoxWidth = pictureUser.Width;
                int pictureBoxHeight = pictureUser.Height;
                float scaleWidth = (float)pictureBoxWidth / (float)originalImage.Width;
                float scaleHeight = (float)pictureBoxHeight / (float)originalImage.Height;

                float scaleFactor = Math.Min(scaleWidth, scaleHeight);

                int newWidth = (int)(originalImage.Width * scaleFactor);
                int newHeight = (int)(originalImage.Height * scaleFactor);

                Image resizedImage = new Bitmap(originalImage, newWidth, newHeight);

                pictureUser.Image = resizedImage;
                imgPath.Visible = true;
            }
        }
        //Select User to modify
        private void btnSelectUser_Click(object sender, EventArgs e)
        {
            userUpdated = null;


            if (ClientList.SelectedItems.Count > 0)
            {
                btnEditUser.Enabled = true;
                ListViewItem selectedUser = ClientList.SelectedItems[0];

                string email = selectedUser.SubItems[0].Text; // Coluna 1
                string name = selectedUser.SubItems[1].Text; // Coluna 2
                string birth_date = selectedUser.SubItems[2].Text; // Coluna 3
                string genderClient = selectedUser.SubItems[3].Text;

                txtNameUser.Text = name;
                txtEmailUser.Text = email;
                txtBirth.Text = birth_date;


                switch (genderClient.ToLower())
                {
                    case "female":
                        gender.SelectedIndex = 1;
                        break;
                    case "male":
                        gender.SelectedIndex = 0;
                        break;
                    case "other":
                        gender.SelectedIndex = 2;
                        break;
                }

                switch (genderClient.ToLower())
                {
                    case "feminino":
                        gender.SelectedIndex = 1;
                        break;
                    case "masculino":
                        gender.SelectedIndex = 0;
                        break;
                    case "outro":
                        gender.SelectedIndex = 2;
                        break;
                }


                Image imagem = null;

                if (selectedUserImage == null)
                {
                    return;
                }

                using (MemoryStream ms = new MemoryStream(selectedUserImage))
                {
                    imagem = Image.FromStream(ms);

                    //Calculos para dimensoes com o picture box
                    int pictureBoxWidth = pictureUser.Width;
                    int pictureBoxHeight = pictureUser.Height;
                    float scaleWidth = (float)pictureBoxWidth / (float)imagem.Width;
                    float scaleHeight = (float)pictureBoxHeight / (float)imagem.Height;

                    float scaleFactor = Math.Min(scaleWidth, scaleHeight);

                    int newWidth = (int)(imagem.Width * scaleFactor);
                    int newHeight = (int)(imagem.Height * scaleFactor);

                    Image resizedImage = new Bitmap(imagem, newWidth, newHeight);
                    pictureUser.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureUser.Image = resizedImage;
                }

            }

        }

        //If button edit was clicked, its be able to modify all fields below
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            imgPath.Visible = true;
            txtNameUser.Enabled = true;
            txtBirth.Enabled = true;
            txtEmailUser.Enabled = true;
            gender.Enabled = true;
            btnSelectIMG.Enabled = true;
            btnDeleteUser.Enabled = true;
            btnUpdate.Enabled = true;
            btnClearIMG.Visible = true;
        }

        //IF user was Edited, call back this two clicks events
        private void userEdited()
        {
            btnSearchUser_Click(btnSearchUser, EventArgs.Empty);
            btnSelectUser_Click(btnSelectUser, EventArgs.Empty);
        }

        //Update user
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result;

            if (GlobalVariables.Language == "en")
            {
                result = MessageBox.Show("Would like to UPDATE this Client?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                result = MessageBox.Show("Deseja ATUALIZAR este Cliente?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }


            if (result == DialogResult.Yes)
            {
                imgPath.Visible = false;
                txtNameUser.Enabled = false;
                txtBirth.Enabled = false;
                txtEmailUser.Enabled = false;
                gender.Enabled = false;
                btnSelectIMG.Enabled = false;
                btnClearIMG.Visible = false;
                txtSearchUser.Text = "";

                ListViewItem selectedUser = ClientList.SelectedItems[0];

                string userForEdit = selectedUser.SubItems[0].Text;

                string name = txtNameUser.Text.ToUpper();
                string email = txtEmailUser.Text.ToUpper();
                DateTime birth_date = txtBirth.Value;
                byte[] photo = null;
                string genderClient = string.Empty;

                switch (gender.SelectedIndex)
                {
                    case 0:
                        genderClient = "male";
                        break;
                    case 1:
                        genderClient = "female";
                        break;
                    case 2:
                        genderClient = "other";
                        break;
                }

                string query = @"
                UPDATE clients
                SET full_name = @name,
                    email = @email,
                    birthdate = @birth_date,
                    gender = @genderClient
                ";

                if (string.IsNullOrEmpty(imgPath.Text) || imgPath.Text == "imgPath")
                {
                    // Definindo o campo photo como NULL
                    query += ", photo = NULL";
                }
                else
                {
                    string imgpathName = imgPath.Text;

                    // Converte a imagem em Binário
                    try
                    {
                        photo = File.ReadAllBytes(imgpathName);
                        query += ", photo = @photo";
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Erro ao ler a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                query += " WHERE email = @clientEmail";

                //Connection with DB
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
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@birth_date", birth_date);
                            cmd.Parameters.AddWithValue("@genderClient", genderClient);

                            if (photo != null)
                            {
                                cmd.Parameters.AddWithValue("@photo", photo);
                            }
                            cmd.Parameters.AddWithValue("@clientEmail", userForEdit);

                            cmd.ExecuteNonQuery();
                        }

                        userUpdated = email;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                imgPath.Text = "imgPath";

                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("User Updated", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuário Atualizado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                userEdited();
            }
        }

        //Clear IMG
        private void btnClearIMG_Click(object sender, EventArgs e)
        {
            pictureUser.Image = null;
            imgPath.Text = "imgPath";
        }

        //Delete User
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            DialogResult result;

            if (GlobalVariables.Language == "en")
            {
                result = MessageBox.Show("Would like to DELETE this Client?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                result = MessageBox.Show("Deseja DELETAR este Cliente?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (result == DialogResult.Yes)
            {
                imgPath.Visible = false;
                txtNameUser.Enabled = false;
                txtBirth.Enabled = false;
                txtEmailUser.Enabled = false;
                gender.Enabled = false;
                btnSelectIMG.Enabled = false;
                txtSearchUser.Text = "";
                btnClearIMG.Visible = false;

                ListViewItem selectedUser = ClientList.SelectedItems[0];

                string userForDelete = selectedUser.SubItems[0].Text;

                string query = @"
                        DELETE FROM clients
                        WHERE email = @emailClient
                        ";


                //Connection with DB
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
                            cmd.Parameters.AddWithValue("@emailClient", userForDelete);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (GlobalVariables.Language == "en")
                {
                    MessageBox.Show("Client Deleted", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cliente Deletado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                imgPath.Visible = false;
                txtNameUser.Enabled = false;
                txtBirth.Enabled = false;
                txtEmailUser.Enabled = false;
                gender.Enabled = false;
                btnSelectIMG.Enabled = false;
                btnEditUser.Enabled = false;
                btnUpdate.Enabled = false;
                btnSelectIMG.Enabled = false;
                btnDeleteUser.Enabled = false;

                ClientList.Items.Clear();

                txtNameUser.Text = "";
                txtBirth.Text = "";
                txtEmailUser.Text = "";
                gender.Text = null;
                selectedUserImage = null;
                pictureUser.Image = null;
            }
        }
    }
}
