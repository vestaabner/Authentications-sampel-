using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Matrix.Infrastructure.Data.Entities
{
	public class PostEntity
	{
        [Key]
        public string Title { get; set; }
        public string Content { get; set; }

    }



    internal sealed class PostConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.ToTable("Categories", "Catalog");
            ;
            builder.Property(x => x.Title);

            builder.Property(e => e.Content);


        }
    }
}

