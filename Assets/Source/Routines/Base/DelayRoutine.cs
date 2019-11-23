using System;
using System.Collections;
using UnityEngine;

namespace Omega.Routines
{
    public sealed class DelayRoutine : Routine, IProgressRoutineProvider
    {
        private readonly TimeSpan _delayInterval;
        private DateTime _releaseTimeSeconds;

        public TimeSpan DelayInterval => _delayInterval;

        internal DelayRoutine(TimeSpan delayInterval)
        {
            _delayInterval = delayInterval;
        }

        protected override IEnumerator RoutineUpdate()
        {
            _releaseTimeSeconds = DateTime.UtcNow + _delayInterval;

            while (DateTime.UtcNow < _releaseTimeSeconds)
                yield return null;
        }

        public float GetProgress()
        {
            var startedIn = _releaseTimeSeconds - _delayInterval;
            var delta = DateTime.UtcNow - startedIn;

            var unclampedProgress = (float) (delta.TotalSeconds / _delayInterval.TotalSeconds);
            
            return Mathf.Clamp01(unclampedProgress);
        }
    }
}