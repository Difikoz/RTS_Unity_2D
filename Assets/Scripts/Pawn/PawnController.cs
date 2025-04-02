using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class PawnController : BasicComponent, ITargetable
    {
        public string Faction;
        public float MoveSpeed = 4f;
        public float RotateSpeed = 90f;
        public float ViewDistance = 10f;
        public float ViewAngle = 90f;
        [SerializeField] protected float _updateDelay = 0.1f;
        protected float _updateTime;

        public Rigidbody2D RB { get; private set; }
        public CircleCollider2D Collider { get; private set; }
        public SensorComponent Sensor { get; private set; }
        public ContextSolver ContexSolver { get; private set; }
        public ITargetable Targetable { get; protected set; }
        public IInteractable Interactable { get; protected set; }
        public Vector3 Destination { get; protected set; }
        public Vector2 MoveDirection { get; protected set; }
        public Vector2 LookDirection { get; protected set; }
        public float LookAngle { get; protected set; }
        public float RemainingDistance { get; protected set; }
        public bool ReachedDestination { get; protected set; }
        public bool IsDead { get; protected set; }

        public override void Initialize()
        {
            RB = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
            Sensor = new(this);
            ContexSolver = new(this);
        }

        public override void Enable()
        {
            SetDestination(transform.position);
            StopMovement();
        }

        public override void OnFixedUpdate()
        {
            if (GameManager.StaticInstance.PauseState == PauseState.Paused)
            {
                RB.linearVelocity = Vector2.zero;
                return;
            }
            if (_updateTime >= _updateDelay)
            {
                HandleDetection();
                CheckTarget();
                CheckInteractable();
                CalculateLookDirection();
                CheckDestination();
                CalculateMoveDirection();
                CalculateLookAngle();
                RB.linearVelocity = MoveDirection.normalized * MoveSpeed;
                _updateTime = 0f;
            }
            else
            {
                _updateTime += Time.fixedDeltaTime;
            }
            RB.rotation = Mathf.MoveTowardsAngle(RB.rotation, LookAngle, RotateSpeed * Time.fixedDeltaTime);
        }

        protected virtual void HandleDetection()
        {
            Sensor.Detect();
        }

        protected virtual void CheckTarget()
        {
            if (Targetable != null)
            {
                if (Targetable.CanBeTargeted(this))
                {
                    Destination = Targetable.GetPosition();
                }
                else
                {
                    StopMovement();
                }
            }
            //else if (Sensor.DetectedTargets.Count > 0)// use for ai chase state
            //{
            //    SetTarget(Sensor.GetClosestTarget());
            //    Destination = Target.transform.position;
            //}
        }

        protected virtual void CheckInteractable()
        {
            if (Interactable != null)
            {
                if (ReachedDestination)
                {
                    if (Interactable.CanInteract(this))
                    {
                        Interactable.Interact(this);
                    }
                    StopMovement();
                }
                else
                {
                    Destination = Interactable.GetInteractionPosition();
                }
            }
            //else if (Sensor.DetectedInteractables.Count > 0)// use for ai chase state
            //{
            //    SetInteractable(Sensor.GetClosestInteractable());
            //    Destination = Interactable.transform.position;
            //}
        }

        protected virtual void CalculateLookDirection()
        {
            LookDirection = (Destination - transform.position).normalized;
        }

        protected virtual void CheckDestination()
        {
            RemainingDistance = Vector2.Distance(transform.position, Destination);
            if (ReachedDestination)
            {
                if (RemainingDistance > GetStopDistance())
                {
                    ReachedDestination = false;
                }
            }
            else
            {
                if (RemainingDistance < GetStopDistance())
                {
                    ReachedDestination = true;
                }
            }
        }

        protected virtual void CalculateMoveDirection()
        {
            if (ReachedDestination)
            {
                MoveDirection = Vector2.zero;
            }
            else
            {
                MoveDirection = ContexSolver.GetDirectionToMove(Destination);
            }
        }

        protected virtual void CalculateLookAngle()
        {
            if (LookDirection != Vector2.zero)
            {
                LookAngle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
            }
        }

        public virtual void SetDestination(Vector2 position, bool resetTargets = true)
        {
            if (resetTargets)
            {
                ResetTargetable();
                ResetInteractable();
            }
            Destination = position;
            ReachedDestination = false;
        }

        public virtual void StopMovement()
        {
            ResetTargetable();
            ResetInteractable();
            Destination = transform.position;
            ReachedDestination = true;
            MoveDirection = Vector2.zero;
            RB.linearVelocity = Vector2.zero;
        }

        public virtual void SetTargetable(ITargetable targetable)
        {
            if (targetable != null && targetable != (ITargetable)this)
            {
                ResetInteractable();
                Targetable = targetable;
                SetDestination(Targetable.GetPosition(), false);
            }
            else
            {
                ResetTargetable();
            }
        }

        public virtual void ResetTargetable()
        {
            Targetable = null;
        }

        public virtual void SetInteractable(IInteractable interactable)
        {
            if (interactable != null)
            {
                ResetTargetable();
                Interactable = interactable;
                SetDestination(Interactable.GetInteractionPosition(), false);
            }
            else
            {
                ResetInteractable();
            }
        }

        public virtual void ResetInteractable()
        {
            Interactable = null;
        }

        protected virtual float GetStopDistance()
        {
            if (Interactable != null)
            {
                return Interactable.GetInteractionDistance();
            }
            return Collider.radius;
        }

        public bool CanBeTargeted(PawnController pawn)
        {
            if (IsDead)
            {
                return false;
            }
            if (pawn == this)
            {
                return false;
            }
            return true;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public string GetFaction()
        {
            return Faction;
        }

        private void OnDrawGizmos()
        {
            if (ContexSolver != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < Directions.EightDirections.Count; i++)
                {
                    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Directions.EightDirections[i] * ContexSolver.Danger[i]);
                }
                Gizmos.color = Color.green;
                for (int i = 0; i < Directions.EightDirections.Count; i++)
                {
                    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Directions.EightDirections[i] * ContexSolver.Interest[i]);
                }
            }
            if (Sensor != null)
            {
                Gizmos.color = Color.magenta;
                foreach (Collider2D obstacle in Sensor.DetectedObstacles)
                {
                    Gizmos.DrawLine(transform.position, obstacle.transform.position);
                }
                foreach (ITargetable targetable in Sensor.DetectedTargets)
                {
                    if (Sensor.TargetIsVisible())
                    {
                        Gizmos.color = Color.yellow;
                    }
                    else
                    {
                        Gizmos.color = Color.grey;
                    }
                    Gizmos.DrawLine(transform.position, targetable.GetPosition());
                }
                Gizmos.color = Color.cyan;
                foreach (IInteractable interactable in Sensor.DetectedInteractables)
                {
                    Gizmos.DrawLine(transform.position, interactable.GetInteractionPosition());
                }
            }
            if (Targetable != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, Targetable.GetPosition());
            }
            if (Interactable != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, Interactable.GetInteractionPosition());
            }
        }
    }
}