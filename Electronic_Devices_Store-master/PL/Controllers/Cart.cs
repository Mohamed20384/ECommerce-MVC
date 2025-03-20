using BLL.DTOs;
using BLL.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICardServices _cardServices;

        public CartController(ICardServices cardServices)
        {
            _cardServices = cardServices;
        }

        [HttpPost]
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized();
                userId = "1";
            }

            var cartItemDto = new CardDTO
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId
            };

            _cardServices.AddToCart(cartItemDto);

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized();
                userId = "1";
            }

            var cartItems = _cardServices.GetCartItems(userId);
            return View(cartItems);
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized();
                userId = "1";
            }

            _cardServices.RemoveFromCart(productId, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized();
                userId = "1";
            }

            var updatedItem = new CardDTO
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId
            };

            _cardServices.UpdateCartItem(updatedItem);
            return RedirectToAction("Index");
        }
    }
}
