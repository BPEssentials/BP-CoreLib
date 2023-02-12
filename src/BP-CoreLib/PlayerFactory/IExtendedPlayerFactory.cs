using BrokeProtocol.Entities;

namespace BPCoreLib.PlayerFactory
{
    public interface IExtendedPlayerFactory
    {
        void AddOrReplace(ShPlayer player);

        bool Remove(int key);
        
        T1 GetSafe<T1>(int key) where T1 : ExtendedPlayer;
    }
}
