using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    public struct EventAttack : IEvent
    {
        public readonly Entity Target;
        
        public EventAttack(Entity target) => Target = target;
    }
}