using Infrastracture;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UploadPhotoServices(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    public async Task<string> UploadProfilePhoto(IFormFile file, Guid userId)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file");
        var path = _webHostEnvironment.ContentRootPath + @"\Properties\wwwroot\profile-photos";
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(path, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user != null) user.AddProfileImage(fileName);
        await _dbContext.SaveChangesAsync();

        return "Profile photo uploaded successfully.";
    }


    protected async Task<string> UploadPostPhotos(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file");

        var path = _webHostEnvironment.ContentRootPath + @"\Properties\wwwroot\post-photos";
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(path, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return "Profile photo uploaded successfully.";
    }
}