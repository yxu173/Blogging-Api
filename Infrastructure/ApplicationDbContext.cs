using Domain.Entities;
using Infrastracture.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastracture;

public class ApplicationDbContext(DbContextOptions options)
    : IdentityDbContext<User, Role, Guid>(options), IDbContext
{
    private IDbContextTransaction _transaction;


    public async Task BegainTransactionAsync(CancellationToken cancellationToken)
    {
        _transaction ??= await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }

    public async Task RetryOnExceptionAsync(Func<Task> func)
    {
        await Database.CreateExecutionStrategy().ExecuteAsync(func);
    }

    public DbSet<Post> Posts { get; init; }

    public DbSet<Comment> Comments { get; init; }

    public DbSet<Like> Likes { get; init; }

    public DbSet<Image> Images { get; init; }

    public DbSet<Tag> Tags { get; init; }

    public DbSet<Follow> Follows { get; init; }

    public DbSet<PostTag> PostTags { get; init; }

    public DbSet<Report> Reports { get; init; }
    
    public DbSet<EmailVerificationToken> EmailVerificationTokens { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Ignore<BasicInfo>();
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}