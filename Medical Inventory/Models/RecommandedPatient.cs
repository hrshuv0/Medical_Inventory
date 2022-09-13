namespace Medical_Inventory.Models;

public class RecommandedPatient
{
    public long Id { get; set; }

    public long ProductId { get; set; }
    public Product Product { get; set; }

    public long PatientGroupId { get; set; }
    public PatientGroup PatientGroup { get; set; }
}