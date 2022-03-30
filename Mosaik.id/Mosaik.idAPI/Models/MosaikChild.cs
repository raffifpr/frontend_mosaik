using System.ComponentModel.DataAnnotations;

namespace Mosaik.idAPI.Models
{
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