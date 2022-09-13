using System.ComponentModel.DataAnnotations;

namespace Medical_Inventory.Models
{
    public class PatientGroup
    {
        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public IList<RecommandedPatient>? RecommandedPatients { get; set; }

    }
}
