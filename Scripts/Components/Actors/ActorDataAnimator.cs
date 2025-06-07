using TriInspector;
using UnityEngine;

namespace EasyCS.Samples
{
    public partial class ActorDataAnimator : ActorData
    {
        [field: SerializeField, Required]
        public Animator Animator { get; private set; }
    }
}
