using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class AIControllersManager : BasicComponent
    {
        public List<AIController> Controllers { get; private set; }

        public override void Initialize()
        {
            Controllers = new();
            AIController[] controllers = FindObjectsByType<AIController>(FindObjectsSortMode.None);
            foreach (AIController controller in controllers)
            {
                controller.Initialize();
                AddController(controller);
            }
        }

        public override void Destroy()
        {
            foreach (AIController controller in Controllers)
            {
                controller.Destroy();
            }
        }

        public override void Enable()
        {
            foreach (AIController controller in Controllers)
            {
                controller.Enable();
            }
        }

        public override void Disable()
        {
            foreach (AIController controller in Controllers)
            {
                controller.Disable();
            }
        }

        public override void OnUpdate()
        {
            foreach (AIController controller in Controllers)
            {
                controller.OnUpdate();
            }
        }

        public override void OnFixedUpdate()
        {
            foreach (AIController controller in Controllers)
            {
                controller.OnFixedUpdate();
            }
        }

        public override void OnLateUpdate()
        {
            foreach (AIController controller in Controllers)
            {
                controller.OnLateUpdate();
            }
        }

        public void AddController(AIController controller)
        {
            Controllers.Add(controller);
        }

        public void RemoveController(AIController controller)
        {
            Controllers.Remove(controller);
        }
    }
}