using UnityEditor;
using UnityEngine;

namespace WinterUniverse
{
    [CustomEditor(typeof(ShipController))]
    public class CE_ShipController : CE_PawnController
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            ShipController ship = (ShipController)target;
            serializedObject.ApplyModifiedProperties();
        }
    }
}