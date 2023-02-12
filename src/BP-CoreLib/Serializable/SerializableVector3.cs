using UnityEngine;

namespace BPCoreLib.Serializable
{
    /// <summary>
    /// A serializable version of the <see cref="Vector3"/> unity class. Can be used to, for example, store a vector in a JSON file.
    /// </summary>
    public class SerializableVector3
    {
        /// <inheritdoc cref="Vector3.x"/>
        public float X { get; set; }

        /// <inheritdoc cref="Vector3.y"/>
        public float Y { get; set; }

        /// <inheritdoc cref="Vector3.z"/>
        public float Z { get; set; }

        /// <inheritdoc cref="SerializableVector3"/>
        /// <param name="vector3">The <see cref="Vector3"/> to inherit.</param>
        public SerializableVector3(Vector3 vector3)
        {
            X = vector3.x;
            Y = vector3.y;
            Z = vector3.z;
        }

        /// <summary>
        /// Converts the internal values to a <see cref="Vector3"/>.
        /// </summary>
        /// <returns>A new <see cref="Vector3"/> with the respective values set.</returns>
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        /// <inheritdoc cref="ToVector3"/>
        public static implicit operator Vector3(SerializableVector3 serializableVector3)
        {
            return serializableVector3.ToVector3();
        }

        /// <inheritdoc cref="SerializableVector3"/>
        public static explicit operator SerializableVector3(Vector3 vector3)
        {
            return new SerializableVector3(vector3);
        }
    }
}
