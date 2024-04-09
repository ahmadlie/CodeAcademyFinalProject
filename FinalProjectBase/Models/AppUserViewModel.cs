namespace FinalProjectBase.Models
{
    public class AppUserViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ImageViewModel? Image { get; set; }
        public IFormFile? FormFile { get; set; }
        public List<AppRoleViewModel> AppRoles { get; set; }
        public List<string>? SelectedRoles { get; set; }
        public List<PostViewModel> postViewModels { get; set; }
    }
}
