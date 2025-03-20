using BLL.DTOs;
using BLL.Services.Abstraction;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services.Implementation
{
    public class CardServices : ICardServices
    {
        private readonly ICardRepo _cardRepo;

        public CardServices(ICardRepo cardRepo)
        {
            _cardRepo = cardRepo;
        }

        public void AddToCart(CardDTO item)
        {
            var cardEntity = new Card
            {
                ProductId = item.ProductId,
                UserId = item.UserId,
                Quantity = item.Quantity
            };

            _cardRepo.AddToCart(cardEntity);
        }

        public void RemoveFromCart(int productId, string userId)
        {
            _cardRepo.RemoveFromCart(productId, userId);
        }

        public List<CardDTO> GetCartItems(string userId)
        {
            return _cardRepo.GetCartItems(userId).Select(c => new CardDTO
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                UserId = c.UserId,

                // Fetch product details separately
                ProductName = c.Product != null ? c.Product.Name : string.Empty,
                ImageUrl = c.Product != null ? c.Product.ImageUrl : string.Empty,
                Price = c.Product != null ? c.Product.Price : 0
            }).ToList();
        }


        public int GetCartCount(string userId)
        {
            return _cardRepo.GetCartCount(userId);
        }

        public void UpdateCartItem(CardDTO item)
        {
            var cardEntity = new Card
            {
                ProductId = item.ProductId,
                UserId = item.UserId,
                Quantity = item.Quantity
            };

            _cardRepo.UpdateCartItem(cardEntity);
        }
    }
}
