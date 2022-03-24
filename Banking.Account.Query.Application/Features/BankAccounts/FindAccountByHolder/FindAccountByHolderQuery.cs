using System;
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Features.BankAccounts.FindAccountByHolder
{
	public class FindAccountByHolderQuery: IRequest<IEnumerable<BankAccount>>
	{
        public string AccountHolder { get; set; } = string.Empty;
    }
}

