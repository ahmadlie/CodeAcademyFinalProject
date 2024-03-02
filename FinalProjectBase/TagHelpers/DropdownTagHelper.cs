using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FinalProjectBase.TagHelpers;

public class DropdownTagHelper : TagHelper
{
//     public string actionName { get; set; } = null!;
//     public string controllerName { get; set; } = null!;
//     public int modelId { get; set; }
//     public override void Process(TagHelperContext context, TagHelperOutput output)
// {
//     var tagBuilder = new TagBuilder("div");
//     tagBuilder.AddCssClass("dropdown");

//     var buttonBuilder = new TagBuilder("button");
//     buttonBuilder.AddCssClass("btn btn-secondary dropdown-toggle");
//     buttonBuilder.Attributes.Add("type", "button");
//     buttonBuilder.Attributes.Add("id", "dropdownMenuButton");
//     buttonBuilder.Attributes.Add("data-toggle", "dropdown");
//     buttonBuilder.Attributes.Add("aria-haspopup", "true");
//     buttonBuilder.Attributes.Add("aria-expanded", "false");
//     buttonBuilder.InnerHtml.Append("Eylemi Seç");

//     var menuBuilder = new TagBuilder("div");
//     menuBuilder.AddCssClass("dropdown-menu");
//     menuBuilder.Attributes.Add("aria-labelledby", "dropdownMenuButton");

//     var updateLink = new TagBuilder("a");
//     updateLink.AddCssClass("dropdown-item");
//     updateLink.Attributes.Add("href", $"/admin/{controllerName}/update/{modelId}");
//     updateLink.InnerHtml.Append("Güncelle");

//     var deleteLink = new TagBuilder("a");
//     deleteLink.AddCssClass("dropdown-item");
//     deleteLink.Attributes.Add("href", $"/admin/{controllerName}/delete/{modelId}");
//     deleteLink.InnerHtml.Append("Sil");

//     menuBuilder.InnerHtml.AppendHtml(updateLink);
//     menuBuilder.InnerHtml.AppendHtml(deleteLink);

//     tagBuilder.InnerHtml.AppendHtml(buttonBuilder);
//     tagBuilder.InnerHtml.AppendHtml(menuBuilder);

//     output.Content.AppendHtml(tagBuilder);
// }
}