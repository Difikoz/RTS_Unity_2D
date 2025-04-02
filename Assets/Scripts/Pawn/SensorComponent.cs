using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WinterUniverse
{
    public class SensorComponent
    {
        private PawnController _pawn;
        private List<ITargetable> _targets;
        private List<IInteractable> _interactables;
        private List<Collider2D> _obstacles;

        public List<ITargetable> DetectedTargets { get; private set; }
        public List<IInteractable> DetectedInteractables { get; private set; }
        public List<Collider2D> DetectedObstacles { get; private set; }

        public SensorComponent(PawnController pawn)
        {
            _pawn = pawn;
            _targets = new();
            _interactables = new();
            _obstacles = new();
            DetectedTargets = new();
            DetectedInteractables = new();
            DetectedObstacles = new();
        }

        public void Detect()
        {
            _targets.Clear();
            _interactables.Clear();
            _obstacles.Clear();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_pawn.transform.position, _pawn.ViewDistance, GameManager.StaticInstance.LayersManager.DetectableMask);
            if (colliders.Length > 0)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.TryGetComponent(out ITargetable targetable))
                    {
                        if (targetable != (ITargetable)_pawn)
                        {
                            if (TargetIsEnemy(targetable) && TargetIsVisible(targetable))
                            {
                                _targets.Add(targetable);
                            }
                        }
                    }
                    else if (collider.TryGetComponent(out IInteractable interactable))
                    {
                        _interactables.Add(interactable);
                    }
                    else
                    {
                        _obstacles.Add(collider);
                    }
                }

            }
            //Collider2D[] colliders = Physics2D.OverlapCircleAll(_pawn.transform.position, _pawn.ViewDistance, GameManager.StaticInstance.LayersManager.ObstacleMask);
            //if (colliders.Length > 0)
            //{
            //    foreach (Collider2D collider in colliders)
            //    {
            //        _obstacles.Add(collider);
            //    }
            //}
            //colliders = Physics2D.OverlapCircleAll(_pawn.transform.position, _pawn.ViewDistance, GameManager.StaticInstance.LayersManager.InteractableMask);
            //if (colliders.Length > 0)
            //{
            //    foreach (Collider2D collider in colliders)
            //    {
            //        if (collider.TryGetComponent(out InteractableBase interactable))
            //        {
            //            _interactables.Add(interactable);
            //        }
            //    }
            //}
            //colliders = Physics2D.OverlapCircleAll(_pawn.transform.position, _pawn.ViewDistance, GameManager.StaticInstance.LayersManager.DetectableMask);
            //if (colliders.Length > 0)
            //{
            //    foreach (Collider2D collider in colliders)
            //    {
            //        if (collider.TryGetComponent(out PawnController pawn))
            //        {
            //            if (pawn != _pawn && !pawn.IsDead)
            //            {
            //                if (pawn != _pawn.Target)
            //                {
            //                    _obstacles.Add(collider);// to avoid other pawns
            //                }
            //                if (TargetIsEnemy(pawn) && TargetIsVisible(pawn.transform))
            //                {
            //                    _targets.Add(pawn);
            //                }
            //            }
            //        }
            //    }
            //}
            DetectedTargets = new(_targets);
            DetectedInteractables = new(_interactables);
            DetectedObstacles = new(_obstacles);
        }

        public bool TargetIsVisible(ITargetable targetable)
        {
            if (Vector2.Angle(_pawn.transform.right, (targetable.GetPosition() - _pawn.transform.position).normalized) > _pawn.ViewAngle / 2f)
            {
                return false;
            }
            if (Physics2D.Linecast(_pawn.transform.position, targetable.GetPosition(), GameManager.StaticInstance.LayersManager.ObstacleMask))
            {
                return false;
            }
            return true;
        }

        public bool TargetIsVisible()
        {
            if (_pawn.Targetable == null)
            {
                return false;
            }
            return TargetIsVisible(_pawn.Targetable);
        }

        public bool TargetIsEnemy(ITargetable targetable)
        {
            return targetable.GetFaction() == "";
        }

        public ITargetable GetClosestTarget()
        {
            return DetectedTargets.OrderBy(target => Vector2.Distance(target.GetPosition(), _pawn.transform.position)).FirstOrDefault();
        }
    }
}