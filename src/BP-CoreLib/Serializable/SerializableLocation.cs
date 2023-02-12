using UnityEngine;

namespace BPCoreLib.Serializable
{
    /// <summary>
    /// A serializable location, which contains everything you need to save. A location is a point in space (including place index), with a rotation.
    /// </summary>
    public class SerializableLocation
    {
        /// <inheritdoc cref="SerializableIndexVector3"/>
        public SerializableIndexVector3 Position { get; set; }

        /// <inheritdoc cref="SerializableQuaternion"/>
        public SerializableQuaternion Rotation { get; set; }
        
        /// <inheritdoc cref="SerializableLocation"/>
        public SerializableLocation()
        {
        }

        /// <inheritdoc cref="SerializableLocation"/>
        /// <param name="position">The vector3 position to save. Passed to <see cref="SerializableIndexVector3"/>.</param>
        /// <param name="rotation">The quaternion rotation to save. Passed to <see cref="SerializableQuaternion"/>.</param>
        /// <param name="placeIndex">The place index to save. Defaults to 0. Passed to <see cref="SerializableIndexVector3"/>.</param>
        public SerializableLocation(Vector3 position, Quaternion rotation, int placeIndex = 0)
        {
            Position = new SerializableIndexVector3(position, placeIndex);
            Rotation = new SerializableQuaternion(rotation);
        }
    }
}
