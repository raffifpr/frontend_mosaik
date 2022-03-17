using System.ComponentModel.DataAnnotations;

namespace Mosaik.idAPI.Models
{
    public class MosaikItem
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string FullName{ get; set; }

        [Required]
        public string Email{ get; set; }

        [Required]
        public string Password { get; set; }
    }
}