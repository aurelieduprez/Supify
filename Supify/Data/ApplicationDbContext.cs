using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Supify.Models;
using Microsoft.AspNetCore.Identity;

namespace Supify.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<Song> Song { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>(n => {
                // clé primaire
                n.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Playlist>(n =>
            {
                // clé primaire
                n.HasKey(p => p.Id);

                // id auto-increment non null
                n.Property(f => f.Id).ValueGeneratedOnAdd();

                // taille maxi
                n.Property(p => p.Name).HasMaxLength(64);

                // clé étrangère (song id)
                n.HasMany<Song>()
                .WithOne()
                .HasForeignKey(fk => fk.PlaylistId)
                .IsRequired();

            });

            modelBuilder.Entity<Song>(n =>
            {
                // clé primaire
                n.HasKey(p => p.Id);

                // id auto_increment non null
                n.Property(f => f.Id).ValueGeneratedOnAdd();

                // taille maxi
                n.Property(p => p.Name).HasMaxLength(64);
            });
        }
    }
}
