namespace JobManagement.UIApp.Models
{
	public class JobOrder
	{
		public Guid Id { get; set; }
		public string AssignedUser { get; set; }
		public string JobLocation { get; set; }
		public string JobServiceType { get; set; }
		public string VehicleBrand { get; set; }
		public string VehicleType { get; set; }
		public string VehicleColor { get; set; }
		public string Mark { get; set; }
		public string VIN { get; set; }
		public string InternalNumber { get; set; }
		public string Location { get; set; }
		public int VehicleMileage { get; set; }
		public DateTime AvailableFromDate { get; set; }
		public DateTime Deadline { get; set; }
		public string Note { get; set; }
		public DateTime OrderCreated { get; set; }
		public DateTime LastUpdateDate { get; set; }
		public string Status { get; set; }
		public DateTime? FinishedDate { get; set; }
		public bool SignedStatus { get; set; }
		public bool BilledStatus { get; set; }
		public bool CanceledStatus { get; set; }
	}
}
