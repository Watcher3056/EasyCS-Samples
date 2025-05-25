using UnityEngine;

namespace EasyCS.Samples
{
    public class ActorBehaviorPlayerInput : ActorBehavior, IUpdate
    {
        [Bind]
        private EntityDataMove _dataMove;
        [Bind]
        private EntityDataMoveSpeed _dataMoveSpeed;

        public void OnUpdate(float deltaTime)
        {
            Vector2 moveAxis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            _dataMove.Value = new Vector3(moveAxis.x, 0f, moveAxis.y) * _dataMoveSpeed.Value;
        }
    }
}
