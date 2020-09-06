using BPCoreLib.Interfaces;
using BrokeProtocol.Entities;
using System;
using System.Collections.Generic;

namespace BPCoreLib.Util
{
    public class CooldownHandler : ICooldownHandler
    {
        public string CustomDataKey { get; private set; }

        public CooldownHandler(string customDataKey)
        {
            CustomDataKey = customDataKey;
        }

        public void AddCooldown(SvPlayer player, string key)
        {
            var PlayerCooldowns = GetCooldowns(player);
            if (PlayerCooldowns == null)
            {
                PlayerCooldowns = new Dictionary<string, DateTimeOffset>();
            }
            PlayerCooldowns.Add(key, DateTimeOffset.Now);
            player.CustomData.AddOrUpdate(CustomDataKey, PlayerCooldowns);
        }

        public bool IsCooldown(SvPlayer player, string key, int delay)
        {
            return GetCooldown(player, key, delay) > 0;
        }

        public Dictionary<string, DateTimeOffset> GetCooldowns(SvPlayer player)
        {
            return player.CustomData.FetchCustomData<Dictionary<string, DateTimeOffset>>(CustomDataKey);
        }

        public double GetCooldown(SvPlayer player, string key, int delay)
        {
            var PlayerCooldowns = GetCooldowns(player);
            if (PlayerCooldowns == null || !PlayerCooldowns.ContainsKey(key)) { return 0; }
            return Math.Max(0, (PlayerCooldowns[key].AddSeconds(delay) - DateTimeOffset.Now).TotalSeconds);
        }
    }
}
