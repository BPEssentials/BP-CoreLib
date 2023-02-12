using BrokeProtocol.Entities;
using System;
using System.Collections.Generic;

namespace BPCoreLib.Interfaces
{
    public interface ICooldownHandler
    {
        string CustomDataKey { get; }

        void AddCooldown(SvPlayer player, string key);

        double GetCooldown(SvPlayer player, string key, int delay);

        Dictionary<string, DateTimeOffset> FetchForUser(SvPlayer player);

        bool HasCooldown(SvPlayer player, string key, int delay);
    }
}
