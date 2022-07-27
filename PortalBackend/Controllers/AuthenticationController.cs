using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public ActionResult Register(UserCredentials getuser)
        {
            CreatePasswordHash(getuser.PasswordHash, out byte[] passwordHash, out byte[] passwordsalt);
            var user = new UserDTO();
            user.UserName = getuser.UserName;
            user.First_Name = getuser.First_Name;
            user.Last_Name = getuser.Last_Name;
            user.Email = getuser.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordsalt;

            this.context.Users.Add(user);

            this.context.SaveChanges();

            user.PasswordSalt = System.Text.Encoding.UTF8.GetBytes("");
            user.PasswordHash = System.Text.Encoding.UTF8.GetBytes("");
            return Ok(user);
        }

        [HttpPost("/login")]
        public ActionResult Login(UserCredUsedToAuth credentials)
        {
            try
            {
                var User = this.context.Users.Where(a => a.UserName == credentials.Username).ToList();

                if (VerifyPasswordHash(credentials.Password, User[0].PasswordHash, User[0].PasswordSalt))
                {

                    return Ok(CreateToken(User[0]));
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


        private string CreateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {

                new Claim("displayName", user.First_Name + " " + user.Last_Name),
                new Claim("first_name", user.First_Name),
                new Claim("last_name" , user.Last_Name),
                new Claim("username" , user.UserName),
                new Claim("email" , user.Email),
                new Claim("ProfilePicture" , user.ProfilePicture)


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


        private JwtPayload VerifyToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);


            return jwtSecurityToken.Payload;

        }
        
                private RefreshToken GenerateRefreshToken()
                {
                    var refreshToken = new RefreshToken
                    {
                        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                        ExpiresIn = DateTime.Now.AddDays(7),
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

