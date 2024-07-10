namespace PractiTech.requests
{
    public class ItemRequest
    {
        public Guid invoice_id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }

        public float price { get; set; }
    }
}
