using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Models.Cases
{
    public class Missing : Case
    {
        public DateTime DateOfDisappearance { get; set; }
        public string LastKnownLocation { get; set; }
        public string PhysicalDescription { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string ContactNumber { get; set; }
        public string HomeAddress { get; set; }

    }
}
