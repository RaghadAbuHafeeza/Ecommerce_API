using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<LocalUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public UsersRepository(AppDbContext dbContext, UserManager<LocalUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public bool IsUniqueUser(string Email)
        {
            var result = dbContext.LocalUser.FirstOrDefault(x => x.Email == Email);
            return result == null;
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<LocalUserDTO> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            var user = new LocalUser
            {
                Email = registerationRequestDTO.Email,  // raghadabuhafeeza@gmail.com
                UserName = registerationRequestDTO.Email.Split("@")[0],  // raghadabuhafeeza
                FirstName = registerationRequestDTO.Fname,
                LastName = registerationRequestDTO.Lname,
                Address = registerationRequestDTO.Address
            };

            using(var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await userManager.CreateAsync(user, registerationRequestDTO.Password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.RoleExistsAsync(registerationRequestDTO.Role);

                        if (!role)
                        {
                            throw new Exception($"The role {registerationRequestDTO.Role} doesn't exist. !");
                        }


                        var userRoleResult = await userManager.AddToRoleAsync(user, registerationRequestDTO.Role);

                        if (userRoleResult.Succeeded)
                        {
                            await transaction.CommitAsync();
                            var userReturn = dbContext.LocalUser.FirstOrDefault(u => u.Email == registerationRequestDTO.Email);
                            return mapper.Map<LocalUserDTO>(userReturn);
                        }
                        else
                        {
                            await transaction.RollbackAsync(); // Rollback transaction if adding to usersRoles fails
                            throw new Exception("Failed to add user to UserRoles");
                        }
                    }
                    else
                    {
                        await transaction.RollbackAsync(); // Rollback transaction if Add user fails
                        throw new Exception("User Registeration Failed");
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            
        }
    }
}
