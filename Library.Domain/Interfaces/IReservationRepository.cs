using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(int id);
        Task<List<Reservation>> GetAllAsync();
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Reservation reservation);

        Task<bool> IsReserved(int bookId);
        Task<Reservation?> GetUserReservation(int userId, int bookId);
    }
}
