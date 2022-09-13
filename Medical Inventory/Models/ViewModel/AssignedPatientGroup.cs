namespace Medical_Inventory.Models.ViewModel;

public class AssignedPatientGroup
{
    public long PatientGroupId { get; set; }
    public string PatientGroupName { get; set; }
    public bool Assigned { get; set; }
}