using System;
using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Features.BankAccounts.FindAccountWithBalance
{
    public class FindAccountWithBalanceHandler : IRequestHandler<FindAccountWithBalanceQuery, IEnumerable<BankAccount>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public FindAccountWithBalanceHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<IEnumerable<BankAccount>> Handle(FindAccountWithBalanceQuery request, CancellationToken cancellationToken)
        {
            if (request.EqualityType == "GREATER_THAN")
            {
                return await _bankAccountRepository.FindByBalanceGreaterThan(request.Balance);
            }

            return await _bankAccountRepository.FindByBalanceLessThan(request.Balance);
        }
    }
}

