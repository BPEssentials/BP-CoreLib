using System;
using BrokeProtocol.Entities;

namespace BPCoreLib.Trigger
{
    public class PlayerEventArgs : EventArgs
    {
        public ShTrigger ShTrigger { get; }

        public ShPlayer Client { get; }

        public PlayerEventArgs(ShTrigger shTrigger, ShPlayer client)
        {
            ShTrigger = shTrigger;
            Client = client;
        }
    }
}
