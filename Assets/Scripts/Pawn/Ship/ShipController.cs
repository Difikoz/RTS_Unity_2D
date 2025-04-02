using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ShipController : PawnController, IInteractable
    {
        public List<UnitController> Units { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            Units = new();
        }

        public void AddUnit(string config)
        {
            UnitController unit = new();//spawn
            //unit.Initialize(config, Faction);
            GameManager.StaticInstance.UnitsManager.AddUnit(unit);
        }

        public void RemoveUnit(UnitController unit)
        {
            Units.Remove(unit);
            GameManager.StaticInstance.UnitsManager.RemoveUnit(unit);
        }

        public float GetInteractionDistance()
        {
            return Collider.radius;
        }

        public Vector3 GetInteractionPosition()
        {
            return transform.position;
        }

        public bool CanInteract(PawnController pawn)
        {
            return pawn != this;
        }

        public void Interact(PawnController pawn)
        {
            Debug.Log($"[{pawn.gameObject.name}] interacted with [{gameObject.name}]");
        }
    }
}