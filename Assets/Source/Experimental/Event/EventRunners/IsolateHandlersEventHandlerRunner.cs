using System;
using System.Collections.Generic;
using Omega.Tools.Experimental.Events;
using UnityEngine;

namespace Omega.Tools.Experimental.Event
{
    internal class IsolateHandlersEventHandlerRunner<TEvent> : IEvent
    {
        private readonly IEnumerable<IEventHandler<TEvent>> _handlers;
        private readonly TEvent _eventArg;
        private readonly Action<IEventHandler<TEvent>, Exception> _exceptionHandler;

        public IsolateHandlersEventHandlerRunner(IEnumerable<IEventHandler<TEvent>> handlers, TEvent arg,
            Action<IEventHandler<TEvent>, Exception> exceptionHandler)
        {
            _handlers = handlers;
            _eventArg = arg;
            _exceptionHandler = exceptionHandler ?? ((h, e) => Debug.LogException(e));
        }

        public void Release()
        {
            foreach (var handler in _handlers)
                try
                {
                    handler.Execute(_eventArg);
                }
                catch (Exception e)
                {
                    _exceptionHandler(handler, e);
                }
        }
    }
}