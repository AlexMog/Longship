﻿namespace Longship.Events
{
    public abstract class RpcEvent : Event, ICancellable
    {
        public bool Cancelled { get; set; } = false;
        public long PlayerId { get; set; }

        public virtual void Construct(object[] parameters) {}
    }
}