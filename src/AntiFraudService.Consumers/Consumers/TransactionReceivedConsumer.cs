using AntiFraudService.Contracts.AntiFraud.Commands;
using MediatR;

namespace AntiFraudService.Consumers;

public class TransactionReceivedConsumer : IConsumer<Transaction>
{
    private readonly ITopicProducer<FraudCheckResultEvent> _producer;
    private readonly IMediator _mediator;
    public TransactionReceivedConsumer(ITopicProducer<FraudCheckResultEvent> topicProducer, IMediator mediator)
    {
        _producer = topicProducer;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<Transaction> context)
    {
        var command = new FraudCheckCommand(context.Message.SourceAccountId, context.Message.SourceAccountId, context.Message.Amount);
        var antifraudCheckResponse = await _mediator.Send(command);
        
        await _producer.Produce(new FraudCheckResultEvent
        {
            TransactionId = context.Message.TransactionId,
            Status = antifraudCheckResponse.TransactionStatus
        });

        await Task.Delay(1000);
        var transcId = context.Message.SourceAccountId;
        Console.WriteLine($"Transaction created: {transcId}");
    }
}

public class Transaction
{
    public Guid TransactionId { get; set; }
    public Guid SourceAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
}
