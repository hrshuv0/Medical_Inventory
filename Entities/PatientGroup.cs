using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Entities
{
    public class PatientGroup
    {
        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public long? CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        [ValidateNever]
        public ApplicationUser? CreatedBy { get; set; }

        public long? UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        [ValidateNever]
        public ApplicationUser? UpdatedBy { get; set; }

        public IList<RecommandedPatient>? RecommandedPatients { get; set; }

    }
}
