using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
	public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.HasOne(x => x.Image)
				   .WithMany(x => x.AppUsers)
				   .HasForeignKey(x => x.ImageId)
				   .OnDelete(DeleteBehavior.SetNull);

			builder.HasMany(x=>x.Comments)
				   .WithOne(x=>x.AppUser)
				   .HasForeignKey(x=>x.AppUserId);
		}
	}
}
