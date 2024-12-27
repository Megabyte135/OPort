namespace Services.Locations.DTOs
{
    public class LocationCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentLocationId { get; set; }
        public List<Guid> ChildrenLocations { get; set; }
    }
}
