﻿using System;
using Banking.Account.Command.Application.Features.BankAccount.Commands.OpenAccount;
using Banking.Cqrs.Core.Domain;
using Banking.Cqrs.Core.Events;

namespace Banking.Account.Command.Application.Aggregates
{
	public class AccountAggregate : AggregateRoot
	{
        public bool Active { get; set; }
        public double Balance { get; set; }

        public AccountAggregate()
        {

        }

        public AccountAggregate(OpenAccountCommand command)
        {
            var accountOpenedEvent = new AccountOpenedEvent(
               command.Id,
               command.AccountHolder,
               command.AccountType,
               DateTime.Now,
               command.OpeningBalance
                );

            RaiseEvent(accountOpenedEvent);
        }

        public void Apply(AccountOpenedEvent @event)
        {
            Id = @event.Id;
            Active = true;
            Balance = @event.OpeningBalance;
               
        }

        public void DepositFunds(double amount)
        {
            if (!Active)
            {
                throw new Exception("Cannot deposit funds to an inactive account");
            }

            if (amount <= 0)
            {
                throw new Exception("The amount to deposit must be greater than 0");
            }

            var fundsDepositEvent = new FundsDepositedEvent(Id)
            {
                Id = Id,
                Amount = amount
            };

            RaiseEvent(fundsDepositEvent);
        }

        public void Apply(FundsDepositedEvent @event)
        {
            Id = @event.Id;
            Balance += @event.Amount;
        }

        public void WithDrawFunds(double amount)
        {
            if (!Active)
            {
                throw new Exception("Cannot withdraw funds to an inactive account");
            }

            if (amount <= 0)
            {
                throw new Exception("The amount to withdraw must be greater than 0");
            }

            var fundsWithDrawEvent = new FundsWithdrawnEvent(Id)
            {
                Id = Id,
                Amount = amount
            };

            RaiseEvent(fundsWithDrawEvent);
        }

        public void Apply(FundsWithdrawnEvent @event)
        {

            Id = @event.Id;
            Balance -= @event.Amount;
        }

        public void CloseAccount()
        {
            if (!Active)
            {
                throw new Exception("The account is inactive");
            }

            var accountClosedEvent = new AccountClosedEvent(Id);
            RaiseEvent(accountClosedEvent);
        }

        public void Apply(AccountClosedEvent @event)
        {
            Id = @event.Id;
            Active = false;
        }
    }
}

