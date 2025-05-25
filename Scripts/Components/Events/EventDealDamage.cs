using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    public struct EventDealDamage : IEvent
    {
        public float damage;
        
        public EventDealDamage(float damage) => this.damage = damage;
    }
}
