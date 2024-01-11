using App.Business.Enums;
using App.Business.Exceptions.Account;
using App.Business.Services.Interfaces;
using App.Business.ViewModels.AccountVMs;
using App.Core.Entities.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.Impelemtations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                var existRole = await _roleManager.RoleExistsAsync(item.ToString());
                if (!existRole)
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = item.ToString(),
                    });
                }
            }
        }

        public async Task Login(LoginVM login)
        {
            var user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
            if(user is null)
            {
                user = await _userManager.FindByNameAsync(login.UsernameOrEmail);
                if(user is null)
                {
                    throw new AccountArgumentException("Username/Email or Password is not valid!", nameof(login.UsernameOrEmail)); 
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);

            if(!result.Succeeded)
            {
                throw new AccountArgumentException("Username/Email or Password is not valid!", nameof(login.UsernameOrEmail));
            }

            await _signInManager.SignInAsync(user, true);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterVM register)
        {
            AppUser user = new()
            {
                Name = register.Name,
                Surname = register.Surname,
                Email = register.Email,
                UserName = register.Username,
            };

            var checkEmail = await _userManager.FindByEmailAsync(register.Email);

            if(checkEmail is null)
            {
                var result = await _userManager.CreateAsync(user, register.Password);
                await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());

                if(!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        throw new AccountArgumentException($"{item.Description}", nameof(item));
                    }
                }
            }
            else
            {
                throw new AccountArgumentException("This email used before, please try another email!", nameof(register.Email));
            }
        }
    }
}
