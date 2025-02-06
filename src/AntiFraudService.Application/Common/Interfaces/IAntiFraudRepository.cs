using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraudService.Application.Common.Interfaces
{
    public interface IAntiFraudRepository
    {        
        Task<ValidateTransacionResponse> ValidateTransaction(Guid sourceAccountId, decimal amount);
    }
}
