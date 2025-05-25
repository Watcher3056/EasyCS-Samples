
using EasyCS.EventSystem;
using UnityEngine;

namespace EasyCS.Samples
{
    public class ActorBehaviorAI : ActorBehavior, IUpdate, IEventListener<EventDied>
    {
        [Bind]
        private EntityDataTarget _dataTarget;
        [Bind]
        private EntityDataMove _dataMove;
        [Bind]
        private EntityDataMoveSpeed _dataMoveSpeed;
        [Bind]
        private EntityDataAttack _dataAttack;

        public void OnUpdate(float deltaTime)
        {
            if (_dataTarget.Value.IsAlive)
            {
                Vector3 distanceToTarget = _dataTarget.Value.Actor.transform.position - transform.position;
                Vector3 move = distanceToTarget.normalized * _dataMoveSpeed.Value;

                if (distanceToTarget.magnitude > _dataAttack.attackRange)
                    _dataMove.Value = move;
                else
                    _dataMove.Value = Vector3.zero;
            }
        }

        public void HandleEvent(in EventContext<EventDied> ctx)
        {
            enabled = false;
        }
    }
}
