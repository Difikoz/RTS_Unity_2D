using UnityEngine;

namespace WinterUniverse
{
    public class AIController : BasicComponent
    {
        public ShipController Ship { get; private set; }

        public override void Initialize()
        {
            // spawn ship
            GameManager.StaticInstance.ShipsManager.AddShip(Ship);
        }

        public override void Destroy()
        {
            GameManager.StaticInstance.ShipsManager.RemoveShip(Ship);
        }
    }
}