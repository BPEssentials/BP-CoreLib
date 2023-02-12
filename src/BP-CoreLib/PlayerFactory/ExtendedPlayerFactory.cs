using BrokeProtocol.Entities;
using System.Collections.Generic;

namespace BPCoreLib.PlayerFactory
{
    public class GenericPlayerFactory<T> : ExtendedPlayerFactory<T> where T : ExtendedPlayer, new()
    {
        public override void AddOrReplace(ShPlayer player)
        {
            this[player.ID] = new T
            {
                Client = player,
            };
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExtendedPlayerFactory<T> : Dictionary<int, T>, IExtendedPlayerFactory where T : ExtendedPlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        public abstract void AddOrReplace(ShPlayer player);

        public T1 GetSafe<T1>(int key) where T1 : ExtendedPlayer
        {
            return GetSafe(key) as T1;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T GetSafe(int key)
        {
            if (!TryGetValue(key, out T item))
            {
                return default;
            }

            return item;
        }
    }
}
