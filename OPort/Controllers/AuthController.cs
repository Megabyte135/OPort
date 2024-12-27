using Core.Entities.Roles;
using Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        RoleManager<Role> _roleManager;

        public AuthController(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(string firstName, string lastName, string password, string email)
        {
            var user = new User
            {
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

            if (result.Succeeded)
            {
                return Ok(_userManager.GetUserId(HttpContext.User));
            }

            return StatusCode(500);
        }

        [HttpPost("signout")]
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpPut("update-role")]
        public async void UpdateUserRole(string email, int roleId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            await _userManager.AddToRoleAsync(user, role.Name);
        }

        [HttpPost("add-role")]
        public async void AddRole(string name)
        {
            var role = new Role
            {
                Name = name
            };

            await _roleManager.CreateAsync(role);
        }

        [HttpGet("fetch-roles")]
        public async Task<List<Role>> FetchRoles()
        {
            var result = await _roleManager.Roles.ToListAsync();

            return result;
        }

        [HttpGet("get-user-info")]
        public async Task<User?> GetUserInfo(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
