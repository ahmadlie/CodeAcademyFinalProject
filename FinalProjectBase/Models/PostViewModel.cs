using System.Drawing;
namespace FinalProjectBase.Models
{
	public class PostViewModel
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public List<ImageViewModel> Images { get; set; }
		public List<IFormFile> FormFiles { get; set; }
		public AppUserViewModel AppUser { get; set; }
		
	}
}
