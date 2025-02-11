using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dokaanah.Models
{
    public class BaseEntity 
    {
        public virtual int       Id         { get; set; }
        public string?          Name        { get; set; }
        public bool            IsDeleted    { get; set; } = false;
        public DateTime        CreationData { get; set; } = DateTime.UtcNow;

    }
}
