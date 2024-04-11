using DTOLayer;

namespace FinalProjectBase.Models;
public class AuthorProfileViewModel
{
	public int Id { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? EMail { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Username { get; set; }
	public ImageViewModel? Image { get; set; }
	public List<AppRoleViewModel> AppRoles { get; set; }
	public List<PostViewModel> Posts { get; set; }
	public PostViewModel Post { get; set; }
}
