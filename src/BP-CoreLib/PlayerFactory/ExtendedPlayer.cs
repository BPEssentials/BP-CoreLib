using BrokeProtocol.Entities;

namespace BPCoreLib.PlayerFactory
{
    /// <summary>
    /// Represents a class that can be used to create a extended instance of a player.
    /// This is particularly useful for creating custom player classes that store extra data such as properties and methods.
    /// </summary>
    public abstract class ExtendedPlayer
    {
        /// <summary>
        /// The player that this extended player is based on.
        /// </summary>
        public ShPlayer Client { get; internal set; }

        /// <inheritdoc cref="ExtendedPlayer"/>
        /// <param name="player">The player instance.</param>
        protected ExtendedPlayer(ShPlayer player)
        {
            Client = player;
        }
    }
}
