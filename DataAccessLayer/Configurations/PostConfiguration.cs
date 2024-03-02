using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
	public class PostConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{

			builder.HasOne(x => x.AppUser)
				   .WithMany(x => x.Posts)
				   .HasForeignKey(x => x.AppUserId);

			builder.HasOne(x => x.DeletedBy)
				   .WithMany()
				   .HasForeignKey(x => x.DeletedById);

			builder.HasOne(x => x.UpdatedBy)
				   .WithMany()
				   .HasForeignKey(x => x.UpdatedById);

			builder.HasMany(x => x.Images)
				   .WithOne(x => x.Post)
				   .HasForeignKey(x => x.PostId)
				   .IsRequired(false);
			       

			builder.HasMany(x => x.Comments)
				   .WithOne(x => x.Post)
				   .HasForeignKey(x => x.PostId);
		}
	}
}
