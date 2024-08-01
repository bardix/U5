using EsSettimanaleU5S3.DataModel;

public class CartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PizzeriaDbContext _context;

    public CartService(IHttpContextAccessor httpContextAccessor, PizzeriaDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public List<OrderItem> GetCart()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cart = session.Get<List<OrderItem>>("Cart") ?? new List<OrderItem>();
        return cart;
    }

    public void AddToCart(int productId, int quantity)
    {
        var cart = GetCart();
        var cartItem = cart.SingleOrDefault(c => c.ProductId == productId);

        if (cartItem == null)
        {
            var product = _context.Products.Find(productId);
            cart.Add(new OrderItem
            {
                ProductId = productId,
                Product = product,
                Quantity = quantity,
                TotalPrice = product.Price * quantity
            });
        }
        else
        {
            cartItem.Quantity += quantity;
            cartItem.TotalPrice = cartItem.Quantity * cartItem.Product.Price;
        }

        SaveCart(cart);
    }

    public void RemoveFromCart(int productId)
    {
        var cart = GetCart();
        var cartItem = cart.SingleOrDefault(c => c.ProductId == productId);
        if (cartItem != null)
        {
            cart.Remove(cartItem);
        }

        SaveCart(cart);
    }

    public void ClearCart()
    {
        SaveCart(new List<OrderItem>());
    }

    private void SaveCart(List<OrderItem> cart)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.Set("Cart", cart);
    }
}
