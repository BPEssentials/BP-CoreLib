using System;
using System.Collections.Generic;
using BrokeProtocol.API;
using BrokeProtocol.Entities;

namespace BPCoreLib.Trigger
{
    public class TriggerHandler
    {
        public static TriggerHandler Instance { get; } = new TriggerHandler();
        
        public Dictionary<string, List<Trigger>> RegisteredTriggers { get; } = new Dictionary<string, List<Trigger>>();

        public void Register(Trigger trigger)
        {
            if (RegisteredTriggers.TryGetValue(trigger.Event, out List<Trigger> triggers))
            {
                triggers.Add(trigger);
                return;
            }

            RegisteredTriggers[trigger.Event] = new List<Trigger> { trigger };
            RegisterEvent(trigger);
        }

        private void RegisterEvent(Trigger trigger)
        {
            EventsHandler.Add(trigger.EnterEvent, new Action<ShEntity, ShPhysical>((entity, physical) => OnEvent(trigger.Event, false, entity, physical)));
            EventsHandler.Add(trigger.ExitEvent, new Action<ShEntity, ShPhysical>((entity, physical) => OnEvent(trigger.Event, true, entity, physical)));
        }

        private void OnEvent(string @event, bool isExitEvent, ShEntity entity, ShPhysical physical)
        {
            if (!RegisteredTriggers.TryGetValue(@event, out List<Trigger> triggers))
            {
                return;
            }
            if (!(physical is ShPlayer player))
            {
                return;
            }
            if (!(entity is ShTrigger shTrigger))
            {
                return;
            }

            foreach (Trigger trigger in triggers)
            {
                if (isExitEvent)
                {
                    trigger.OnExitTrigger(shTrigger, player);
                }
                else
                {
                    trigger.OnEnterTrigger(shTrigger, player);
                }
            }
        }
    }
}