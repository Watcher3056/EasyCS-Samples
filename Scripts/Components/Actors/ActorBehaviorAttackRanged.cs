using UnityEngine;
using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    public class ActorBehaviorAttackRanged : ActorBehavior, IEventListener<EventTryAttack>
    {
        [Bind]
        private EntityDataAttack _dataAttack;
        [Bind]
        private EntityDataAttackDamage _dataDamage;
        [Bind]
        private EntityDataTarget _dataTarget;
        [Bind]
        private ActorDataProjectileSpawnPoint _projectileSpawnPoint;
        [Bind]
        private ActorDataSharedProjectilePrefab _sharedProjectilePrefab;

        public void HandleEvent(in EventContext<EventTryAttack> ctx)
        {
            ExecuteAttack();
        }

        private void ExecuteAttack()
        {
            Actor actorProjectile = EasyCsContainer.InstantiateWithEntity(
                _sharedProjectilePrefab.Prefab,
                _projectileSpawnPoint.SpawnPoint.position,
                _projectileSpawnPoint.SpawnPoint.rotation)
                .GetComponent<Actor>();

            Vector3 direction = (_dataTarget.Value.Actor.transform.position - transform.position).normalized;

            actorProjectile.Entity
                .TryGetOrAddData<EntityDataAttackDamage>().Value = _dataDamage.Value;
            actorProjectile.Entity
                .TryGetOrAddData<EntityDataTarget>().Value = _dataTarget.Value;
        }
    }
}
