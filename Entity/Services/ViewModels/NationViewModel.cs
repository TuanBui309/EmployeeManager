namespace Entity.Services.ViewModels
{
	public class NationViewModel
	{
		public int Id { get; set; }
		public string NationName { get; set; }
	}
	public class InsertNation
	{
		public string NationName { get; set; }
	}
	public class UpdateNation
	{
		public int Id { get; set; }
		public string NationName { get; set; }
	}
}
