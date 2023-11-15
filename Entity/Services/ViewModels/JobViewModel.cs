namespace Entity.Services.ViewModels
{
	public class JobViewModel
	{
		public int Id { get; set; }
		public string JobName { get; set; }
	}
	public class InsertJob
	{
		public string JobName { get; set; }
	}
	public class UpdateJob
	{
		public int Id { get; set; }
		public string JobName { get; set; }
	}
}
