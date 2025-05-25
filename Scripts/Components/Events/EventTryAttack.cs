using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    public struct EventTryAttack : IEvent
    {
        public readonly Entity Target;
        
        public EventTryAttack(Entity target) => Target = target;
    }
}