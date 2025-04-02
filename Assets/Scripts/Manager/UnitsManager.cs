using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class UnitsManager : BasicComponent
    {
        public List<UnitController> Units { get; private set; }

        public override void Initialize()
        {
            Units = new();
        }

        public override void Destroy()
        {
            foreach (UnitController unit in Units)
            {
                unit.Destroy();
            }
        }

        public override void Enable()
        {
            foreach (UnitController unit in Units)
            {
                unit.Enable();
            }
        }

        public override void Disable()
        {
            foreach (UnitController unit in Units)
            {
                unit.Disable();
            }
        }

        public override void OnUpdate()
        {
            foreach (UnitController unit in Units)
            {
                unit.OnUpdate();
            }
        }

        public override void OnFixedUpdate()
        {
            foreach (UnitController unit in Units)
            {
                unit.OnFixedUpdate();
            }
        }

        public override void OnLateUpdate()
        {
            foreach (UnitController unit in Units)
            {
                unit.OnLateUpdate();
            }
        }

        public void AddUnit(UnitController unit)
        {
            unit.Initialize();
            Units.Add(unit);
        }

        public void RemoveUnit(UnitController unit)
        {
            Units.Remove(unit);
            unit.Destroy();
        }
    }
}