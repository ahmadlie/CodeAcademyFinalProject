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
	internal class ImageConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			builder.HasOne(x => x.DeletedBy)
				   .WithMany()
				   .HasForeignKey(x => x.DeletedById);

			builder.HasOne(x => x.UpdatedBy)
				   .WithMany()
				   .HasForeignKey(x => x.UpdatedById);
		}
	}
}
