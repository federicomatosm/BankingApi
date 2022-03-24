using System;
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Features.BankAccounts.FindAccountById
{
	public class FindAccountByIdQuery : IRequest<BankAccount>
	{
        public string Identifier { get; set; } = string.Empty;
    }
}

