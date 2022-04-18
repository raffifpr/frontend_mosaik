using Microsoft.EntityFrameworkCore;

namespace Mosaik.idAPI.Models
{
    [Index(nameof(Email), IsUnique=true)]
    public class MosaikUser
    {
        public int MosaikUserID { get; set; }
        public string Email {get; set;}
        public string AccountStatus {get; set;}
    }
}
