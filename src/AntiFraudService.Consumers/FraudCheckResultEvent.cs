using System.Transactions;

namespace AntiFraudService.Consumers;

    public record FraudCheckResultEvent
{
    public Guid TransactionId { get; set; }
    public string Status { get; set; } = string.Empty;
}

