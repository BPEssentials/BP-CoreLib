using BPCoreLib.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace BPCoreLib.Util
{
    public static class Interval
    {
        public static IEnumerator Start(int time, Action action)
        {
            while (true)
            {
                action.Invoke();
                yield return new WaitForSecondsRealtime(time);
            }
        }

        public static IEnumerator Start(int time, Action action, uint repeat)
        {
            for (; repeat > 0; repeat--)
            {
                action.Invoke();
                yield return new WaitForSecondsRealtime(time);
            }
        }
    }
}
