using UnityEngine;

namespace EasyCS.Samples
{
    public partial class ActorBehaviorProjectile : ActorBehavior
    {
        [Bind]
        private EntityDataAttackDamage _dataDamage;
        [Bind]
        private EntityDataTarget _dataTarget;

        private void OnTriggerEnter(Collider other)
        {
            Actor actor = other.gameObject.GetComponentInParent<Actor>();

            if (actor == null || actor.Entity.Equals(_dataTarget.Value) == false)
                return;

            EventSystem.Raise(new EventDealDamage { damage = _dataDamage.Value }, actor.Entity);

            Destroy(gameObject);
        }
    }
}