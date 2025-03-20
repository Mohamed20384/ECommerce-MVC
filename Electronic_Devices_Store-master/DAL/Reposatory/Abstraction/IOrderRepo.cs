using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatory.Abstraction
{
    public interface IOrderRepo
    {
        Task<Order> GetbyId(int id);
        Task<List<Order>> GetAll();
        Task CreateNew(Order product);
        Task EditProduct(int id, Order newProduct);
        Task Delete(int id);
    }
}
