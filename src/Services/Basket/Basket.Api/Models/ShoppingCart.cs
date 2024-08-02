namespace Basket.Api.Models
{
    public class ShoppingCart
    {
        public string Username { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string username)
        {
            Username = username;
        }

        //Required for mapping
        public ShoppingCart()
        {
            
        }
    }
}
