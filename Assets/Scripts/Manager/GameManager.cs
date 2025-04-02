using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private List<BasicComponent> _components;
        public InputMode InputMode { get; private set; }
        public PauseState PauseState { get; private set; }
        public AIControllersManager AIControllersManager { get; private set; }
        public CameraManager CameraManager { get; private set; }
        public LayersManager LayersManager { get; private set; }
        public PlayerInputManager PlayerInputManager { get; private set; }
        public ShipsManager ShipsManager { get; private set; }
        public UIManager UIManager { get; private set; }
        public UnitsManager UnitsManager { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _components = new();
            AIControllersManager = GetComponentInChildren<AIControllersManager>();
            CameraManager = GetComponentInChildren<CameraManager>();
            LayersManager = GetComponentInChildren<LayersManager>();
            PlayerInputManager = GetComponentInChildren<PlayerInputManager>();
            ShipsManager = GetComponentInChildren<ShipsManager>();
            UIManager = GetComponentInChildren<UIManager>();
            UnitsManager = GetComponentInChildren<UnitsManager>();
            _components.Add(AIControllersManager);
            _components.Add(CameraManager);
            _components.Add(LayersManager);
            _components.Add(PlayerInputManager);
            _components.Add(ShipsManager);
            _components.Add(UIManager);
            _components.Add(UnitsManager);
            foreach (BasicComponent component in _components)
            {
                component.Initialize();
            }
            SetInputMode(InputMode.Game);
            SetPauseState(PauseState.Unpaused);
        }

        private void OnDestroy()
        {
            foreach (BasicComponent component in _components)
            {
                component.Destroy();
            }
        }

        private void OnEnable()
        {
            foreach (BasicComponent component in _components)
            {
                component.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (BasicComponent component in _components)
            {
                component.Disable();
            }
        }

        private void Update()
        {
            foreach (BasicComponent component in _components)
            {
                component.OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            foreach (BasicComponent component in _components)
            {
                component.OnFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            foreach (BasicComponent component in _components)
            {
                component.OnLateUpdate();
            }
        }

        public void SetInputMode(InputMode mode)
        {
            InputMode = mode;
            switch (InputMode)
            {
                case InputMode.Game:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                case InputMode.UI:
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    break;
            }
        }

        public void TogglePauseState()
        {
            switch (PauseState)
            {
                case PauseState.Paused:
                    SetPauseState(PauseState.Unpaused);
                    break;
                case PauseState.Unpaused:
                    SetPauseState(PauseState.Paused);
                    break;
            }
        }

        public void SetPauseState(PauseState state)
        {
            PauseState = state;
            switch (PauseState)
            {
                case PauseState.Paused:
                    //...
                    break;
                case PauseState.Unpaused:
                    //...
                    break;
            }
        }
    }
}