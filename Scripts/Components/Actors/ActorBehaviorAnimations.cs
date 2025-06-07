using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    public partial class ActorBehaviorAnimations : ActorBehavior, IUpdate, IEventListener<EventTryAttack>, IEventListener<EventDied>
    {
        [Bind]
        private EntityDataMove _dataMove;
        [Bind]
        private ActorDataAnimator _dataAnimator;

        public void OnUpdate(float deltaTime)
        {
            if (_dataMove.Value.magnitude > 0)
                _dataAnimator.Animator.SetBool("IsMoving", true);
            else
                _dataAnimator.Animator.SetBool("IsMoving", false);
        }

        public void HandleEvent(in EventContext<EventTryAttack> ctx)
        {
            _dataAnimator.Animator.SetTrigger("Attack");
        }

        public void HandleEvent(in EventContext<EventDied> ctx)
        {
            _dataAnimator.Animator.SetBool("IsDead", true);
        }
    }
}
