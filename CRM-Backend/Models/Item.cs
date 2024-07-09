using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PractiTech.Models
{
    public class Item
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }


        public Guid invoice_id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }

        public float price { get; set; }
    }
}
