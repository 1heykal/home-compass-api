using System;

namespace HomeCompassApi.Services.Cases
{
    public class MissingDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfDisappearance { get; set; }
        public string PhysicalDescription { get; set; }
        public string HomeAddress { get; set; }
        public string PhotoUrl { get; set; }
    }
}
