using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestePMWEB.DTOs;

namespace TestePMWEB.Controllers
{
    public interface IUserService
    {
        Task<ActionResult> Login(UsuarioDTO userInfo);
    }

    [AllowAnonymous]
    [Route("api/[Controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase, IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em: " + DateTime.Now.ToLongDateString();
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromForm] UsuarioDTO model)
        {
            if (model.ConfirmPassword != model.Password)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Dados inválidos para registro");
            }
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);

            //return Ok(GeraToken(model));
            return new RedirectToActionResult("Index", "Upload", null);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] UsuarioDTO userInfo)
         {
            //verifica se o modelo é válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            //verifica as credencias do usuário e retorna um valor
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email
                , userInfo.Password, isPersistent: false, lockoutOnFailure: false);


            if (result.Succeeded)
            {
                return new RedirectToActionResult("Index","Upload", null);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return BadRequest(ModelState);
            }
        }


        private UsuarioToken GeraToken(UsuarioDTO userInfo)
        {
            //define declarações do usuário
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            //gera a assinatura digital do token usando o algoritimo Hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiração do token
            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            //Classe que representa um token jwt e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            string tokenstr = new JwtSecurityTokenHandler().WriteToken(token);


            return new UsuarioToken()
            {
                Authenticated = true,
                Token = tokenstr,
                Expiration = expiration,
                Message = "Token JWT OK"
            };
        }

    }
}
