using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Mosaik.idAPI.Models
{
    [Index(nameof(Email), IsUnique=true)]
    public class MosaikChild
    {
        [Required]
        public int MosaikChildID { get; set; }

        [Required]
        public string Username{ get; set; }

        [Required]
        public string Email{ get; set; }

        [Required]
        public string Password { get; set; }
    }
}