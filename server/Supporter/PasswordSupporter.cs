using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace server.Auth {
    public class MyPasswordEncryptor{
        private IConfiguration _config {get;}
        public MyPasswordEncryptor(IConfiguration configuration){
            _config = configuration;
        }
        public string EncryptPassword(string password){
            string res = "";
            string encryptionKey = _config["SecretKey:PasswordEncryptKey"];

            byte[] toBytePassword = Encoding.Unicode.GetBytes(password);
            byte[] salt = Encoding.Unicode.GetBytes(_config["SecretKey:PasswordEncryptStringSalt"]);
            using (Aes encryptor = Aes.Create()){
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream()){
                    using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)){
                        cs.Write(toBytePassword, 0, toBytePassword.Length);
                        cs.Close();
                    }
                    res = Convert.ToBase64String(ms.ToArray());
                }
            }
            return res;
        }
        public string DecryptPassword(string encrypted){
            string res = "";
            string encryptionKey = _config["SecretKey:PasswordEncryptKey"];
            byte[] toByteEncrypted = Convert.FromBase64String(encrypted);
            byte[] salt = Encoding.Unicode.GetBytes(_config["SecretKey:PasswordEncryptStringSalt"]);
            using (Aes decryptor = Aes.Create()){
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, salt);
                decryptor.Key = pdb.GetBytes(32);
                decryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream()){
                    using(CryptoStream cs = new CryptoStream(ms, decryptor.CreateDecryptor(), CryptoStreamMode.Write)){
                        cs.Write(toByteEncrypted, 0, toByteEncrypted.Length);
                        cs.Close();
                    }
                    res = Encoding.Unicode.GetString(ms.ToArray());
                    
                }
            }
            return res;
        }
    }
}