using FluentValidation;
// Assuming CreateReservationCommand is in the same namespace or a referenced one.
// If not, add: using Library.Application.Features.Reservations.Commands.CreateReservation;

namespace Library.Application.Features.Reservations.Commands.CreateReservation;

// TODO: Define CreateReservationCommand - it seems to be missing
// public record CreateReservationCommand(int BookId, int UserId, int ReservationDurationDays);

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0)
            .WithMessage("Book ID must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User ID must be greater than 0");

        RuleFor(x => x.ReservationDurationDays)
            .GreaterThan(0)
            .LessThanOrEqualTo(30)
            .WithMessage("Reservation duration must be between 1 and 30 days");
    }
} 