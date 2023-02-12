using UnityEngine;

namespace BPCoreLib.Serializable
{
    /// <summary>
    /// A serializable version of the <see cref="Quaternion"/> unity class. Can be used to, for example, store a quaternion in a JSON file.
    /// </summary>
    public class SerializableQuaternion
    {
        /// <inheritdoc cref="Quaternion.x"/>
        public float X { get; set; }

        /// <inheritdoc cref="Quaternion.y"/>
        public float Y { get; set; }

        /// <inheritdoc cref="Quaternion.z"/>
        public float Z { get; set; }

        /// <inheritdoc cref="Quaternion.w"/>
        public float W { get; set; }

        /// <inheritdoc cref="SerializableQuaternion"/>
        /// <param name="quaternion">>The <see cref="Quaternion"/> to inherit.</param>
        public SerializableQuaternion(Quaternion quaternion)
        {
            X = quaternion.x;
            Y = quaternion.y;
            Z = quaternion.z;
            W = quaternion.w;
        }

        /// <summary>
        /// Converts the internal values to a <see cref="Quaternion"/>.
        /// </summary>
        /// <returns>A new <see cref="Quaternion"/> with the respective values set.</returns>
        public Quaternion ToQuaternion()
        {
            return new Quaternion(X, Y, Z, W);
        }

        /// <inheritdoc cref="ToQuaternion"/>
        public static implicit operator Quaternion(SerializableQuaternion serializableQuaternion)
        {
            return serializableQuaternion.ToQuaternion();
        }

        /// <inheritdoc cref="SerializableQuaternion"/>
        public static explicit operator SerializableQuaternion(Quaternion quaternion)
        {
            return new SerializableQuaternion(quaternion);
        }
    }
}
