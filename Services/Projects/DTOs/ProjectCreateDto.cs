using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Projects.DTOs
{
    public class ProjectCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public DateTime CompletedDate { get; set; }
        public List<Guid> AttachedFileIds { get; set; } = new List<Guid>();
    }
}
