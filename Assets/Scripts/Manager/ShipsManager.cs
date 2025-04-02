using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ShipsManager : BasicComponent
    {
        public ShipController PlayerShip { get; private set; }
        public List<ShipController> AIShips { get; private set; }

        public override void Initialize()
        {
            PlayerShip = FindFirstObjectByType<ShipController>();// test
            PlayerShip.Initialize();
            AIShips = new();
        }

        public override void Destroy()
        {
            PlayerShip.Destroy();
            foreach (ShipController ship in AIShips)
            {
                ship.Destroy();
            }
        }

        public override void Enable()
        {
            PlayerShip.Enable();
            foreach (ShipController ship in AIShips)
            {
                ship.Enable();
            }
        }

        public override void Disable()
        {
            PlayerShip.Disable();
            foreach (ShipController ship in AIShips)
            {
                ship.Disable();
            }
        }

        public override void OnUpdate()
        {
            PlayerShip.OnUpdate();
            foreach (ShipController ship in AIShips)
            {
                ship.OnUpdate();
            }
        }

        public override void OnFixedUpdate()
        {
            PlayerShip.OnFixedUpdate();
            foreach (ShipController ship in AIShips)
            {
                ship.OnFixedUpdate();
            }
        }

        public override void OnLateUpdate()
        {
            PlayerShip.OnLateUpdate();
            foreach (ShipController ship in AIShips)
            {
                ship.OnLateUpdate();
            }
        }

        public void AddShip(ShipController ship)
        {
            ship.Initialize();
            AIShips.Add(ship);
        }

        public void RemoveShip(ShipController ship)
        {
            AIShips.Remove(ship);
            ship.Destroy();
        }
    }
}