using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using server.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace server.Auth {
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
}