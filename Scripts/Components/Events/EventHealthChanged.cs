using EasyCS.EventSystem;

namespace EasyCS.Samples
{
    public struct EventHealthChanged : IEvent
    {
        public readonly float PrevHealth;
        public readonly float CurHealth;
        public float Delta => CurHealth - PrevHealth;

        public EventHealthChanged(float prevHealth, float curHealth)
        {
            PrevHealth = prevHealth;
            CurHealth = curHealth;
        }
    }
}
