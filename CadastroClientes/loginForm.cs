using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroClientes
{
    public partial class loginForm : Form
    {
        private bool passwordState = false;
        public loginForm()
        {
            InitializeComponent();

            //Defalut background BtnShowPass button
            btnShowPass.BackgroundImage = Image.FromFile(@"icons/closedEye.png");
            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;
        }


        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            //Define values props of Class LoginAuth
            loginAuth.user_Login = txtUserLogin.Text.ToUpper();
            loginAuth.user_Password = txtUserPassword.Text;

            //Waiting veirify
            if (loginAuth.VerifyLogin())
            {
                //Case true, will be created a new form of Cadastros
                cadastroForm cadastroForm = new cadastroForm();

                //New Thread to open a new Form
                Thread cadastroThread = new Thread(() =>
                {
                    Application.Run(cadastroForm);
                });
                //Turns the cadastroThread principal line to be able OpenDialog and others resources
                cadastroThread.SetApartmentState(ApartmentState.STA);
                cadastroThread.Start();

                //Close the current form

                this.Close();

            }
            else
            {
                MessageBox.Show("Invalid Credentials Try again", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Logical to change words visible in input of Password
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            passwordState = !passwordState;
            txtUserPassword.PasswordChar = passwordState ? '\0' : '*';

            if (passwordState)
            {
                btnShowPass.BackgroundImage = Image.FromFile(@"icons/openedEye.png");
            }
            else
            {
                btnShowPass.BackgroundImage = Image.FromFile(@"icons/closedEye.png");
            }

            btnShowPass.BackgroundImageLayout = ImageLayout.Zoom;
        }
    }
}
