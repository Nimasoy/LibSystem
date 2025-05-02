using Library.Domain.Entities;
using Library.Domain.Interfaces; // Added missing using for IBorrowRecordRepository
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Services;

public class OverdueNotificationService : BackgroundService
{
    private readonly IBorrowRecordRepository _borrowRecordRepository;
    private readonly ILogger<OverdueNotificationService> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(24);

    public OverdueNotificationService(
        IBorrowRecordRepository borrowRecordRepository,
        ILogger<OverdueNotificationService> logger)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var overdueRecords = await _borrowRecordRepository.GetOverdueBorrows();
                foreach (var record in overdueRecords)
                {
                    // In a real application, you would send an email or notification here
                    _logger.LogWarning(
                        "Book {BookId} is overdue for user {UserId}. Due date: {DueDate}",
                        record.BookId,
                        record.UserId,
                        record.DueDate);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking for overdue books");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }
} 