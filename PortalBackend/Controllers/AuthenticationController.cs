using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PortalBackend.Data;
using PortalBackend.Objects.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public AuthenticationController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("/register")]
        public ActionResult<object> Register(UserCredentials getuser)
        {
            CreatePasswordHash(getuser.Password, out byte[] passwordHash, out byte[] passwordsalt);
            RefreshToken refresh = GenerateRefreshToken();
            var user = new UserDTO();
            user.UserName = getuser.UserName;
            user.First_Name = getuser.First_Name;
            user.Last_Name = getuser.Last_Name;
            user.Email = getuser.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordsalt;
            user.Department = getuser.Department;
            user.StudentId = getuser.StudentId;
            user.RefreshToken = GenerateRefreshToken().Token;

            this.context.Users.Add(user);

            this.context.SaveChanges();

            var _User = this.context.Users.Where(a => a.UserName == user.UserName).ToList();


            Dictionary<string, string> token = new Dictionary<string, string>();
            token.Add("token", CreateToken(user));
            token.Add("refresh_token", CreateRefreshToken(user));

            return Ok(token);
        }

        [HttpPost("/login")]
        public ActionResult Login(UserCredUsedToAuth credentials)
        {
            try
            {
                var User = this.context.Users.Where(a => a.UserName == credentials.Username).ToList();

                if (VerifyPasswordHash(credentials.Password, User[0].PasswordHash, User[0].PasswordSalt))
                {
                    Dictionary<string, string> token = new Dictionary<string, string>();

                    token.Add("token", CreateToken(User[0]));
                    token.Add("refresh_token", CreateRefreshToken(User[0]));

                    return Ok(token);
                }
                return BadRequest("Wrong Password");
            }
            catch
            {
                return BadRequest("UserNotFound");
            }


        }


        [HttpPost("/verifyToken")]

        public ActionResult SendClaims(string token)
        {

            return Ok(VerifyToken(token));

        }

        [HttpPost("/get-new-token")]
        public ActionResult GetNewToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtSecurityToken = handler.ReadJwtToken(token).Payload.Values;
                var tokenContentItems = new List<string>();
                foreach (var item in jwtSecurityToken)
                {
                    tokenContentItems.Add(item.ToString());

                }

                var _user = this.context.Users.Where(a => a.RefreshToken == tokenContentItems[1]).ToList()[0];
                if (_user.RefreshToken != tokenContentItems[1]) return Unauthorized("err : tokenmatcherror");

                Dictionary<string, string> tokens = new Dictionary<string, string>();

                RefreshToken newRefresh = new RefreshToken();
                _user.RefreshToken = newRefresh.Token;
                _user.RefreshTokenExpires = newRefresh.ExpiresIn;

                this.context.SaveChanges();
                tokens.Add("token", CreateToken(_user));
                tokens.Add("refresh_token", CreateRefreshToken(_user));

                return Ok(tokens);
            }
            catch
            {
                return BadRequest("TokenErr");
            }

        }

        [HttpDelete("/test-delete"), Authorize(Roles = "Neexiyar")]
        public ActionResult Delete()
        {
            this.context.Users.RemoveRange(this.context.Users);
            this.context.SaveChanges();
            return Ok(this.context.Users);
        }
        private string CreateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role , user.UserName),
                new Claim("displayName", user.First_Name + " " + user.Last_Name),
                new Claim("first_name", user.First_Name),
                new Claim("last_name" , user.Last_Name),
                new Claim("username" , user.UserName),
                new Claim("email" , user.Email),
                new Claim("ProfilePicture" , user.ProfilePicture
                )


            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                this.configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(15),
                    signingCredentials: cred
                );
            var returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

        private string CreateRefreshToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {

                new Claim("username" , user.UserName),
                new Claim("TokenRefresh" , user.RefreshToken)

            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                this.configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(15),
                    signingCredentials: cred
                );
            var returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

        private ActionResult VerifyToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtSecurityToken = handler.ReadJwtToken(token);
                return Ok(jwtSecurityToken.Payload);
            }
            catch
            {
                return BadRequest("TokenErr");
            }



        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresIn = DateTime.Now.AddDays(2),
                GeneratedIn = DateTime.Now
            };

            return refreshToken;
        }

        protected void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {

            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
