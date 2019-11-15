using BPCoreLib.Abstractions;
using BrokeProtocol.Entities;
using System.Collections.Generic;

namespace BPCoreLib.PlayerFactory
{
    public abstract class ExtendedPlayerFactory<T> where T : ExtendedPlayer
    {
        public Dictionary<int, T> Players { get; set; } = new Dictionary<int, T>();

        public abstract void AddOrReplace(ShPlayer player);

        public virtual bool Remove(int id) => Players.Remove(id);

        public virtual T GetUnsafe(int id) => Players[id];

        public virtual T GetSafe(int id)
        {
            if (!Players.TryGetValue(id, out var item))
            {
                return null;
            }
            return item;
        }

        public virtual bool TryGetSafe(int id, out T item)
        {
            return Players.TryGetValue(id, out item);
        }
    }
}
