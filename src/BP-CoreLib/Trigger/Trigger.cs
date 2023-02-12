using System;
using BPCoreLib.PlayerFactory;
using BPCoreLib.ExtensionMethods;
using BPCoreLib.Interval;
using BrokeProtocol.Entities;

namespace BPCoreLib.Trigger
{
    class GasHandler
    {
        public Trigger Trigger { get; } = new Trigger("myplugin::gas");
        
        public GasHandler()
        {
            Trigger.OnEnter += OnEnter;
            Trigger.OnExit += OnExit;
        }

        public void OnEnter(object _, PlayerEventArgs args)
        {
            MyPluginPlayer player = args.Client.GetExtended<MyPluginPlayer>();
            
            // TODO send message to player
            if (player.GasInterval != null)
            {
                player.GasInterval.Start();
                return;
            }

            player.GasInterval = player.Client.svPlayer.AttachInterval(1, player.OnGasInterval);
        }

        public void OnExit(object _, PlayerEventArgs args)
        {
            MyPluginPlayer player = args.Client.GetExtended<MyPluginPlayer>();
            
            player.GasInterval?.Stop();
        }
    }

    class MyPluginPlayer : ExtendedPlayer
    {
        public Interval.Interval GasInterval { get; set; }

        public MyPluginPlayer(ShPlayer player) : base(player)
        {
        }
        
        public void OnGasInterval()
        {
            Client.svPlayer.Damage(5f);
        }
    }

    public class Trigger
    {
        public string Event { get; }
        
        public string EnterEvent => $"{Event}.enter";

        public string ExitEvent => $"{Event}.exit";
        
        // better delegate class, trigger and shplayer as args
        public event EventHandler<PlayerEventArgs> OnEnter;

        public event EventHandler<PlayerEventArgs> OnExit;
        
        public Trigger(string @event)
        {
            Event = @event;
        }
        
        internal void OnEnterTrigger(ShTrigger shTrigger, ShPlayer player)
        {
            OnEnter?.Invoke(this, new PlayerEventArgs(shTrigger, player));
        }
        
        internal void OnExitTrigger(ShTrigger shTrigger, ShPlayer player)
        {
            OnExit?.Invoke(this, new PlayerEventArgs(shTrigger, player));
        }
    }
}
