using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : BasicComponent
    {
        [SerializeField] private float _followSpeed = 10f;
        [SerializeField] private float _moveSpeed = 10f;

        private bool _followShip;

        public override void OnLateUpdate()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            if (GameManager.StaticInstance.PlayerInputManager.MoveInput != Vector2.zero)
            {
                transform.Translate(GameManager.StaticInstance.PlayerInputManager.MoveInput * _moveSpeed * Time.deltaTime);
                _followShip = false;
            }
            else if (_followShip)
            {
                transform.position = Vector3.Lerp(transform.position, GameManager.StaticInstance.ShipsManager.PlayerShip.transform.position, _followSpeed * Time.deltaTime);
            }
        }

        public void FollowShip()
        {
            _followShip = true;
        }
    }
}