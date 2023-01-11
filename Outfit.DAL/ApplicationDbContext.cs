using Microsoft.EntityFrameworkCore;
using Outfit.Domain;
using Outfit.Domain.Entity;
using Outfit.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.DAL
{
    public class ApplicationDbContext : DbContext
    {//установка связи с БД
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Added> Addeds { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User
                {
                    Id = 1,
                    Name = "StR",
                    Password = HashPasswordHelper.HashPassword("63obutup"),
                    Role = Role.Admin
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(20).IsRequired();

                builder.HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasPrincipalKey<User>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.Favorite)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Clothes>(builder =>
            {
                builder.ToTable("Clothes").HasKey(x => x.Id);

                builder.HasData(new Clothes
                {
                    Id=1,
                    Name="StR",
                    Description = new string('A',50),
                    DateOfAdd = DateTime.Now,
                    avatar= null,
                    outfitType = OutfitType.StreetStyle
                });
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Age).IsRequired();
                builder.Property(x => x.Address).HasMaxLength(200).IsRequired(false);

                builder.HasData(new Profile()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Favorite>(builder =>
            {
                builder.ToTable("Favorites").HasKey(x => x.Id);

                builder.HasData(new Favorite()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Added>(builder =>
            {
                builder.ToTable("Addeds").HasKey(x => x.Id);

                builder.HasOne(r => r.Favorite).WithMany(t => t.Addeds)
                .HasForeignKey(r => r.FavoriteId);
            });
        }
    }
}
