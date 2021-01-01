using AutoMapper;
using Core.Entities.Identity;
using Core.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.DTOs;
using Web.Errors;
using Web.Extensions;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApplicationUserDTO>> GetApplicationUser()
        {
            var user = await _userManager.FindUserAsync(HttpContext.User);

            return new ApplicationUserDTO
            {
                DisplayNme = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address, AddressDTO>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO addressDTO)
        {
            var user = await _userManager.FindUserWithAddressAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDTO, Address>(addressDTO);

            var userUpdate = await _userManager.UpdateAsync(user);

            if (userUpdate.Succeeded)
            {
                return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
            }

            return BadRequest("Problem updating the user");
        }


        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
            {
                return Unauthorized(new ResponseBody(401));
            }

            var signIn = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!signIn.Succeeded)
            {
                return Unauthorized(new ResponseBody(401));
            }

            return new ApplicationUserDTO
            {
                DisplayNme = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<ActionResult<ApplicationUserDTO>> Register(RegistrationDTO registrationDTO)
        {
            var user = new ApplicationUser
            {
                UserName = registrationDTO.Email,
                DisplayName = registrationDTO.DisplayName,
                Email = registrationDTO.Email,
            };

            var register = await _userManager.CreateAsync(user, registrationDTO.Password);

            if (!register.Succeeded)
            {
                return BadRequest(new ResponseBody(400));
            }

            return new ApplicationUserDTO
            {
                DisplayNme = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}
