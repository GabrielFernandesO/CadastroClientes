using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Text;
using Microsoft.Win32;


namespace CadastroClientes
{

    public partial class cadastroForm : Form
    {
        //Controls Form
        private ListView colaboratorList;
        private ListView clientList;
        private Button btnUpdateListColab;
        private Button btnUpdateListClient;
        private Label titleListColab;
        private Label titleListClient;

        //Forms to Use
        private searchUserForm searchUser;
        private colaboradorForm colabForm;
        private clientForm clientForm;
        private searchClientForm searchClientForm;


        public cadastroForm()
        {
            InitializeComponent();

            //Event close of this Form
            this.FormClosing += CadastroForm_FormClosing;

            //Initialize Controls Components
            colaboratorList = new ListView();
            btnUpdateListColab = new Button();
            btnUpdateListClient = new Button();
            clientList = new ListView();
            titleListColab = new Label();
            titleListClient = new Label();

            this.MinimumSize = this.Size;

            //Define which navBar will be visible, depends the level of Access_Level
            if (loginAuth.acessLevel == 1)
            {
                menuUser.Visible = false;
                menuAdmin.Visible = true;

            }
            else
            {
                menuAdmin.Visible = false;
                menuUser.Visible = true;
            }

        }

        // Close all Application
        private void CadastroForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //Protect width columns of Listviews
        void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            //Block the resize width of ListView Columns
            e.NewWidth = colaboratorList.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        void listView2_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            //Block the resize width of ListView Columns
            e.NewWidth = clientList.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        //Lock size of This Form to max
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.MinimumSize = this.Size;
        }

        //Lock the posibility to max window and to move
        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDOWN = 0x00A1; 
            const int WM_NCLBUTTONDBLCLK = 0x00A3; 
            const int HTCAPTION = 0x0002; 

            
            if (m.Msg == WM_NCLBUTTONDOWN && (int)m.WParam == HTCAPTION)
            {
                return; 
            }

            if (m.Msg == WM_NCLBUTTONDBLCLK && (int)m.WParam == HTCAPTION)
            {
              
                if (this.WindowState == FormWindowState.Maximized)
                {
                 
                    return;
                }
            }

