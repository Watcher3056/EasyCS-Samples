using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    public partial class ActorBehaviorAttackMelee : ActorBehavior, IEventListener<EventTryAttack>
    {
        [Bind]
        private EntityDataAttackDamage _dataDamage;
        [Bind]
        private EntityDataTarget _dataTarget;

        public void HandleEvent(in EventContext<EventTryAttack> ctx)
        {
            ExecuteAttack();
        }

        private void ExecuteAttack()
        {
            EventSystem.Raise(new EventDealDamage { damage = _dataDamage.Value }, _dataTarget.Value);
        }
    }
}
