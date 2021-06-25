using BrokeProtocol.Entities;
using System;

namespace BPCoreLib.Interfaces
{
    public interface IActionLabel
    {
        string Name { get; }
        Action<ShPlayer, string> Action { get; }
    }
}