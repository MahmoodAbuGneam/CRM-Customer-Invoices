using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PractiTech.requests
{
    public class InvoiceRequest
    {
        public Guid customer_id { get; set; }
        public DateTime Date { get; set; }

    }
}
