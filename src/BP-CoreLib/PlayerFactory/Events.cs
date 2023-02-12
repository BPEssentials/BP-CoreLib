using BrokeProtocol.API;
using BrokeProtocol.Entities;

namespace BPCoreLib.PlayerFactory
{
    public class Events : IScript
    {
        [Target(GameSourceEvent.PlayerInitialize, ExecutionMode.Event)]
        public void PlayerInitialize(ShPlayer player)
        {
            if (!player.isHuman)
            {
                return;
            }
            
            ExtendedPlayerFactories.Instance.AddPlayer(player);
        }

        [Target(GameSourceEvent.PlayerDestroy, ExecutionMode.Event)]
        public void PlayerDestroy(ShPlayer player)
        {
            if (!player.isHuman)
            {
                return;
            }

            ExtendedPlayerFactories.Instance.Remove(player);
        }
    }
}
