namespace FinalProjectBase.Models
{
	public class ImageViewModel
	{
		public int Id { get; set; }	
		public string? ImageName { get; set; }
		public string? ImageUrl { get; set; }
		public List<AppUserViewModel> AppUsers { get; set; }
	}
}
