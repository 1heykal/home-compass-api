namespace HomeCompassApi.Models.Facilities
{
    public class Restaurant : Facility
    {
        int AvailableMeals;

        public int FacilityId { get; set; }
        public Facility facility { get; set; }

    }
}
