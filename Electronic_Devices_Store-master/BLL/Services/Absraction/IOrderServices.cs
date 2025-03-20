using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Absraction
{
    internal interface IOrderServices
    {
        Task<OrderDTO> GetbyId(int id);
        Task<List<OrderDTO>> GetAll();
        Task CreateNew(OrderDTO order);
        Task EditOrder(int id, OrderDTO newOrder);
        Task Delete(int id);
    }
}
