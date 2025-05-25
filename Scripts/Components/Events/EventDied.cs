
using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    public struct EventDied : IEvent
    {
        public Entity Entity => entity;
        public Entity entity;
    }
}
