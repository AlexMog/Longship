using System;
using System.Collections.Generic;
using Longship.Events;

namespace Longship.Managers
{
    public class EventManager
    {
        public delegate void EventListener<in T>(T e) where T : Event;
        private readonly Dictionary<Type, List<EventListener<Event>>> _eventListeners;

        public void RegisterListener<T>(EventListener<T> listener) where T : Event
        {
            List<EventListener<Event>> eventListeners;
            if (_eventListeners.TryGetValue(typeof(T), out var list))
            {
                eventListeners = list;
            }
            else
            {
                eventListeners = new List<EventListener<Event>>();
                _eventListeners[typeof(T)] = eventListeners;
            }
            eventListeners.Add((EventListener<Event>) listener);
        }
        
        public void DispatchEvent(Event e)
        {
            // TODO Move this part to a "DispatcherStrategy" class
            if (!_eventListeners.TryGetValue(e.GetType(), out var list)) return;
            foreach (var listener in list)
            {
                listener.Invoke(e);
            }
        }
    }
}