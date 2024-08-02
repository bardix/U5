using EsSettimanaleU5S3.DataModel;
using EsSettimanaleU5S3.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

public class CartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PizzeriaDbContext _context;

    public CartService(IHttpContextAccessor httpContextAccessor, PizzeriaDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public List<CartItem> GetCart()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cart = session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        return cart;
    }

    public void AddToCart(int productId, int quantity)
    {
        var cart = GetCart();
        var cartItem = cart.SingleOrDefault(c => c.ProductId == productId);

        if (cartItem == null)
        {
            var product = _context.Products.Find(productId);
            cart.Add(new CartItem
            {
                ProductId = productId,
                Product = product,
                Quantity = quantity
            });
        }
        else
        {
            cartItem.Quantity += quantity;
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
        SaveCart(new List<CartItem>());
    }

    private void SaveCart(List<CartItem> cart)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.SetObjectAsJson("Cart", cart);
    }
}
