using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestDevBackJr
{
    public partial class Login : System.Web.UI.Page
    {
        private static readonly string encryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
        private static readonly string ivKey = ConfigurationManager.AppSettings["IvKey"];

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            string username = login__username.Value;
            string password = login__password.Value;

            if (ValidateUser(username, password))
            {
                Response.Redirect("Index.aspx");
            }
            else
            {
                Response.Write("Login fallido");
            }
        }

        private bool ValidateUser(string username, string password)
        {
            bool Validacion = false;
            string encryptedUsername = Encrypt(password);

            using (var objLogIn = new libLogIn.rnLogIn() { Usuario = username, Contraseña = encryptedUsername })
            {
                objLogIn.ListarDatos();

                if (objLogIn.objError.bError == true)
                {
                    Validacion = false;
                }
                else
                {
                    if (objLogIn.Propiedades != null)
                    {
                        if (password == Decrypt(objLogIn.Propiedades["Contraseña"].ToString()))
                        {
                            Validacion = true;
                        }
                    }
                }
            }

            return Validacion;
        }

        public static string Encrypt(string plainText)
        {
            byte[] keyBytes = GetValidKey(encryptionKey, 16);
            byte[] ivBytes = GetValidKey(ivKey, 16);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (!IsBase64String(cipherText))
            {
                throw new FormatException("El texto proporcionado no está en formato Base64.");
            }

            byte[] keyBytes = GetValidKey(encryptionKey, 16);
            byte[] ivBytes = GetValidKey(ivKey, 16);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        private static bool IsBase64String(string base64)
        {
            if (string.IsNullOrEmpty(base64) || base64.Length % 4 != 0)
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static byte[] GetValidKey(string key, int size)
        {
            byte[] keyBytes = new byte[size];
            byte[] tempBytes = Encoding.UTF8.GetBytes(key);

            Array.Copy(tempBytes, keyBytes, Math.Min(tempBytes.Length, keyBytes.Length));
            return keyBytes;
        }

    }
}