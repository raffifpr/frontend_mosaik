using System.ComponentModel.DataAnnotations;

namespace Mosaik.idAPI.Models
{
    public class MosaikParent
    {
        [Required]
        public int MosaikParentID { get; set; }

        [Required]
        public string Username{ get; set; }

        [Required]
        public string Email{ get; set; }

        [Required]
        public string Password { get; set; }
    }
}