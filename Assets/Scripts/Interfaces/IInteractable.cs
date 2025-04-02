using UnityEngine;

namespace WinterUniverse
{
    public interface IInteractable
    {
        public float GetInteractionDistance();
        public Vector3 GetInteractionPosition();
        public bool CanInteract(PawnController pawn);
        public void Interact(PawnController pawn);
    }
}