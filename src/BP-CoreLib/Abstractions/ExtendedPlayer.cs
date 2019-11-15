using BrokeProtocol.Entities;

namespace BPCoreLib.Abstractions
{
    public abstract class ExtendedPlayer
    {
        public ShPlayer Client { get; }

        protected ExtendedPlayer(ShPlayer player)
        {
            Client = player;
        }
    }
}
