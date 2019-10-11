using System;
using System.Collections.Generic;
using System.Reflection;
using Omega.Tools.Experimental.Event;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Omega.Tools.Experimental.Events.Internals.EventManagers
{
    internal partial class UniversalEventManager<TEvent> : IEventManager<TEvent>
    {
        
        private List<IEventHandler<TEvent>> _eventHandlers;

        public UniversalEventManager()
        {
            _eventHandlers = new List<IEventHandler<TEvent>>();
        }

        public void Event(TEvent arg)
        {
            var handlersOfEvent = _eventHandlers.ToArray();

            var @event = EventBuilder.CreateEvent(handlersOfEvent, arg);
            
            EventScheduler.Schedule(@event);
        }

        public void AddHandler(IEventHandler<TEvent> handler)
        {
            _eventHandlers.Add(handler);
        }

        public void RemoveHandler(IEventHandler<TEvent> handler)
        {
            _eventHandlers.Remove(handler);
        }
    }
}