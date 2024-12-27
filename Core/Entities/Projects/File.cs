namespace Core.Entities.Projects
{
    public class File : BaseEntity<Guid>
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
