
using Banking.Cqrs.Core.Events;

namespace Banking.Cqrs.Core.Domain
{
	public abstract class AggregateRoot
	{
        public string Id { get; set; } = string.Empty;
        public int Version { get; set; } = -1;
        List<BaseEvent> changes = new();

        public int GetVersion()
        {
            return Version;
        }

        public void SetVersion(int version)
        {
            this.Version = version;
        }

        public List<BaseEvent> GetUncommitedChanges()
        {
            return changes;
        }

        public void MarkChangesAsCommited()
        {
            changes.Clear();
        }

        public void ApplyChange(BaseEvent @event, bool isNewEvent)
        {
            try
            {
                var EventType = @event.GetType();
                var method = GetType().GetMethod("Apply", new[] { EventType });
                method.Invoke(this, new object[] { @event });

            }catch(Exception ex)
            {

            }
            finally
            {
                if (isNewEvent)
                    changes.Add(@event);
            }
        }

        public void RaiseEvent(BaseEvent @event)
        {
            ApplyChange(@event, true);
        }

        public void ReplayEvents(IEnumerable<BaseEvent> events)
        {
            foreach(var ev in events)
            {
                ApplyChange(ev, false);
            }
        }
    }
}

