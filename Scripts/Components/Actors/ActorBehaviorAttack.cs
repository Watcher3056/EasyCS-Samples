using EasyCS.EventSystem;
using EasyCS.Groups;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace EasyCS.Samples
{
    public class ActorBehaviorAttack : ActorBehavior, IUpdate, IEventListener<EventDied>
    {
        [Bind]
        private EntityDataTeam _dataTeam;
        [Bind]
        private EntityDataAttack _dataAttack;
        [Bind]
        private EntityDataTarget _dataTarget;
        [Bind]
        private ActorDataAnimator _dataAnimator;

        private IGroup _groupTargets;

        protected override void HandleAwake()
        {
            base.HandleAwake();

            GroupsSystem groupsSystem = EasyCsContainer.Resolve<GroupsSystem>();

            _groupTargets =
                CustomGroupBuilder
                    .Create(groupsSystem)
                    .WithComponent<EntityDataHealth>()
                    .WithComponent<EntityDataTeam>()
                    .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            UpdateTarget();

            if (_dataAttack.delayToNextAttack > 0f)
                _dataAttack.delayToNextAttack -= deltaTime;

            TryAttack();
        }

        private void UpdateTarget()
        {
            List<Actor> potentialTargets = ListPool<Actor>.Get();

            foreach (Entity entity in _groupTargets)
            {
                if (entity.GetComponent<EntityDataTeam>().Value != _dataTeam.Value)
                {
                    if (entity.GetComponent<EntityDataHealth>().Value > 0f)
                        potentialTargets.Add(entity.Actor);
                }
            }

            Actor nearestEnemy = Actor.FindNearestActor(potentialTargets);

            if (nearestEnemy != null)
            {
                _dataTarget.Value = nearestEnemy.Entity;
            }

            ListPool<Actor>.Release(potentialTargets);
        }

        private void TryAttack()
        {
            if (_dataTarget.Value.IsAlive == false)
                return;

            if (_dataAttack.delayToNextAttack > 0f)
                return;

            float distanceToTarget = Vector3.Distance(
                Entity.Actor.transform.position,
                _dataTarget.Value.Actor.transform.position);

            if (distanceToTarget > _dataAttack.attackRange)
                return;

            _dataAttack.delayToNextAttack = 1f / _dataAttack.attackSpeed;

            Entity.RaiseEvent(new EventTryAttack(_dataTarget.Value));
        }

        public void HandleEvent(in EventContext<EventDied> ctx)
        {
            enabled = false;
        }
    }
}
