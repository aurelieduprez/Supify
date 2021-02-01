using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Supify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

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

                // playlist id foreign key
                n.Property(f => f.Id).ValueGeneratedOnAdd();

                // taille maxi
                n.Property(p => p.Name).HasMaxLength(64);
            });


            modelBuilder.Entity<Playlist>().HasData(
                new Playlist
                {
                    Id = 3,
                    User = "999",
                    Name = "All time favorites"
                }

                );

           modelBuilder.Entity<Song>().HasData(
                new Song
                {
                    Id = 3,
                    PlaylistId = 3,
                    Name = "the Weeknd - Save your Tears"
                },
                            new Song
                            {
                                Id = 4,
                                PlaylistId = 3,
                                Name = "Yazoo - Only You"
                            },
                                        new Song
                                        {
                                            Id = 5,
                                            PlaylistId = 3,
                                            Name = "JOJI - DANCING IN THE DARK"
                                        }

                );



 

        }
    }
}
