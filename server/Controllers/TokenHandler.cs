using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using server.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security;
using System.IdentityModel;
using System.Security.Cryptography;
using System.IO;

namespace server.Controllers {
    public class MyTokenHandler {
        private IConfiguration _config {get;}
        public MyTokenHandler(IConfiguration configuration){
            _config = configuration;
        }
        public string GenerateToken(User user){
            var key = Encoding.ASCII.GetBytes(_config["SecretKey:QuizAppKey"]);
            var sercurityKey = new SymmetricSecurityKey(key);

            var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(){
                {"id", user.UID}
            };

            var secToken = new JwtSecurityToken(header, payload);
            var tokenHandler = new JwtSecurityTokenHandler();

            string tokenString = tokenHandler.WriteToken(secToken);
            return tokenString;
        }
        public string ValidateToken(string token){
            if(token == null){
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["SecretKey:QuizAppKey"]);
            var sercurityKey = new SymmetricSecurityKey(key);
            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters(){
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = sercurityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken) validatedToken;
                string id = jwtToken.Claims.First(x => x.Type == "id").Value.ToString();
                return id;
            } catch (Exception ex){
                return null;
            }
        }
    }
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