using UnityEngine;

namespace WinterUniverse
{
    public class ContextSolver
    {
        private PawnController _pawn;
        private Vector3 _position;
        private Vector3 _direction;
        private Vector2 _directionToObstacle;
        private float[] _danger;
        private float[] _interest;
        private float _weight;
        private float _result;
        private float _valueToPutIn;
        private readonly int _directions = 8;

        public float[] Danger => _danger;
        public float[] Interest => _interest;

        public ContextSolver(PawnController pawn)
        {
            _pawn = pawn;
            _danger = new float[_directions];
            _interest = new float[_directions];
        }

        public Vector2 GetDirectionToMove(Vector3 position)
        {
            _position = position;
            _danger = new float[_directions];
            _interest = new float[_directions];
            GetSteering();
            for (int i = 0; i < _directions; i++)
            {
                _interest[i] = Mathf.Clamp01(_interest[i] - _danger[i]);
            }
            Vector2 outputDirection = Vector2.zero;
            for (int i = 0; i < _directions; i++)
            {
                outputDirection += Directions.EightDirections[i] * _interest[i];
            }
            outputDirection.Normalize();
            return outputDirection;
        }

        private void GetSteering()
        {
            foreach (Collider2D obstacleCollider in _pawn.Sensor.DetectedObstacles)
            {
                _directionToObstacle = obstacleCollider.ClosestPoint(_pawn.transform.position) - (Vector2)_pawn.transform.position;
                _weight = _directionToObstacle.magnitude <= _pawn.Collider.radius ? 1f : (_pawn.ViewDistance - _directionToObstacle.magnitude) / _pawn.ViewDistance;
                for (int i = 0; i < Directions.EightDirections.Count; i++)
                {
                    _result = Vector2.Dot(_directionToObstacle.normalized, Directions.EightDirections[i]);
                    _valueToPutIn = _result * _weight;
                    if (_valueToPutIn > _danger[i])
                    {
                        _danger[i] = _valueToPutIn;
                    }
                }
            }
            _direction = (_position - _pawn.transform.position).normalized;
            for (int i = 0; i < _interest.Length; i++)
            {
                _result = Vector2.Dot(_direction, Directions.EightDirections[i]);
                if (_result > 0)
                {
                    _valueToPutIn = _result;
                    if (_valueToPutIn > _interest[i])
                    {
                        _interest[i] = _valueToPutIn;
                    }
                }
            }
        }
    }
}