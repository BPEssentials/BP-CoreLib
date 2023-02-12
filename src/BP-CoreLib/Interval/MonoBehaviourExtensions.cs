using System;
using UnityEngine;

namespace BPCoreLib.Interval
{
    public static class MonoBehaviourExtensions
    {
        public static Interval AttachInterval(this MonoBehaviour monoBehaviour, float time, Action action, bool start = true)
        {
            Interval interval = new Interval(time, action)
            {
                Container = monoBehaviour,
            };
            if (start)
            {
                interval.Start();
            }

            return interval;
        }
    }
}
