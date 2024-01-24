using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using System.Reflection.Metadata;

namespace HomeCompassApi.Models.Facilities
{
    public class Shelter : Facility
    {
        int availableBeds;

        public int FacilityId { get; set; }
        public Facility facility { get; set; }

    }
}
