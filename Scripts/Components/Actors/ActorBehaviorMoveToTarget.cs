

using UnityEngine;

namespace EasyCS.Samples
{
    public partial class ActorBehaviorMoveToTarget : ActorBehavior, IUpdate
    {
        [Bind]
        private EntityDataTarget _dataTarget;
        [Bind]
        private EntityDataMove _dataMove;
        [Bind]
        private EntityDataMoveSpeed _dataMoveSpeed;

        public void OnUpdate(float deltaTime)
        {
            if (_dataTarget.Value.IsAlive)
            {
                Vector3 distanceToTarget = _dataTarget.Value.Actor.transform.position - transform.position;
                Vector3 move = distanceToTarget.normalized * _dataMoveSpeed.Value;
                _dataMove.Value = move;
            }
        }
    }
}
