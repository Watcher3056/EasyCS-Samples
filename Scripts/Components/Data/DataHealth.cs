using System;

namespace EasyCS.Samples
{
    [Serializable, RuntimeOnly]
    public class EntityDataHealth : EntityDataBase<float> { }
    [Serializable]
    public class EntityDataHealthMax : EntityDataBase<float> { }
}
