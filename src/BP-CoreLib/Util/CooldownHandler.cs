using BPCoreLib.Interfaces;
using BrokeProtocol.Entities;
using System;
using System.Collections.Generic;

namespace BPCoreLib.Util
{
    // TODO: refactor to DateTime, I don't see a reason to use DateTimeOffset
    //       Do note that this would break existing saved cooldowns, so a different key needs to be picked in BPE.
    public class CooldownHandler : ICooldownHandler
    {
        public string CustomDataKey { get; private set; }

        public CooldownHandler(string customDataKey)
        {
            CustomDataKey = customDataKey;
        }

        /// <summary>
        /// Fetches the persisted cooldowns from the player's custom data using the instance custom data key.
        /// </summary>
        /// <param name="player">The player in question.</param>
        /// <returns>A full list of cooldowns based on the custom data key.</returns>
        public Dictionary<string, DateTimeOffset> FetchForUser(SvPlayer player)
        {
            return player.CustomData.FetchCustomData<Dictionary<string, DateTimeOffset>>(CustomDataKey);
        }

        /// <summary>
        /// Adds a cooldown to the player (or updates an existing one) with the current time, in UTC.
        /// The value that gets saved in the dictionary is the UTC time when this method is ran. When calling <see cref="HasCooldown"/>, the provided delay is compared to this value.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="key"></param>
        public void AddCooldown(SvPlayer player, string key)
        {
            Dictionary<string, DateTimeOffset> playerCooldowns = FetchForUser(player);
            playerCooldowns = playerCooldowns ?? new Dictionary<string, DateTimeOffset>();
            playerCooldowns[key] = DateTimeOffset.UtcNow;
            player.CustomData.AddOrUpdate(CustomDataKey, playerCooldowns);
        }

        public bool HasCooldown(SvPlayer player, string key, int delay)
        {
            return GetCooldown(player, key, delay) > 0;
        }
        
        public bool HasCooldown(SvPlayer player, string key, int delay, out double cooldownInSeconds)
        {
            cooldownInSeconds = GetCooldown(player, key, delay);
            return cooldownInSeconds > 0;
        }

        public double GetCooldown(SvPlayer player, string key, int delay)
        {
            Dictionary<string, DateTimeOffset> playerCooldowns = FetchForUser(player);
            playerCooldowns = playerCooldowns ?? new Dictionary<string, DateTimeOffset>();
            if (!playerCooldowns.TryGetValue(key, out DateTimeOffset playerCooldown))
            {
                return 0;
            }

            return Math.Max(0, (playerCooldown.AddSeconds(delay) - DateTimeOffset.UtcNow).TotalSeconds);
        }
    }
}
