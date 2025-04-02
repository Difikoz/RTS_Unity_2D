using UnityEngine;

namespace WinterUniverse
{
    public class UnitController : PawnController
    {
        public float AttackDamage = 10f;
        public float AttackRange = 4f;
        public float Health = 100f;
        protected override float GetStopDistance()
        {
            if (Sensor.TargetIsVisible())
            {
                return AttackRange;
            }
            return base.GetStopDistance();
        }
    }
}