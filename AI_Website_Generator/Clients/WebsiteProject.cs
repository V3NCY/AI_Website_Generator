namespace Orak.WebPro.Admin.Models
{
	public class WebsiteProject
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public int RequestId { get; set; }

		public string ProjectName { get; set; } = "";
		public string CurrentStage { get; set; } = "";
		public string SelectedTemplate { get; set; } = "";
		public string AssignedDesigner { get; set; } = "";
		public string AssignedDeveloper { get; set; } = "";
		public bool DesignApproved { get; set; }
		public bool ReadyForCms { get; set; }
		public bool IsLaunched { get; set; }
		public DateTime? LaunchDate { get; set; }
		public string Notes { get; set; } = "";
	}
}