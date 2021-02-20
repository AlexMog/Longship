using System;
using System.Collections.Generic;
using Longship.Events;
using Longship.Plugins;

namespace Longship.Managers
{
    public class EventManager : Manager
    {
        public delegate void EventListener<in T>(T e) where T : Event;
        private readonly Dictionary<Type, List<EventListener<Event>>> _eventListeners =
            new Dictionary<Type, List<EventListener<Event>>>();
        private readonly Dictionary<IPlugin, List<ListenedEvent>> _pluginListeners =
            new Dictionary<IPlugin, List<ListenedEvent>>();

        public override void Init() {}

        public void RegisterListener<T>(IPlugin plugin, EventListener<T> listener) where T : Event
        {
            if (!_eventListeners.TryGetValue(typeof(T), out var eventListeners))
            {
                eventListeners = new List<EventListener<Event>>();
                _eventListeners[typeof(T)] = eventListeners;
            }

            if (!_pluginListeners.TryGetValue(plugin, out var listeners))
            {
                listeners = new List<ListenedEvent>();
                _pluginListeners[plugin] = listeners;
            }
            listeners.Add(new ListenedEvent() {
                eventType = typeof(T),
                eventListener = (EventListener<Event>) listener
            });
            eventListeners.Add((EventListener<Event>) listener);
        }

        public void ClearListeners(IPlugin plugin)
        {
            if (!_pluginListeners.TryGetValue(plugin, out var events)) return;
            foreach (var listenedEvent in events)
            {
                var listeners = _eventListeners[listenedEvent.eventType];
                listeners.Remove(listenedEvent.eventListener);
                if (listeners.Count == 0)
                {
                    _eventListeners.Remove(listenedEvent.eventType);
                }
            }

            _pluginListeners.Remove(plugin);
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

        private class ListenedEvent
        {
            public Type eventType;
            public EventListener<Event> eventListener;
        }
    }
}