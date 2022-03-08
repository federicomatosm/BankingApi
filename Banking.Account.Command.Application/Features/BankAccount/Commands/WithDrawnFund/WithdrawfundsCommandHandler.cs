using System;
using Banking.Account.Command.Application.Aggregates;
using Banking.Cqrs.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccount.Commands.WithDrawnFund
{
    public class WithdrawfundsCommandHandler : IRequestHandler<WithdrawFundsCommand, bool>
	{
        private readonly IEventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public WithdrawfundsCommandHandler(IEventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(WithdrawFundsCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventSourcingHandler.GetById(request.Id);
            aggregate.WithDrawFunds(request.Amount);
            await _eventSourcingHandler.Save(aggregate);

            return true;
        }
    }
}

