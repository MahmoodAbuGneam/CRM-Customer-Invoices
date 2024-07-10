using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Humanizer;

namespace PractiTech.Models
{
    public class Invoice
    {

        public Guid customer_id{ get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public DateTime Date { get; set; }

        
    }
}
