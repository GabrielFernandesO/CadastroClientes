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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace CadastroClientes
{
    public partial class searchUserForm : Form
    {
        //Global Variables and new Form to be created
        private byte[] selectedUserImage;
        private string userUpdated;
        private resetPassColabForm resetPassColabForm;

        public searchUserForm()
        {
            InitializeComponent();

            //Pre Visible and Enables states
            btnClearIMG.Visible = false;
            imgPath.Visible = false;
            txtNameUser.Enabled = false;
            txtUser.Enabled = false;
            txtEmailUser.Enabled = false;
            access_Level.Enabled = false;
            btnSelectIMG.Enabled = false;
            btnEditUser.Enabled = false;
            btnDeleteUser.Enabled = false;
            btnUpdate.Enabled = false;

            //Define OPtions to ComboBox
            access_Level.Items.Add("Nível de acesso 0");
            access_Level.Items.Add("Nível de acesso 1");

            //Handle Languages
            if (GlobalVariables.Language == "en")
            {
                btnSearchUser.Text = "Search";
                label1.Text = "Colaborator Name";
                label7.Text = "List of Colaborators";
                ColaboratorList.Columns[0].Text = "Full Name";
                ColaboratorList.Columns[1].Text = "User";
                ColaboratorList.Columns[2].Text = "Email";
                ColaboratorList.Columns[3].Text = "Access Level";
                btnSelectIMG.Text = "Select Image";
                label2.Text = "Full Name";
                label4.Text = "Email";
                label3.Text = "User";
                label6.Text = "Acces Level";
                btnUpdate.Text = "Update";
                btnDeleteUser.Text = "Delete";
                btnEditUser.Text = "Edit";
                btnClearIMG.Text = "Remove Image";
                linkLabel1.Text = "Do you need redefine your password? Click here!";
                btnSelectUser.Text = "Select";
                access_Level.Text = "Access Level";
                access_Level.Items[0] = "Access Level 0";
                access_Level.Items[1] = "Access Level 1";
                this.Text = "Search Colaborator";
            }
            else
            {
                btnSearchUser.Text = "Procurar";
                label1.Text = "Nome do Colaborador";
                label7.Text = "Lista de Colaboradores";
                ColaboratorList.Columns[0].Text = "Nome Completo";
                ColaboratorList.Columns[1].Text = "Usuário";
                ColaboratorList.Columns[2].Text = "Email";
                ColaboratorList.Columns[3].Text = "Nível de Acesso";
                btnSelectIMG.Text = "Selecionar Imagem";
                label2.Text = "Nome Completo";
                label4.Text = "Email";
                label3.Text = "Usuário";
                label6.Text = "Nível de Acesso";
                btnUpdate.Text = "Atualizar";
                btnDeleteUser.Text = "Deletar";
                btnEditUser.Text = "Editar";
                btnClearIMG.Text = "Remover Imagem";
                linkLabel1.Text = "Precisa redefinir sua senha? Clique Aqui!";
                btnSelectUser.Text = "Selecionar";
                access_Level.Text = "Nível de Acesso";
                access_Level.Items[0] = "Nível de Acesso 0";
                access_Level.Items[1] = "Nível de Acesso 1";
                this.Text = "Procurar Colaborador";
            }

            //Lock and Protect width colums of listview
            ColaboratorList.ColumnWidthChanging += new ColumnWidthChangingEventHandler(listView1_ColumnWidthChanging);
        }

        //SearchColaborator
        private void btnSearchUser_Click(object sender, EventArgs e)
        {

            btnClearIMG.Visible = false;
            imgPath.Visible = false;
            txtNameUser.Enabled = false;
            txtUser.Enabled = false;
            txtEmailUser.Enabled = false;
            access_Level.Enabled = false;
            btnSelectIMG.Enabled = false;
            btnEditUser.Enabled = false;
            btnUpdate.Enabled = false;
            btnSelectIMG.Enabled = false;
            btnDeleteUser.Enabled = false;

            ColaboratorList.Items.Clear();

            txtNameUser.Text = "";
            txtUser.Text = "";
            txtEmailUser.Text = "";
            access_Level.Text = null;
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
                    SELECT name, user, email, access_level, photo 
                    FROM colaborators
                    WHERE name like @colaboratorUser
                       OR email like @colaboratorUser
                       OR user like @colaboratorUser
                    ";

            try
            {
                //Finding all data of DB to insert in ListView
                using (SQLiteConnection connection = new SQLiteConnection(strConnection))
                {
                    connection.Open();


                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@colaboratorUser", "%" + userForQuery + "%");
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            //While Reading, add to a ListView with a new item
                            while (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string user = reader["user"].ToString();
                                string email = reader["email"].ToString();
                                string access_level = reader["access_level"].ToString();

                                if (reader["photo"].ToString() != "")
                                {
                                    selectedUserImage = (byte[])reader["photo"];
                                }

                                ListViewItem item = new ListViewItem(name);
                                item.SubItems.Add(user);
                                item.SubItems.Add(email);
                                item.SubItems.Add(access_level);

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

        //SelectIMG
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
        //Select Colaborator to modify
        private void btnSelectUser_Click(object sender, EventArgs e)
        {
            userUpdated = null;


            if (ColaboratorList.SelectedItems.Count > 0)
            {
                btnEditUser.Enabled = true;
                ListViewItem selectedUser = ColaboratorList.SelectedItems[0];

                string name = selectedUser.SubItems[0].Text; // Coluna 1
                string user = selectedUser.SubItems[1].Text; // Coluna 2
                string email = selectedUser.SubItems[2].Text; // Coluna 3
                string accessLevel = selectedUser.SubItems[3].Text;

                txtNameUser.Text = name;
                txtEmailUser.Text = email;
                txtUser.Text = user;

                if (accessLevel == "0")
                {
                    access_Level.SelectedIndex = 0;
                }
                else
                {
                    access_Level.SelectedIndex = 1;
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

        //If btnEdit clicked, its posible to change all below
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            imgPath.Visible = true;
            txtNameUser.Enabled = true;
            txtUser.Enabled = true;
            txtEmailUser.Enabled = true;
            access_Level.Enabled = true;
            btnSelectIMG.Enabled = true;
            btnDeleteUser.Enabled = true;
            btnUpdate.Enabled = true;
            btnClearIMG.Visible = true;
        }

        //Call back two click events
        private void userEdited()
        {
            btnSearchUser_Click(btnSearchUser, EventArgs.Empty);
            btnSelectUser_Click(btnSelectUser, EventArgs.Empty);
        }

        //Update Colaborator
        private void btnUpdate_Click(object sender, EventArgs e)
        {


            DialogResult result;

            if (GlobalVariables.Language == "en")
            {
                result = MessageBox.Show("Would like to UPDATE this Colaborator?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                result = MessageBox.Show("Deseja ATUALIZAR este Colaborador?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }


            if (result == DialogResult.Yes)
            {
                imgPath.Visible = false;
                txtNameUser.Enabled = false;
                txtUser.Enabled = false;
                txtEmailUser.Enabled = false;
                access_Level.Enabled = false;
                btnSelectIMG.Enabled = false;
                txtSearchUser.Text = "";
                btnClearIMG.Visible= false;

                ListViewItem selectedUser = ColaboratorList.SelectedItems[0];

                string userForEdit = selectedUser.SubItems[1].Text;

                string name = txtNameUser.Text.ToUpper();
                string email = txtEmailUser.Text.ToUpper();
                string user = txtUser.Text.ToUpper();
                byte[] photo = null;
                int user_acces_Level = access_Level.SelectedIndex;

                string query = @"
                UPDATE colaborators
                SET name = @name,
                    email = @email,
                    user = @user,
                    access_level = @access_level
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

                query += " WHERE user = @UserEdit";

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
                            cmd.Parameters.AddWithValue("@user", user);
                            cmd.Parameters.AddWithValue("@access_level", user_acces_Level);

                            if (photo != null)
                            {
                                cmd.Parameters.AddWithValue("@photo", photo);
                            }
                            
                            cmd.Parameters.AddWithValue("@UserEdit", userForEdit);

                            cmd.ExecuteNonQuery();
                        }

                        userUpdated = user;
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

        //DeleteColaborator
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {

            DialogResult result;

            if (GlobalVariables.Language == "en")
            {
                result = MessageBox.Show("Would like to DELETE this Colaborator?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                result = MessageBox.Show("Deseja DELETAR este Colaborador?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (result == DialogResult.Yes)
            {
                imgPath.Visible = false;
                txtNameUser.Enabled = false;
                txtUser.Enabled = false;
                txtEmailUser.Enabled = false;
                access_Level.Enabled = false;
                btnSelectIMG.Enabled = false;
                txtSearchUser.Text = "";
                btnClearIMG.Visible = false;

                ListViewItem selectedUser = ColaboratorList.SelectedItems[0];

                string userForDelete = selectedUser.SubItems[1].Text;

                if (checkCurrentUser() == loginAuth.idCurrentUser)
                {
                    if(GlobalVariables.Language == "en")
                    {
                        MessageBox.Show("You can not delete your own account while is logged", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Você não pode deletar sua própria conta enquanto está logado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
                else
                {
                    string query = @"
                DELETE FROM colaborators
                WHERE user = @user
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
                                cmd.Parameters.AddWithValue("@user", userForDelete);
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
                        MessageBox.Show("Colaborator Deleted", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Colaborador Deletado", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    imgPath.Visible = false;
                    txtNameUser.Enabled = false;
                    txtUser.Enabled = false;
                    txtEmailUser.Enabled = false;
                    access_Level.Enabled = false;
                    btnSelectIMG.Enabled = false;
                    btnEditUser.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnSelectIMG.Enabled = false;
                    btnDeleteUser.Enabled = false;

                    ColaboratorList.Items.Clear();

                    txtNameUser.Text = "";
                    txtUser.Text = "";
                    txtEmailUser.Text = "";
                    access_Level.Text = null;
                    selectedUserImage = null;
                    pictureUser.Image = null;
                }
            }

        }

        //Clear IMG
        private void btnClearIMG_Click(object sender, EventArgs e)
        {
            pictureUser.Image = null;
            imgPath.Text = "imgPath";
        }

        //Protect and Lock Width columns
        void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = ColaboratorList.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        //Check if currentUser is trying delete own account
        private int checkCurrentUser()
        {

            ListViewItem selectedUser = ColaboratorList.SelectedItems[0];

            string userForDelete = selectedUser.SubItems[1].Text;
            int idUserDelete = -1;

            string query = @"
                SELECT id
                FROM colaborators
                WHERE user = @user
                ";


            //Connection with DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";
            string strConection = @"Data Source = " + pathDB + "; Version = 3";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(strConection))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user", userForDelete);
                        // Execute the command and read the results with SQLiteDataReader
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idUserDelete = Convert.ToInt32(reader["id"]);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return idUserDelete;
        }

        //New Form to handle with reset of password
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (resetPassColabForm == null || !resetPassColabForm.Visible)
            {
                resetPassColabForm = new resetPassColabForm();

                Thread resetPassForm = new Thread(() =>
                {
                    resetPassColabForm.FormClosed += (senderForm, eFormClosed) =>
                    {
                        // Quando a searchUserForm for fechada, ativar o formulário atual (Form1)
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Activate(); // Foca no formulário atual (Form1)
                        });
                    };
                    Application.Run(resetPassColabForm);
                });
                resetPassForm.Start();
            }
        }
    }
}
