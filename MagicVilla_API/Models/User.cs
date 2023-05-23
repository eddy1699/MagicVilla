using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //hara que se asigne automaticamente
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        
    }
}
