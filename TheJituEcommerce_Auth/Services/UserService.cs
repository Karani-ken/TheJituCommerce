using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheJituEcommerce_Auth.Data;
using TheJituEcommerce_Auth.Models;
using TheJituEcommerce_Auth.Models.DTOs;
using TheJituEcommerce_Auth.Services.IService;

namespace TheJituEcommerce_Auth.Services
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtInterface _jwtGenerator;

        public UserService(IJwtInterface jwtToken ,AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtToken;
        }
        public async Task<bool> AssignUserRole(string email, string Rolename)
        {
            //get user by email
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                //user exists and we can assign a role
                //check if role exists
                if (!_roleManager.RoleExistsAsync(Rolename).GetAwaiter().GetResult())
                {
                    //first create the role
                    _roleManager.CreateAsync(new IdentityRole(Rolename)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, Rolename);

                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            //checks if user exists
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());
            //check if password is the right one
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            
            //check if user is null or password is wrong
            if(!isValid || user == null)
            {
                new LoginRequestDto();
            }
            //correct credentials
            var roles = await _userManager.GetRolesAsync(user);
            //create token
            var token = _jwtGenerator.GenerateToken(user,roles);
            var LoggedUser = new LoginResponseDto()
            {
                User = _mapper.Map<UserDto>(user),
                Token = token
            };

            return LoggedUser;
        }

        public async Task<string> RegisterUser(RegisterRequestDto registerRequestDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerRequestDto);
            try
            {
                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
                if (result.Succeeded)
                {
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }catch(Exception ex)
            {
                return ex.Message;
            }
           
        }
    }
}
