using System;
using TriInspector;

namespace EasyCS.Samples
{
    [Serializable]
    public class EntityDataAttackDamage : EntityDataBase<float> { }

    [Serializable]
    public class EntityDataAttack : EntityDataCustomBase
    {
        public float attackRange;
        public float attackSpeed;
        [ReadOnly, ShowInPlayMode]
        public float delayToNextAttack;

        public override object Clone()
        {
            return new EntityDataAttack
            {
                attackSpeed = this.attackSpeed,
                delayToNextAttack = this.delayToNextAttack,
                attackRange = this.attackRange
            };
        }
    }
}
