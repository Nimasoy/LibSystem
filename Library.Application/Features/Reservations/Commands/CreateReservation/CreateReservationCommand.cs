using Library.Application.Common;
using MediatR;

namespace Library.Application.Features.Reservations.Commands.CreateReservation;

/// <summary>
/// Represents the command to create a new reservation.
/// </summary>
/// <param name="BookId">The ID of the book to reserve.</param>
/// <param name="UserId">The ID of the user making the reservation.</param>
/// <param name="ReservationDurationDays">The duration of the reservation in days.</param>
public record CreateReservationCommand(int BookId, int UserId, int ReservationDurationDays) : IRequest<BaseResponse<int>>; // Assuming return BaseResponse with Reservation ID
