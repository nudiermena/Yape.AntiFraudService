using Microsoft.EntityFrameworkCore;
using AntiFraudService.Application.Common.Interfaces;
using AntiFraudService.Domain.AntiFrauds;
using AntiFraudService.Infrastructure.Common.Persistence;
using AntiFraudService.Domain.AntiFraud;

namespace AntiFraudService.Infrastructure.AntiFrauds
{
    public class AntiFraudRepository : IAntiFraudRepository
    {
        private readonly AntiFraudDbContext _antiFraudDbContext;

        public AntiFraudRepository(AntiFraudDbContext antiFraudDbContext)
        {
            _antiFraudDbContext = antiFraudDbContext;
        }

        public async Task<ValidateTransacionResponse> ValidateTransaction(Guid sourceAccountId, decimal TransactionAmount)
        {
            AntiFraud dailyTotal;

            ValidateTransacionResponse response;
            if (TransactionAmount > 2000)
                return response = new ValidateTransacionResponse(Guid.NewGuid(), TransactionStatus.Rejected.ToString());

            var todayDate = DateTime.UtcNow.Date;

            try
            {

                dailyTotal = await _antiFraudDbContext.AntiFrauds
                                .Where(d => d.SourceAccountId == sourceAccountId && d.CreatedAt.Date == todayDate)
                                .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            if (dailyTotal == null)
            {
                var antiFraud = new AntiFraud(sourceAccountId, TransactionAmount, DateTime.UtcNow); //It coulbe use automapper instead.

                _antiFraudDbContext.AntiFrauds.Add(antiFraud);
                await _antiFraudDbContext.CommitChangesAsync();
            }

            if ((dailyTotal?.Value + TransactionAmount) > 20000)
                return response = new ValidateTransacionResponse(Guid.NewGuid(), TransactionStatus.Rejected.ToString());

            return response = new ValidateTransacionResponse(Guid.NewGuid(), TransactionStatus.Approved.ToString());
        }
    }
}
