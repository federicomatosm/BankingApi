using System;
namespace Banking.Cqrs.Core.Events
{
	public class FoundsWithdrawnEvent : BaseEvent
	{
        public double Amount { get; set; }

        public FoundsWithdrawnEvent(string id) : base(id)
        {
        }
    }
}

