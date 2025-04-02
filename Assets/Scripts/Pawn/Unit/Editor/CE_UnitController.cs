using UnityEditor;
using UnityEngine;

namespace WinterUniverse
{
    [CustomEditor(typeof(UnitController))]
    public class CE_UnitController : CE_PawnController
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            UnitController unit = (UnitController)target;
            serializedObject.ApplyModifiedProperties();
        }
    }
}