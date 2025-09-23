namespace Backend1.Helper
{
    public class AddToCartRequest
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }

        public string? ProductName {  get; set; }
        public int ProductPrice { get; set; }
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