            base.WndProc(ref m);
        }

        //Initialize registerColaborator
        private void cadastrarColaboradorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colabForm == null || !colabForm.Visible)
            {
                colabForm = new colaboradorForm();

                Thread registerColabForm = new Thread(() =>
                {
                    //Se o form for fechado ele torna esse aqui atual como foco

                    colabForm.FormClosed += (senderForm, eFormClosed) =>
                    {
                        // Quando a searchUserForm for fechada, ativar o formulário atual (Form1)
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Activate(); // Foca no formulário atual (Form1)
                        });
                    };
                    Application.Run(colabForm);

                });

                //Possibilita que seja usado Dialogs mesmo não estando na form principal
                registerColabForm.SetApartmentState(ApartmentState.STA);
                registerColabForm.Start();
            }
        }
        //Initialize searchColaborator
        private void procurarToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (searchUser == null || !searchUser.Visible)
            {
                searchUser = new searchUserForm();

                Thread newThread = new Thread(() =>
                {
                    searchUser.FormClosed += (senderForm, eFormClosed) =>
                    {
                        // Quando a searchUserForm for fechada, ativar o formulário atual (Form1)
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Activate(); // Foca no formulário atual (Form1)
                        });
                    };

                    Application.Run(searchUser);
                });

                newThread.SetApartmentState(ApartmentState.STA);
                newThread.Start();
            }
        }
        //Initialize registerClient
        private void cadastrarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clientForm == null || !clientForm.Visible)
            {
                clientForm = new clientForm();

                Thread registerClientForm = new Thread(() =>
                {
                    //Se o form for fechado ele torna esse aqui atual como foco

                    clientForm.FormClosed += (senderForm, eFormClosed) =>
                    {
                        // Quando a searchUserForm for fechada, ativar o formulário atual (Form1)
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Activate(); // Foca no formulário atual (Form1)
                        });
                    };
                    Application.Run(clientForm);

                });

                //Possibilita que seja usado Dialogs mesmo não estando na form principal
                registerClientForm.SetApartmentState(ApartmentState.STA);
                registerClientForm.Start();
            }
        }
        //Initialize searchClient
        private void procurarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (searchClientForm == null || !searchClientForm.Visible)
            {
                searchClientForm = new searchClientForm();

                Thread newThread = new Thread(() =>
                {
                    searchClientForm.FormClosed += (senderForm, eFormClosed) =>
                    {
                        // Quando a searchUserForm for fechada, ativar o formulário atual (Form1)
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Activate(); // Foca no formulário atual (Form1)
                        });
                    };

                    Application.Run(searchClientForm);
                });

                newThread.SetApartmentState(ApartmentState.STA);
                newThread.Start();
            }
        }
        //Initialize registerClient
        private void cadastrarClienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (clientForm == null || !clientForm.Visible)
            {
                clientForm = new clientForm();

                Thread registerClientForm = new Thread(() =>
                {
                    //Se o form for fechado ele torna esse aqui atual como foco

                    clientForm.FormClosed += (senderForm, eFormClosed) =>
                    {
                        // Quando a searchUserForm for fechada, ativar o formulário atual (Form1)
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Activate(); // Foca no formulário atual (Form1)
                        });
                    };
                    Application.Run(clientForm);

                });

                //Possibilita que seja usado Dialogs mesmo não estando na form principal
                registerClientForm.SetApartmentState(ApartmentState.STA);
                registerClientForm.Start();
            }
        }
        //Initialize SearchClient
        private void procurarClienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (searchClientForm == null || !searchClientForm.Visible)
            {
                searchClientForm = new searchClientForm();

                Thread newThread = new Thread(() =>
                {
                    searchClientForm.FormClosed += (senderForm, eFormClosed) =>
                    {
                        // Quando a searchUserForm for fechada, ativar o formulário atual (Form1)
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Activate(); // Foca no formulário atual (Form1)
                        });
                    };

                    Application.Run(searchClientForm);
                });

                newThread.SetApartmentState(ApartmentState.STA);
                newThread.Start();
            }
        }



        //Create ListView of Colaborators
        private void colaboradoresAtivosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Controls.Contains(clientList))
            {
                Controls.Remove(clientList);
                Controls.Remove(btnUpdateListClient);
                Controls.Remove(titleListClient);
            }

            colaboratorList.Items.Clear();
            colaboratorList.Columns.Clear();



            //Config of ListView
            colaboratorList.Width = 1050;
            colaboratorList.Height = 550;
            colaboratorList.View = View.Details;
            colaboratorList.FullRowSelect = true;
            colaboratorList.HeaderStyle = ColumnHeaderStyle.Clickable;
            colaboratorList.ColumnWidthChanging += new ColumnWidthChangingEventHandler(listView1_ColumnWidthChanging);

            //Center Location ListView
            int x = (this.ClientSize.Width - colaboratorList.Width) / 2;
            int y = (this.ClientSize.Height - colaboratorList.Height) / 2;


            colaboratorList.Location = new Point(x, y);
            //Columns

            if (GlobalVariables.Language == "en")
            {
                colaboratorList.Columns.Add("Full Name", 382);
                colaboratorList.Columns.Add("Username", 184);
                colaboratorList.Columns.Add("Email", 382);
                colaboratorList.Columns.Add("Access Level", 102);
                btnUpdateListColab.Text = "Update List";
                titleListColab.Text = "Colaborators List";
            }
            else
            {
                colaboratorList.Columns.Add("Nome Completo", 382);
                colaboratorList.Columns.Add("Usuário", 184);
                colaboratorList.Columns.Add("Email", 382);
                colaboratorList.Columns.Add("Nível de Acesso", 102);
                btnUpdateListColab.Text = "Atualizar Lista";
                titleListColab.Text = "Lista de Colaboradores";
            }

            //Create Button


            btnUpdateListColab.Size = new Size(149, 38);

            btnUpdateListColab.Font = new Font("Arial", 12f, FontStyle.Bold);

            btnUpdateListColab.Click -= BtnUpdateList_Click;
            btnUpdateListColab.Click += BtnUpdateList_Click;


            int btnX = colaboratorList.Right - btnUpdateListColab.Width;
            int btnY = colaboratorList.Bottom - btnUpdateListColab.Height + 45;

            btnUpdateListColab.Location = new Point(btnX, btnY);

            titleListColab.Size = new Size(350, 38);
            titleListColab.Font = new Font("Arial", 14f, FontStyle.Bold);

            int lblX = colaboratorList.Left;
            int lblY = colaboratorList.Top - titleListColab.Height + 10;

            titleListColab.Location = new Point(lblX, lblY);

            //Add to FormControls about this current form
            this.Controls.Add(colaboratorList);
            this.Controls.Add(btnUpdateListColab);
            this.Controls.Add(titleListColab);


            //Path DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";
            string strConection = @"Data Source = " + pathDB + "; Version = 3";

            string query = @"
                    SELECT name, user, email, access_level 
                    FROM colaborators
                    ORDER BY name ASC
                    ";

            try
            {
                //Finding all data of DB to insert in ListView
                using (SQLiteConnection connection = new SQLiteConnection(strConection))
                {
                    connection.Open();


                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            //While Reading, add to a ListView with a new item
                            while (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string user = reader["user"].ToString();
                                string email = reader["email"].ToString();
                                string access_level = reader["access_level"].ToString();


                                ListViewItem item = new ListViewItem(name);

                                item.SubItems.Add(user);
                                item.SubItems.Add(email);
                                item.SubItems.Add(access_level);

                                colaboratorList.Items.Add(item);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //Toggle English Language
        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Verifica se as forms em questão estão abertas para que não seja possivel alterar até fechalas

            if (searchUser != null && !searchUser.IsDisposed || colabForm != null && !colabForm.IsDisposed || clientForm != null && !clientForm.IsDisposed || searchClientForm != null && !searchClientForm.IsDisposed)
            {
                MessageBox.Show("Close all tabs of register before", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                inglesToolStripMenuItem.Checked = true;
                portuguêsToolStripMenuItem.Checked = false;

                GlobalVariables.Language = "en";

                if (clientList.Columns.Count > 0)
                {
                    BtnUpdateList_Click2(this, EventArgs.Empty);
                }

                handleLanguage("en");
            }

        }

        //Toggle Portuguese Language
        private void portuguêsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // FecharSearchUserForm();

            if (searchUser != null && !searchUser.IsDisposed || colabForm != null && !colabForm.IsDisposed || clientForm !=null && !clientForm.IsDisposed || searchClientForm != null && !searchClientForm.IsDisposed)
            {
                MessageBox.Show("Feche todas as abas de cadastro antes", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                inglesToolStripMenuItem.Checked = false;
                portuguêsToolStripMenuItem.Checked = true;

                GlobalVariables.Language = "pt";

                if (clientList.Columns.Count > 0)
                {
                    BtnUpdateList_Click2(this, EventArgs.Empty);
                }


                handleLanguage("pt");
            }



        }

        //Logical to Handle with two languages
        private void handleLanguage(string currentLang)
        {
            if (currentLang == "en")
            {
                cadastrarToolStripMenuItem.Text = "Register";
                cadastrarClienteToolStripMenuItem.Text = "Client Registration";
                cadastrarColaboradorToolStripMenuItem.Text = "Colaborator Registration";

                clientesToolStripMenuItem.Text = "Clients";
                clientesAtivosToolStripMenuItem.Text = "Active Clients";

                colaboradoresToolStripMenuItem.Text = "Colaborators";
                colaboradoresAtivosToolStripMenuItem.Text = "Active Colaborators";
                procurarToolStripMenuItem.Text = "Search Colaborator";

                idiomaToolStripMenuItem.Text = "Language";
                portuguêsToolStripMenuItem.Text = "Portuguese";
                inglesToolStripMenuItem.Text = "English";

                procurarClienteToolStripMenuItem.Text = "Search Client";

                aboutToolStripMenuItem.Text = "About";
                aToolStripMenuItem1.Text = "Version 1.0";
                gabrielToolStripMenuItem.Text = "By Gabriel Fernandes";
                btnUpdateListColab.Text = "Update List";
                titleListColab.Text = "Colaborators List";
                btnUpdateListClient.Text = "Update List";
                titleListClient.Text = "Clients List";



                if (colaboratorList.Columns.Count > 0)
                {
                    colaboratorList.Columns[0].Text = "Full Name";
                    colaboratorList.Columns[1].Text = "Username";
                    colaboratorList.Columns[2].Text = "Email";
                    colaboratorList.Columns[3].Text = "Access Level";
                }

                if (clientList.Columns.Count > 0)
                {
                    clientList.Columns[0].Text = "Full Name";
                    clientList.Columns[1].Text = "Email";
                    clientList.Columns[2].Text = "Age";
                    clientList.Columns[3].Text = "Birth Date";
                    clientList.Columns[4].Text = "Gender";
                }


            }
            else
            {
                cadastrarToolStripMenuItem.Text = "Cadastrar";
                cadastrarClienteToolStripMenuItem.Text = "Cadastrar Cliente";
                cadastrarColaboradorToolStripMenuItem.Text = "Cadastrar Colaborador";

                clientesToolStripMenuItem.Text = "Clientes";
                clientesAtivosToolStripMenuItem.Text = "Clientes Ativos";

                colaboradoresToolStripMenuItem.Text = "Colaboradores";
                colaboradoresAtivosToolStripMenuItem.Text = "Colaboradores Ativos";
                procurarToolStripMenuItem.Text = "Procurar Colaborador";

                idiomaToolStripMenuItem.Text = "Idioma";
                portuguêsToolStripMenuItem.Text = "Português";
                inglesToolStripMenuItem.Text = "Inglês";

                procurarClienteToolStripMenuItem.Text = "Procurar Cliente";

                aboutToolStripMenuItem.Text = "Sobre";

                aToolStripMenuItem1.Text = "Versão 1.0";
                gabrielToolStripMenuItem.Text = "Por Gabriel Fernandes";
                btnUpdateListColab.Text = "Atualizar Lista";
                titleListColab.Text = "Lista de Colaboradores";
                btnUpdateListClient.Text = "Atualizar Lista";
                titleListClient.Text = "Lista de Clientes";


                if (colaboratorList.Columns.Count > 0)
                {
                    colaboratorList.Columns[0].Text = "Nome Completo";
                    colaboratorList.Columns[1].Text = "Usuário";
                    colaboratorList.Columns[2].Text = "Email";
                    colaboratorList.Columns[3].Text = "Nível de Acesso";
                }

                if (clientList.Columns.Count > 0)
                {
                    clientList.Columns[0].Text = "Nome Completo";
                    clientList.Columns[1].Text = "Email";
                    clientList.Columns[2].Text = "Idade";
                    clientList.Columns[3].Text = "Data de Nascimento";
                    clientList.Columns[4].Text = "Gênero";
                }
            }
        }


        //Create ListView of Clients
        private void clientesAtivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Controls.Contains(colaboratorList))
            {
                Controls.Remove(colaboratorList);
                Controls.Remove(btnUpdateListColab);
                Controls.Remove(titleListColab);
            }

            

            clientList.Items.Clear();
            clientList.Columns.Clear();



            //Config of ListView
            clientList.Width = 1050;
            clientList.Height = 550;
            clientList.View = View.Details;
            clientList.FullRowSelect = true;
            clientList.HeaderStyle = ColumnHeaderStyle.Clickable;
            clientList.ColumnWidthChanging += new ColumnWidthChangingEventHandler(listView2_ColumnWidthChanging);

            //Center Location ListView
            int x = (this.ClientSize.Width - clientList.Width) / 2;
            int y = (this.ClientSize.Height - clientList.Height) / 2;


            clientList.Location = new Point(x, y);
            //Columns

            if (GlobalVariables.Language == "en")
            {
                clientList.Columns.Add("Full Name", 283);
                clientList.Columns.Add("Email", 283);
                clientList.Columns.Add("Age", 100);
                clientList.Columns.Add("Birth Date", 283);
                clientList.Columns.Add("Gender", 100);
                btnUpdateListClient.Text = "Update List";
                titleListClient.Text = "Clients List";
            }
            else
            {
                clientList.Columns.Add("Nome Completo", 283);
                clientList.Columns.Add("Email", 283);
                clientList.Columns.Add("Idade", 100);
                clientList.Columns.Add("Data de Nascimento", 283);
                clientList.Columns.Add("Gênero", 100);
                btnUpdateListClient.Text = "Atualizar Lista";
                titleListClient.Text = "Lista de Clientes";
            }

            //Create Button


            // Configuração do botão
            btnUpdateListClient.Size = new Size(149, 38);

            btnUpdateListClient.Font = new Font("Arial", 12f, FontStyle.Bold);

            //Remove a instancia anterior ou seja, cancela ela para que não haja looping infinito se o usuario
            //Ficar clicando
            btnUpdateListClient.Click -= BtnUpdateList_Click2;
            btnUpdateListClient.Click += BtnUpdateList_Click2;

            int btnX = clientList.Right - btnUpdateListClient.Width;
            int btnY = clientList.Bottom - btnUpdateListClient.Height + 45;

            btnUpdateListClient.Location = new Point(btnX, btnY);

            titleListClient.Size = new Size(350, 38);
            titleListClient.Font = new Font("Arial", 14f, FontStyle.Bold);

            int lblX = clientList.Left;
            int lblY = clientList.Top - titleListClient.Height + 10;

            titleListClient.Location = new Point(lblX, lblY);

            //Add to FormControls about this current form
            this.Controls.Add(clientList);
            this.Controls.Add(btnUpdateListClient);
            this.Controls.Add(titleListClient);

            //Path DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";
            string strConection = @"Data Source = " + pathDB + "; Version = 3";

            string query = @"
                    SELECT full_name, email, age, birthdate, gender 
                    FROM clients
                    ORDER BY full_name ASC
                    ";

            try
            {
                //Finding all data of DB to insert in ListView
                using (SQLiteConnection connection = new SQLiteConnection(strConection))
                {
                    connection.Open();


                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            //While Reading, add to a ListView with a new item
                            while (reader.Read())
                            {
                                string full_name = reader["full_name"].ToString();
                                string email = reader["email"].ToString();
                                DateTime birthdate = Convert.ToDateTime(reader["birthdate"]);
                                string formattedBirthdate = birthdate.ToString("dd/MM/yyyy");
                                string gender = reader["gender"].ToString();
                                string age = (DateTime.Now.Year  - birthdate.Year).ToString();
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
                                ListViewItem item = new ListViewItem(full_name);
                                item.SubItems.Add(email);
                                item.SubItems.Add(age);
                                item.SubItems.Add(formattedBirthdate);
                                item.SubItems.Add(displayList);

                                clientList.Items.Add(item);
                            }
                        }
                    }

                  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

 
        //Update Button ListView Colaborator
        private void BtnUpdateList_Click(object sender, EventArgs e)
        {
            colaboradoresAtivosToolStripMenuItem_Click(sender, e);
        }
        //Update Button ListView Client
        private void BtnUpdateList_Click2(object sender, EventArgs e)
        {
            clientesAtivosToolStripMenuItem_Click(sender, e);
        }



 

        //Same toolStrip Functions but in other accessLevel
        private void clientesAtivosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Controls.Contains(colaboratorList))
            {
                Controls.Remove(colaboratorList);
                Controls.Remove(btnUpdateListColab);
                Controls.Remove(titleListColab);
            }



            clientList.Items.Clear();
            clientList.Columns.Clear();



            //Config of ListView
            clientList.Width = 1050;
            clientList.Height = 550;
            clientList.View = View.Details;
            clientList.FullRowSelect = true;
            clientList.HeaderStyle = ColumnHeaderStyle.Clickable;
            clientList.ColumnWidthChanging += new ColumnWidthChangingEventHandler(listView2_ColumnWidthChanging);

            //Center Location ListView
            int x = (this.ClientSize.Width - clientList.Width) / 2;
            int y = (this.ClientSize.Height - clientList.Height) / 2;


            clientList.Location = new Point(x, y);
            //Columns

            if (GlobalVariables.Language == "en")
            {
                clientList.Columns.Add("Full Name", 283);
                clientList.Columns.Add("Email", 283);
                clientList.Columns.Add("Age", 100);
                clientList.Columns.Add("Birth Date", 283);
                clientList.Columns.Add("Gender", 100);
                btnUpdateListClient.Text = "Update List";
                titleListClient.Text = "Clients List";
            }
            else
            {
                clientList.Columns.Add("Nome Completo", 283);
                clientList.Columns.Add("Email", 283);
                clientList.Columns.Add("Idade", 100);
                clientList.Columns.Add("Data de Nascimento", 283);
                clientList.Columns.Add("Gênero", 100);
                btnUpdateListClient.Text = "Atualizar Lista";
                titleListClient.Text = "Lista de Clientes";
            }

            //Create Button


            // Configuração do botão
            btnUpdateListClient.Size = new Size(149, 38);

            btnUpdateListClient.Font = new Font("Arial", 12f, FontStyle.Bold);

            //Remove a instancia anterior ou seja, cancela ela para que não haja looping infinito se o usuario
            //Ficar clicando
            btnUpdateListClient.Click -= BtnUpdateList_Click2;
            btnUpdateListClient.Click += BtnUpdateList_Click2;

            int btnX = clientList.Right - btnUpdateListClient.Width;
            int btnY = clientList.Bottom - btnUpdateListClient.Height + 45;

            btnUpdateListClient.Location = new Point(btnX, btnY);

            titleListClient.Size = new Size(350, 38);
            titleListClient.Font = new Font("Arial", 14f, FontStyle.Bold);

            int lblX = clientList.Left;
            int lblY = clientList.Top - titleListClient.Height + 10;

            titleListClient.Location = new Point(lblX, lblY);

            //Add to FormControls about this current form
            this.Controls.Add(clientList);
            this.Controls.Add(btnUpdateListClient);
            this.Controls.Add(titleListClient);

            //Path DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";
            string strConection = @"Data Source = " + pathDB + "; Version = 3";

            string query = @"
                    SELECT full_name, email, age, birthdate, gender 
                    FROM clients
                    ORDER BY full_name ASC
                    ";

            try
            {
                //Finding all data of DB to insert in ListView
                using (SQLiteConnection connection = new SQLiteConnection(strConection))
                {
                    connection.Open();


                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            //While Reading, add to a ListView with a new item
                            while (reader.Read())
                            {
                                string full_name = reader["full_name"].ToString();
                                string email = reader["email"].ToString();
                                DateTime birthdate = Convert.ToDateTime(reader["birthdate"]);
                                string formattedBirthdate = birthdate.ToString("dd/MM/yyyy");
                                string gender = reader["gender"].ToString();
                                string age = (DateTime.Now.Year - birthdate.Year).ToString();
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
                                ListViewItem item = new ListViewItem(full_name);
                                item.SubItems.Add(email);
                                item.SubItems.Add(age);
                                item.SubItems.Add(formattedBirthdate);
                                item.SubItems.Add(displayList);

                                clientList.Items.Add(item);
                            }
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void portuguêsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (searchUser != null && !searchUser.IsDisposed || colabForm != null && !colabForm.IsDisposed || clientForm != null && !clientForm.IsDisposed || searchClientForm != null && !searchClientForm.IsDisposed)
            {
                MessageBox.Show("Feche todas as abas de cadastro antes", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                inglesToolStripMenuItem.Checked = false;
                portuguêsToolStripMenuItem.Checked = true;

                GlobalVariables.Language = "pt";

                if (clientList.Columns.Count > 0)
                {
                    BtnUpdateList_Click2(this, EventArgs.Empty);
                }


                handleLanguagenoAdmin("pt");
            }


        }

        private void inglêsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Verifica se as forms em questão estão abertas para que não seja possivel alterar até fechalas

            if (searchUser != null && !searchUser.IsDisposed || colabForm != null && !colabForm.IsDisposed || clientForm != null && !clientForm.IsDisposed || searchClientForm != null && !searchClientForm.IsDisposed)
            {
                MessageBox.Show("Close all tabs of register before", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                inglesToolStripMenuItem.Checked = true;
                portuguêsToolStripMenuItem.Checked = false;

                GlobalVariables.Language = "en";

                if (clientList.Columns.Count > 0)
                {
                    BtnUpdateList_Click2(this, EventArgs.Empty);
                }

                handleLanguagenoAdmin("en");
            }

        }

        private void handleLanguagenoAdmin(string currentLanguage)
        {
            if (currentLanguage == "en")
            {
                cadastrarToolStripMenuItem1.Text = "Register";
                cadastrarClienteToolStripMenuItem1.Text = "Client Registration";

                clientesToolStripMenuItem1.Text = "Clients";
                clientesAtivosToolStripMenuItem1.Text = "Active Clients";
                procurarClienteToolStripMenuItem1.Text = "Search Client";


                idiomaToolStripMenuItem1.Text = "Language";
                portuguesToolStripMenuItem1.Text = "Portuguese";
                inglesToolStripMenuItem1.Text = "English";

                sobreToolStripMenuItem.Text = "About";
                versao10ToolStripMenuItem.Text = "Version 1.0";
                porGabrielFernandesToolStripMenuItem.Text = "By Gabriel Fernandes";
                btnUpdateListClient.Text = "Update List";
                titleListClient.Text = "Clients List";


                if (clientList.Columns.Count > 0)
                {
                    clientList.Columns[0].Text = "Full Name";
                    clientList.Columns[1].Text = "Email";
                    clientList.Columns[2].Text = "Age";
                    clientList.Columns[3].Text = "Birth Date";
                    clientList.Columns[4].Text = "Gender";
                }
            }
            else
            {
                cadastrarToolStripMenuItem1.Text = "Cadastrar";
                cadastrarClienteToolStripMenuItem1.Text = "Cadastrar Cliente";

                clientesToolStripMenuItem1.Text = "Clientes";
                clientesAtivosToolStripMenuItem1.Text = "Clientes Ativos";
                procurarClienteToolStripMenuItem1.Text = "Procurar Cliente";


                idiomaToolStripMenuItem1.Text = "Idioma";
                portuguesToolStripMenuItem1.Text = "Português";
                inglesToolStripMenuItem1.Text = "Inglês";

                sobreToolStripMenuItem.Text = "Sobre";
                versao10ToolStripMenuItem.Text = "Versão 1.0";
                porGabrielFernandesToolStripMenuItem.Text = "Por Gabriel Fernandes";
                btnUpdateListClient.Text = "Atualizar Lista";
                titleListClient.Text = "Lista de Clientes";


                if (clientList.Columns.Count > 0)
                {
                    clientList.Columns[0].Text = "Nome Completo";
                    clientList.Columns[1].Text = "Email";
                    clientList.Columns[2].Text = "Idade";
                    clientList.Columns[3].Text = "Data de Nascimento";
                    clientList.Columns[4].Text = "Gênero";
                }
            }
        }

 
    }
}
