using Domain.Entities;
using Infrastracture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Application.Services;

public class UserServices(
    UserManager<User> userManager,
    ApplicationDbContext dbContext,
    SignInManager<User> signInManager)
{
    public async Task<IdentityResult> CreateUser(User user, string password)
    {
        var result = await userManager.CreateAsync(user, password);
        await dbContext.SaveChangesAsync();
        return result;
    }

    public async Task<SignInResult> LoginUser(string userName, string password)
    {
        return await signInManager
            .PasswordSignInAsync(userName, password, false, false);
    }

    public async Task<Task> LogoutUser()
    {
        await signInManager.SignOutAsync();
        return Task.CompletedTask;
    }

    public async Task<User?> UpdateUserName(Guid id, string userName)
    {
        var user = await GetUserById(id);
        if (user != null)
        {
            user.UserName = userName;
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        return user;
    }

    public async Task<User?> UpdateEmail(Guid id, string email)
    {
        var user = await GetUserById(id);
        if (user != null)
        {
            user.Email = email;
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        return await userManager.FindByNameAsync(userName);
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        return await userManager.FindByIdAsync(userId.ToString());
    }


    public async Task<bool> CheckPassword(User user, string password)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> DeleteUserById(Guid id)
    {
        var user = await GetUserById(id);
        if (user == null) return false;
        var follows = dbContext.Follows
            .Where(f => f.FollowedId == user.Id || f.FollowerId == user.Id);
        dbContext.Follows.RemoveRange(follows);
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return true;
    }
}