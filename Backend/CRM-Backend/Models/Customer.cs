using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiTech.Models
{
    public class Customer
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public Guid ID { get; set; }

        public required string Name { get; set; }

        public required string Address {  get; set; }
        public required string PhoneNum { get; set; }
    }
}
