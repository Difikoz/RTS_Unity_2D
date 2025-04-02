using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInputManager : BasicComponent
    {
        [SerializeField] private bool _invertMoveInput;

        private PlayerInputActions _inputActions;
        private RaycastHit2D _cameraHit;

        public Vector2 MoveInput { get; private set; }

        public override void Initialize()
        {
            _inputActions = new();
        }

        public override void Enable()
        {
            _inputActions.Enable();
            _inputActions.World.SetDestination.performed += ctx => OnSetDestinationPerfomed();
            _inputActions.World.StopShip.performed += ctx => OnStopMovementPerfomed();
            _inputActions.World.FollowShip.performed += ctx => OnFollowShipPerfomed();
            _inputActions.UI.PauseGame.performed += ctx => OnPauseGamePerfomed();
        }

        public override void Disable()
        {
            _inputActions.World.SetDestination.performed -= ctx => OnSetDestinationPerfomed();
            _inputActions.World.StopShip.performed -= ctx => OnStopMovementPerfomed();
            _inputActions.World.FollowShip.performed -= ctx => OnFollowShipPerfomed();
            _inputActions.UI.PauseGame.performed -= ctx => OnPauseGamePerfomed();
            _inputActions.Disable();
        }

        public override void OnUpdate()
        {
            MoveInput = _invertMoveInput ? -_inputActions.Camera.MoveCamera.ReadValue<Vector2>() : _inputActions.Camera.MoveCamera.ReadValue<Vector2>();
        }

        private void OnSetDestinationPerfomed()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            _cameraHit = Physics2D.Raycast(Camera.main.transform.position, Vector3.forward, float.MaxValue, GameManager.StaticInstance.LayersManager.DetectableMask);
            if (_cameraHit.collider != null && _cameraHit.collider.TryGetComponent(out IInteractable interactable))
            {
                GameManager.StaticInstance.ShipsManager.PlayerShip.SetInteractable(interactable);
            }
            else
            {
                GameManager.StaticInstance.ShipsManager.PlayerShip.SetDestination(GameManager.StaticInstance.CameraManager.transform.position);
            }
        }

        private void OnStopMovementPerfomed()
        {
            GameManager.StaticInstance.ShipsManager.PlayerShip.StopMovement();
        }

        private void OnFollowShipPerfomed()
        {
            GameManager.StaticInstance.CameraManager.FollowShip();
        }

        private void OnPauseGamePerfomed()
        {
            GameManager.StaticInstance.TogglePauseState();
        }
    }
}