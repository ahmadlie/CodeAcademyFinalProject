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
			builder.HasMany(x => x.Images)
				   .WithOne(x => x.AppUser)
				   .HasForeignKey(x => x.AppUserId);

			builder.HasMany(x=>x.Comments)
				   .WithOne(x=>x.AppUser)
				   .HasForeignKey(x=>x.AppUserId);
		}
	}
}
