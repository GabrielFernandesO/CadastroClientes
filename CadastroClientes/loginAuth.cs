using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.ComponentModel;


namespace CadastroClientes
{
    static internal class loginAuth
    {
        //Public Variables Props
        static public string user_Login;
        static public string user_Password;
        
        //Hash db Variable Prop
        static  string Pass_hash_db { get;  set; }

        //Global Variables Props
        public static bool userLogged { get; private set; } = false;
        public static int acessLevel { get; private set; }
        public static int idCurrentUser {  get; private set; }

        //Public function to use the private method about handle login
        static public bool VerifyLogin()
        {
            return AuthLogin(user_Login, user_Password);
        }

        
        //Handle login with SQLite
        static private bool AuthLogin(string username, string password)
        {

            //Path DB
            string pathDB = Application.StartupPath + @"\db\DBSQLite.db";

            //Connection string
            string strConection = @"Data Source = " + pathDB + "; Version = 3";

            try
            {
                
                //Query to find user
                string query = @"
                    SELECT id, password_hash, access_level 
                    FROM colaborators
                    WHERE user = @user
                    ";

                //using to close and dispose queries more easy

                using (SQLiteConnection connection = new SQLiteConnection(strConection))
                {
                    connection.Open();

                    // initializing connection with query
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        // Agregate parameters to @user
                        command.Parameters.AddWithValue("@user", username);

                        // Execute the command and read the results with SQLiteDataReader
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            //Execute command and read at the same time
                            if (reader.Read())
                            {
                                //Convert Hash of DB to String
                                Pass_hash_db = reader["password_hash"].ToString();
                                //Convert to INT 32 the access level of DB
                                acessLevel = Convert.ToInt32(reader["access_level"]);
                                idCurrentUser = Convert.ToInt32(reader["id"]);
                            }
                            else
                            {
                                //MessageBox.Show("Usuario inexistente");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro" + ex.Message);
            }

            //Class passordHash to compare hash from DB and Sent of user
            passwordHash comparePassword = new passwordHash();

            if (comparePassword.CompararMD5(password, Pass_hash_db))
            {
                //Set global variable be able to login in
                userLogged = true;
                return userLogged;
            }
            else
            {
                return false;
            }


        }
    }
    }
