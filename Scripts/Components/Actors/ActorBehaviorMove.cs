
using EasyCS.EventSystem;
using UnityEngine;

namespace EasyCS.Samples
{
    public partial class ActorBehaviorMove : ActorBehavior, IFixedUpdate, IEventListener<EventDied>
    {
        [Bind]
        private EntityDataMove _dataMove;
        [Bind]
        private ActorDataRigidbody _dataRigidbody;


        public void OnFixedUpdate(float deltaTime)
        {
            Vector3 nextPosition = Actor.transform.position + _dataMove.Value * deltaTime;

            if (_dataMove.Value.magnitude > 0f)
            {
                Quaternion rotation = Quaternion.LookRotation(_dataMove.Value.normalized, Vector3.up);
                _dataRigidbody.Rigidbody.MoveRotation(rotation);
                _dataRigidbody.Rigidbody.MovePosition(nextPosition);
            }
        }

        public void HandleEvent(in EventContext<EventDied> ctx)
        {
            enabled = false;
        }
    }
}
