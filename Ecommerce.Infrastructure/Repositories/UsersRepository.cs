using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Core.IRepositories.IServices;
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
        // Injected AppDbContext for interacting with the database using Entity Framework Core.
        private readonly AppDbContext dbContext;
        // UserManager is used to manage user-related operations like creating, deleting, and updating user information.
        private readonly UserManager<LocalUser> userManager;
        // RoleManager is used to manage roles, such as creating, deleting, and assigning roles to users.
        private readonly RoleManager<IdentityRole> roleManager;
        // SignInManager is used to handle user authentication, including sign-in and sign-out processes.
        private readonly SignInManager<LocalUser> signInManager;
        // IMapper is used for mapping between entities and DTOs (Data Transfer Objects) to simplify data transformations.
        private readonly IMapper mapper;
        // ITokenService is used to generate and manage authentication tokens, enabling secure user authentication and authorization.
        private readonly ITokenService tokenService;

        public UsersRepository(AppDbContext dbContext, UserManager<LocalUser> userManager,
                   RoleManager<IdentityRole> roleManager, IMapper mapper,
                   SignInManager<LocalUser> signInManager,
                   ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        public bool IsUniqueUser(string Email)
        {
            var result = dbContext.LocalUser.FirstOrDefault(e => e.Email == Email);
            return result == null;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Email);

            var checkPasssword = await signInManager.CheckPasswordSignInAsync(user, loginRequestDTO.Password, false);// false => Validates the user's password without locking the account on failed attempts.
            if (!checkPasssword.Succeeded)
            {
                return new LoginResponseDTO()
                {
                    User = null,
                    Token = "",

                };
            }

            var role = await userManager.GetRolesAsync(user);
            return new LoginResponseDTO()
            {
                User = mapper.Map<LocalUserDTO>(user),
                Token = await tokenService.CreateTokenAsync(user),
                Role = role.FirstOrDefault(),
            };
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

            // Ensures user creation and role assignment are completed together or rolled back if any step fails.
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Creates a new user with the specified password using the UserManager service.
                    var result = await userManager.CreateAsync(user, registerationRequestDTO.Password);
                    if (result.Succeeded)
                    {
                        var role = await roleManager.RoleExistsAsync(registerationRequestDTO.Role);

                        if (!role)
                        {
                            throw new Exception($"The role {registerationRequestDTO.Role} doesn't exist. !");
                        }

                        // Assigns the specified role to the newly created user.
                        var userRoleResult = await userManager.AddToRoleAsync(user, registerationRequestDTO.Role);

                        if (userRoleResult.Succeeded)
                        {
                            await transaction.CommitAsync(); // Finalize the transaction and save all changes permanently after successfully assigning the role.
                            var userReturn = dbContext.LocalUser.FirstOrDefault(u => u.Email == registerationRequestDTO.Email); // Gets the newly created user from the database.
                            return mapper.Map<LocalUserDTO>(userReturn); // Converts the user entity to a LocalUserDTO object using AutoMapper.
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
