using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class SpaceStation : BasicComponent, IInteractable
    {
        public Rigidbody2D RB { get; private set; }
        public CircleCollider2D Collider { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public override void Initialize()
        {
            RB = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
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
            return true;
        }

        public void Interact(PawnController pawn)
        {
            Debug.Log($"[{pawn.gameObject.name}] interacted with [{gameObject.name}]");
        }
    }
}