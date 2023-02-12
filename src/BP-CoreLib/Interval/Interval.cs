using System;
using System.Collections;
using BrokeProtocol.Managers;
using UnityEngine;

namespace BPCoreLib.Interval
{
    //todo add monobehaviour .attachinterval extension method
    /// <summary>
    /// Used as a simple way to start a coroutine based on a interval.
    /// </summary>
    public class Interval
    {
        /// <summary>
        /// The interval between actions. This value should be expressed in seconds, but can contain decimals. (eg. 0.5f for 500ms)
        /// </summary>
        public float Time { get; set; }

        /// <summary>
        /// The maximum amount of times this interval will be called. Set to -1 for infinite.
        /// </summary>
        public int MaxIterations { get; set; } = -1;

        /// <summary>
        /// The container from which the coroutine will be started. This is usually the <see cref="SvManager"/> instance.
        /// </summary>
        public MonoBehaviour Container { get; set; } = SvManager.Instance;
        
        /// <summary>
        /// The actual action that will be called. This action will be called every <see cref="Time"/> seconds.
        /// </summary>
        /// <remarks>
        /// It is not recommended to change this value while the interval is running. If you need to change the action, you should stop, change, and start the interval again.
        /// </remarks>
        public Action Action { get; set; }
        
        /// <summary>
        /// Determines if the interval is currently running.
        /// </summary>
        public bool IsRunning { get; private set; }
        
        /// <summary>
        /// The current iteration of the interval. This value will be reset to 0 when the interval is started, and will be incremented every time the action is ran.
        /// </summary>
        public int Iteration { get; private set; }
        
        /// <summary>
        /// Fetches how long the interval has been running for. This is simply calculated by multiplying the <see cref="Iteration"/> by the <see cref="Time"/> value.
        /// </summary>
        public float RanFor => Iteration * Time;
        
        /// <summary>
        /// Determines the remaining iterations of the interval. This is calculated by subtracting the <see cref="Iteration"/> from the <see cref="MaxIterations"/> value.
        /// This value is -1 if the interval is infinite.
        /// </summary>
        public int RemainingIterations => IsInfinite ? -1 : MaxIterations - Iteration;
        
        /// <summary>
        /// Determines if this interval has been marked as infinite.
        /// </summary>
        public bool IsInfinite => MaxIterations < 0;
        
        /// <summary>
        /// Determines if this current iteration is the last iteration.
        /// Can be used to invoke cleanup code.
        /// </summary>
        public bool IsLastIteration => Iteration == MaxIterations;

        /// <inheritdoc cref="Interval"/>
        public Interval(float time, Action action)
        {
            Time = time;
            Action = action;
        }

        /// <summary>
        /// Starts the interval. If the interval is already running, the interval will be stopped and restarted.
        /// </summary>
        public void Start()
        {
            Stop();
            Container.StartCoroutine(StartEnumerator());
        }
        
        /// <summary>
        /// Stops the interval.
        /// </summary>
        public void Stop()
        {
            Container.StopCoroutine(StartEnumerator());
            IsRunning = false;
        }

        /// <summary>
        /// Internal enumerator used for the interval. While this should not to be called directly, but instead through <see cref="Start"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator StartEnumerator()
        {
            IsRunning = true;
            for (Iteration = 0; IsInfinite || Iteration < MaxIterations; Iteration++)
            {
                Action.Invoke();
                yield return new WaitForSecondsRealtime(Time);
            }
        }
        
        /// <summary>
        /// Starts a new interval and returns the newly created (and started) instance.
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Interval StartNew(int interval, Action action)
        {
            Interval instance = new Interval(interval, action);
            instance.Start();
            return instance;
        }
    }
}
