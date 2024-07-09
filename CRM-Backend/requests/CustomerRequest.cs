using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PractiTech.requests
{
    public class CustomerRequest
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string PhoneNum { get; set; }
    }
}
