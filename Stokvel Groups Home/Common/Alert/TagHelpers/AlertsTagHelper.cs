using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using Stokvel_Groups_Home.common.Alert;



public class AlertsTagHelper : TagHelper
{
	private const string AlertKey = "SimpleToDo.Alert";

	[Microsoft.AspNetCore.Mvc.ViewFeatures.ViewContext]
	public ViewContext? ViewContext { get; set; }

	protected ITempDataDictionary TempData => (ITempDataDictionary)ViewContext.TempData;

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.TagName = "div";

		if (TempData[AlertKey] == null)
			TempData[AlertKey] = JsonConvert.SerializeObject(new HashSet<Alert>());

		var alerts = JsonConvert.DeserializeObject<ICollection<Alert>>(TempData[AlertKey].ToString());

		var html = string.Empty;

		foreach (var alert in alerts)
		{
			html += $"<div class='alert {alert.Type} mt-2' id='inner-alert' role='alert'>" +
						$"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
							$"<span aria-hidden='true'>&times;</span>" +
						$"</button>" +
						$"{alert.Message}" +
					$"</div>";
		}

		output.Content.SetHtmlContent(html);
	}
}
