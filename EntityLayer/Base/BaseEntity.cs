using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Base
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public DateTime DeletedDate { get; set; }
		[ForeignKey(nameof(UpdatedBy))]
		public int? UpdatedById { get; set; }
		public AppUser? UpdatedBy { get; set; }
		[ForeignKey(nameof(DeletedBy))]

		public int? DeletedById { get; set; }
		public AppUser? DeletedBy { get; set; }



	}
}
