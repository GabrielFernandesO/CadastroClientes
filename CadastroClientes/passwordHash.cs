using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CadastroClientes
{
    internal class passwordHash
    {
        //Public Function to Access the private function with logical of returns MD5
        public string RetornarMD5(string Senha)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return RetornarHash(md5Hash, Senha);
            }
        }
        //Public CompareMD5 Function to Access Private function with logical to compare two passwords hash
        public bool CompararMD5(string senhaEntrada, string senhaMD5)
        {
            string senha = RetornarMD5(senhaEntrada);
            if (VerificarHash(senhaMD5, senha))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Private Function that return a pass with hash
        private string RetornarHash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            return sBuilder.ToString();
        }
        //Private bool function that return true or false if two pass are equal or not
        private bool VerificarHash(string input, string hash)
        {
            StringComparer comparar = StringComparer.OrdinalIgnoreCase;
            if (comparar.Compare(input, hash) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
