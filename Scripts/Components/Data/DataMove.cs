using System;
using UnityEngine;

namespace EasyCS.Samples
{
    [Serializable, RuntimeOnly]
    public class EntityDataMove : EntityDataBase<Vector3>
    {

    }

    [Serializable]
    public class EntityDataMoveSpeed : EntityDataBase<float>
    {

    }
}
