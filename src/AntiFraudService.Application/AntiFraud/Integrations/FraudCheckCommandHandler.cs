using AntiFraudService.Application.Common.Interfaces;
using AntiFraudService.Contracts.AntiFraud.Commands;
using MediatR;
namespace AntiFraudService.Application.AntiFraud.Commands
{
    internal class FraudCheckCommandHandler : IRequestHandler<FraudCheckCommand, FraudCheckResponse>
    {
        private readonly IAntiFraudRepository _repository;

        public FraudCheckCommandHandler(IAntiFraudRepository repository)
        {
            _repository = repository;
        }

        public async Task<FraudCheckResponse> Handle(FraudCheckCommand request, CancellationToken cancellationToken)
        {
            var validateResponse = await _repository.ValidateTransaction(request.SourceAccountId, request.Amount);

            var fraudCheckResponse = new FraudCheckResponse(validateResponse.Id, validateResponse.Status);

            return fraudCheckResponse;
        }
    }
}
