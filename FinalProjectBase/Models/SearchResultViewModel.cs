using DTOLayer;

namespace FinalProjectBase.Models;
public class SearchResultViewModel
{
	public IEnumerable<AppUserDTO>? UserDTOs { get; set; }
	public AppUserDTO? User { get; set; }
}
