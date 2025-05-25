using TriInspector;
using UnityEngine;

namespace EasyCS.Samples
{
    public class ActorDataRigidbody : ActorData
    {
        [field: SerializeField, Required]
        public Rigidbody Rigidbody { get; private set; }
    }
}
