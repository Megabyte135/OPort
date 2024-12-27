namespace Services.Locations.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentLocationId { get; set; }
        public List<Guid> ChildrenLocations { get; set; }
    }
}
