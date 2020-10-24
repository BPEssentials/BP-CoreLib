using BrokeProtocol.Entities;
using System;
using System.Collections.Generic;

namespace BPCoreLib.Interfaces
{
    public interface IActionLabel
    {
        string Name { get; }
        Action<ShPlayer, string> Action { get; }
    }
}