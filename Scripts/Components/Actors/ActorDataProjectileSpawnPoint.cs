using TriInspector;
using UnityEngine;

namespace EasyCS.Samples
{
    public class ActorDataProjectileSpawnPoint : ActorData
    {
        [field: SerializeField, Required]
        public Transform SpawnPoint { get; private set; }
    }
}