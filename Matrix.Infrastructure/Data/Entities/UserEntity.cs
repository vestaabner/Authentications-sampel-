using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix.Infrastructure.Data.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Phone { get; set; }


    }
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

             builder.HasKey(x => x.Id);
             builder.Property(x => x.Email).IsRequired();
             builder.Property(x => x.PasswordHash).IsRequired();
             builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.Phone).IsRequired();


        }
    }
}

