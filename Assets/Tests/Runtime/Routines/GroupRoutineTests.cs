using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Omega.Experimental;
using UnityEngine;
using UnityEngine.TestTools;

namespace Omega.Routines.Tests
{
    public class GroupRoutineTests
    {
        [UnityTest]
        public IEnumerator RoutineShouldCompleteWhenHisRoutinesIsCompletedTest()
        {
            var routineWithDelay160Ms = Routine.Delay(Utilities.Time.FromMilliseconds(160));
            var routineWithDelay150Ms = Routine.Delay(Utilities.Time.FromMilliseconds(150));

            var startTestTime = DateTime.Now;

            yield return Routine.WhenAll(routineWithDelay160Ms, routineWithDelay150Ms);

            var deltaTime = DateTime.Now - startTestTime;

            Assert.Greater(Utilities.Time.FromMilliseconds(160 + 150), deltaTime.TotalSeconds);
        }

        [UnityTest]
        public IEnumerator RoutineShouldProcessNestedTest()
        {
            var routines = Enumerable.Range(0, 2).Select(e => new RoutineTest()).ToArray();
            yield return new GroupRoutine(routines);
            Assert.True(routines.All(e => e.Flag));
        }

        private class RoutineTest : Routine
        {
            public bool Flag;

            protected override IEnumerator RoutineUpdate()
            {
                yield return Task(() =>
                {
                    Thread.Sleep(50);
                    Flag = true;
                });
            }
        }
    }
}