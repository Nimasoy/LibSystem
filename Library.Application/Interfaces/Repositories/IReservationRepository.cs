using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(int id);
        Task<List<Reservation>> GetAllAsync();
        Task AddAsync(Reservation entity);
        Task UpdateAsync(Reservation entity);
        Task DeleteAsync(Reservation entity);
    }
}
