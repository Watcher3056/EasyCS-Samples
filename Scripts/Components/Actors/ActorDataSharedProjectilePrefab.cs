
using System;
using TriInspector;
using UnityEngine;

namespace EasyCS.Samples
{
    [Serializable]
    public class ActorDataSharedProjectilePrefab : ActorDataSharedBase
    {
        [field: SerializeField, Required]
        public PrefabRootData Prefab { get; private set; }
        [field: SerializeField]
        public float ProjectileSpeed { get; private set; }
    }
}
