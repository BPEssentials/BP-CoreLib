using BrokeProtocol.Entities;

namespace BPCoreLib.PlayerFactory
{
    public static class Extensions
    {
        public static T GetExtended<T>(this ShPlayer player) where T : ExtendedPlayer
        {
            return player.svPlayer.GetExtended<T>();
        }

        public static T GetExtended<T>(this SvPlayer player) where T : ExtendedPlayer
        {
            if (!ExtendedPlayerFactories.Instance.Factories.TryGetValue(typeof(T), out IExtendedPlayerFactory factory))
            {
                return default;
            }

            return factory.GetSafe<T>(player.player.ID);
        }
    }
}
