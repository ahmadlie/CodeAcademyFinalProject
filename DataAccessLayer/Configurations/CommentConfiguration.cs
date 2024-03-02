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
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
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
