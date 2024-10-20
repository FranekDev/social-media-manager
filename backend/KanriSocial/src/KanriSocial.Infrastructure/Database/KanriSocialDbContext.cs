using KanriSocial.Domain.Models;
using KanriSocial.Domain.Models.Instagram.Post;
using KanriSocial.Domain.Models.Instagram.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KanriSocial.Infrastructure.Database;

public class KanriSocialDbContext : IdentityDbContext<User>
{
    public KanriSocialDbContext(DbContextOptions<KanriSocialDbContext> options)
    : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //
        // var roles = new List<IdentityRole>
        // {
        //     new IdentityRole
        //     {
        //         Name = "Admin",
        //         NormalizedName = "ADMIN"
        //     },
        //     new IdentityRole
        //     {
        //         Name = "User",
        //         NormalizedName = "USER"
        //     }
        // };
        //
        // builder.Entity<IdentityRole>().HasData(roles);

        builder.Entity<InstagramUser>()
            .Property(iu => iu.Id)
            .HasColumnType("uuid");
        
        builder.Entity<InstagramPost>()
            .Property(ip => ip.Id)
            .HasColumnType("uuid");
        
        builder.Entity<InstagramUser>()
            .Property(iu => iu.CreatedAt)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        builder.Entity<InstagramUser>()
            .Property(iu => iu.UpdatedAt)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        builder.Entity<InstagramPost>()
            .Property(ip => ip.ScheduledAt)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        builder.Entity<InstagramUser>()
            .HasOne(iu => iu.User)
            .WithOne()
            .HasForeignKey<InstagramUser>(iu => iu.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<InstagramUser>()
            .HasMany(iu => iu.InstagramPosts)
            .WithOne(ip => ip.InstagramUser)
            .HasForeignKey(ip => ip.InstagramUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserToken>()
            .HasOne(ut => ut.User)
            .WithOne()
            .HasForeignKey<UserToken>(ut => ut.UserId);
    }
    
    public DbSet<InstagramUser> InstagramUsers { get; set; }
    public DbSet<InstagramPost> InstagramPosts { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
}