using System;
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Features.BankAccounts.FindAllAccounts
{
	public class FindAllAccountsQuery : IRequest<IEnumerable<BankAccount>>
	{
		
	}
}

