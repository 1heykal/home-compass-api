using HomeCompassApi.Models.Facilities;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HomeCompassApi.Models.Facilities
{
    public class Work : Facility
    {
        public decimal Salary { get; set; }
        public string EmployementType { get; set; }
        public string Benefits { get; set; }
    }
}
