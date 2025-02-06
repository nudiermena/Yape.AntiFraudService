using AntiFraudService.Application.AntiFraud.Commands;
using MediatR;

namespace AntiFraudService.Contracts.AntiFraud.Commands
{
    public record FraudCheckCommand(Guid Id, Guid SourceAccountId,decimal Amount) : IRequest<FraudCheckResponse>;
}
