
using System;
using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    [Serializable]
    public class EntityBehaviorHealth : EntityBehaviorBase, IAwake, IEventListener<EventDealDamage>
    {
        [Bind]
        private EntityDataHealth _health;
        [Bind]
        private EntityDataHealthMax _healthMax;

        public bool IsAlive => _health.Value > 0f;

        public void OnAwake()
        {
            ResetToMax();
        }

        public void HandleEvent(in EventContext<EventDealDamage> ctx)
        {
            ApplyHealthDelta(-ctx.Event.damage);
        }

        public void ApplyHealthDelta(float delta)
        {
            SetHealth(_health.Value + delta);
        }

        public void SetHealth(float health)
        {
            bool isAliveBefore = IsAlive;
            float prevHealth = _health.Value;

            SetHealthSilent(health);
            float delta = _health.Value - prevHealth;


            EventSystem.Raise(new EventHealthChanged(prevHealth, _health.Value), Entity);

            if (IsAlive == false && isAliveBefore)
            {
                EventSystem.Raise(new EventDied(), Entity);
            }
        }

        public void ResetToMax()
        {
            float prevHealth = _health.Value;
            SetHealthSilent(_healthMax.Value);

            EventSystem.Raise(new EventHealthChanged(prevHealth, _health.Value), Entity);
        }

        public void SetHealthSilent(float health)
        {
            _health.Value = health;
        }
    }
}