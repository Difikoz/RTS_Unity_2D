using UnityEngine;

namespace WinterUniverse
{
    public interface ITargetable
    {
        public bool CanBeTargeted(PawnController pawn);
        public Vector3 GetPosition();
        public string GetFaction();
    }
}