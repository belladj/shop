using shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop.Actions
{
    public class CartActions : IDisposable
    {
        public string ShoppingCartId { get; set; }
        private ProductContext _db = new ProductContext();
        public const string CartSessionKey = "CartId";
        public void AddToCart(string code)
        {
            ShoppingCartId = GetCartId();
            var cartItem = _db.CartItems.SingleOrDefault(
            c => c.CartId == ShoppingCartId
            && c.ProductCode == code);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductCode = code,
                    CartId = ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(
                p => p.ProductCode == code),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }
        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] =
                   HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }
        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();
            return _db.CartItems.Where(
            c => c.CartId == ShoppingCartId).ToList();
        }

        public decimal GetTotal()
        {
            ShoppingCartId = GetCartId();
            decimal? total = decimal.Zero;
            total = (decimal?)(from cartItems in _db.CartItems
                               where cartItems.CartId == ShoppingCartId
                               select (int?)cartItems.Quantity *
                                cartItems.Product.Price).Sum();
            return total ?? decimal.Zero;
        }

        public CartActions GetCart(HttpContext context)
        {
            using (var cart = new CartActions())
            {
                cart.ShoppingCartId = cart.GetCartId();
                return cart;
            }
        }
        public void UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[]
       CartItemUpdates)
        {
            using (var db = new ProductContext())
            {
                try
                {
                    int CartItemCount = CartItemUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach (var cartItem in myCart)
                    {
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (cartItem.Product.ProductCode.Equals(CartItemUpdates[i].ProductCode))
                            {
                                if (CartItemUpdates[i].PurchaseQuantity < 1 ||
                               CartItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, cartItem.ProductCode);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.ProductCode,
                                   CartItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Database - " +
                   exp.Message.ToString(), exp);
                }
            }
        }
        public void RemoveItem(string removeCartID, string removeProductCode)
        {
            using (var _db = new ProductContext())
            {
                try
                {
                    var myItem = (from c in _db.CartItems
                                  where c.CartId == removeCartID && c.Product.ProductCode.Equals(removeProductCode)
                                  select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        _db.CartItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " +
                   exp.Message.ToString(), exp);
                }
            }
        }
        public void UpdateItem(string updateCartID, string updateProductCode, int
       quantity)
        {
            using (var _db = new ProductContext())
            {
                try
                {
                    var myItem = (from c in _db.CartItems
                                  where c.CartId == updateCartID && c.Product.ProductCode.Equals(updateProductCode)
                                  select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " +
                   exp.Message.ToString(), exp);
                }
            }
        }
        public void EmptyCart()
        {
            ShoppingCartId = GetCartId();
            var cartItems = _db.CartItems.Where(
            c => c.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                _db.CartItems.Remove(cartItem);
            }
            _db.SaveChanges();
        }

        public void CreateRecord()
        {
            using (var _db = new ProductContext())
            {
                try
                {
                    List<TransactionDetail> items = new List<TransactionDetail>();
                    var cartItems = GetCartItems();
                    foreach (var cartItem in cartItems)
                    {
                        var Item = new TransactionDetail
                        {
                            DocumentCode = "TRX",
                            DocumentId = GetCartId(),
                            ProductCode = cartItem.ProductCode,
                            Price = cartItem.Product.Price,
                            Quantity = cartItem.Quantity,
                            Unit = cartItem.Product.Unit,
                            Subtotal = cartItem.Product.Price * cartItem.Quantity,
                            Currency = cartItem.Product.Currency
                        };
                        items.Add(Item);
                        _db.TransactionDetails.Add(Item);
                    }
                    var Transaction = new TransactionHeader
                    {
                        DocumentCode = "TRX",
                        DocumentId = GetCartId(),
                        User = !string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name) ?
                            HttpContext.Current.User.Identity.Name : "guest",
                        Total = GetTotal(),
                        Date = DateTime.Now,
                        ItemList = new List<TransactionDetail>(items)
                    };
                    _db.TransactionHeaders.Add(Transaction);
                    _db.SaveChanges();
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Process Transaction - " +
                    exp.Message.ToString(), exp);
                }
            }
            
        }
        public int GetCount()
        {
            ShoppingCartId = GetCartId();
            int? count = (from cartItems in _db.CartItems
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            return count ?? 0;
        }
        public struct ShoppingCartUpdates
        {
            public string ProductCode;
            public int PurchaseQuantity;
            public bool RemoveItem;
        }
        public void MigrateCart(string cartId, string userName)
        {
            var shoppingCart = _db.CartItems.Where(c => c.CartId == cartId);
            foreach (CartItem item in shoppingCart)
            {
                item.CartId = userName;
            }
            HttpContext.Current.Session[CartSessionKey] = userName;
            _db.SaveChanges();
        }
    }
}