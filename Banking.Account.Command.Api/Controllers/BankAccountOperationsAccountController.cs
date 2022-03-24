

using System.Net;
using Banking.Account.Command.Application.Features.BankAccount.Commands.CloseAccount;
using Banking.Account.Command.Application.Features.BankAccount.Commands.DepositFund;
using Banking.Account.Command.Application.Features.BankAccount.Commands.OpenAccount;
using Banking.Account.Command.Application.Features.BankAccount.Commands.WithDrawnFund;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Account.Command.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BankAccountOperationsAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankAccountOperationsAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("OpenAccount", Name ="OpenAcount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> OpenAccount(OpenAccountCommand command)
        {
            var id = Guid.NewGuid().ToString();
            command.Id = id;

            return await _mediator.Send(command);
        }

        [HttpDelete("CloseAccount/{id}", Name ="CloseAccount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult<bool>> CloseAccount(string id)
        {
            var command = new CloseAccountCommand
            {
                Id = id
            };

            return await _mediator.Send(command);
        }

        [HttpPut("DepositFund/{id}", Name = "DepositFund")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DepositFund(string id, [FromBody] DepositFundsCommand command)
        {
            command.Id = id;

            return await _mediator.Send(command);
        }

        [HttpPut("WithdrawnFund/{id}", Name = "WithdrawnFund")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> WithdrawnFund(string id, [FromBody] WithdrawFundsCommand command)
        {
            command.Id = id;

            return await _mediator.Send(command);
        }

    }
}

