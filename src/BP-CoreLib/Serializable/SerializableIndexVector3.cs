using UnityEngine;

namespace BPCoreLib.Serializable
{
    /// <summary>
    /// Inherits <see cref="SerializableVector3"/>, but also includes a property for the place index. Useful for say, fixed placed vectors.
    /// </summary>
    public class SerializableIndexVector3 : SerializableVector3
    {
        /// <summary>
        /// The place index of the vector.
        /// </summary>
        public int PlaceIndex { get; set; }

        /// <inheritdoc cref="SerializableVector3"/>
        /// <param name="vector3">The <see cref="Vector3"/> to inherit.</param>
        /// <param name="placeIndex">The place index to inherit.</param>
        public SerializableIndexVector3(Vector3 vector3, int placeIndex) : base(vector3)
        {
            PlaceIndex = placeIndex;
        }
    }
}
