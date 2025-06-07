using TriInspector;
using UnityEngine;

namespace EasyCS.Samples
{
    public partial class ActorDataProjectileSpawnPoint : ActorData
    {
        [field: SerializeField, Required]
        public Transform SpawnPoint { get; private set; }
    }
}