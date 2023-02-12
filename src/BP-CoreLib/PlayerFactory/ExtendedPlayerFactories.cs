using System;
using System.Collections.Generic;
using BrokeProtocol.Entities;

namespace BPCoreLib.PlayerFactory
{
    public class ExtendedPlayerFactories
    {
        public static ExtendedPlayerFactories Instance { get; } = new ExtendedPlayerFactories();

        public Dictionary<Type, IExtendedPlayerFactory> Factories { get; } = new Dictionary<Type, IExtendedPlayerFactory>();
        
        public void Register<T>(IExtendedPlayerFactory factory) where T : ExtendedPlayer
        {
            Factories.Add(typeof(T), factory);
        }
        
        public void Register<T>() where T : ExtendedPlayer, new()
        {
            Factories.Add(typeof(T), new GenericPlayerFactory<T>());
        }
        
        internal void AddPlayer(ShPlayer player)
        {
            foreach (IExtendedPlayerFactory factory in Factories.Values)
            {
                factory.AddOrReplace(player);
            }
        }

        internal void Remove(ShPlayer player)
        {
            foreach (IExtendedPlayerFactory factory in Factories.Values)
            {
                factory.Remove(player.ID);
            }
        }
    }
}
