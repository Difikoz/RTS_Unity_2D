using UnityEditor;
using UnityEngine;

namespace WinterUniverse
{
    [CustomEditor(typeof(PawnController))]
    public class CE_PawnController : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            PawnController pawn = (PawnController)target;
            GUILayout.Label("===== Basic Info =====");
            //if (pawn.Data != null)
            //{
            //    GUILayout.Label($"Name => {pawn.Data.DisplayName}");
            //}
            GUILayout.Label("===== Status =====");
            //if (pawn.Status != null)
            //{
            //    GUILayout.Label($"Health => {pawn.Status.HealthPercent * 100f}%");
            //    GUILayout.Label($"Energy => {pawn.Status.EnergyPercent * 100f}%");
            //    GUILayout.Label($"Mana => {pawn.Status.ManaPercent * 100f}%");
            //}
            GUILayout.Label("===== States =====");
            //if (pawn.StateHolder != null)
            //{
            //    foreach (KeyValuePair<string, bool> state in pawn.StateHolder.States)
            //    {
            //        GUILayout.Label($"{state.Key} : {state.Value}");
            //    }
            //}
            GUILayout.Label("===== Combat =====");
            //if (pawn.Combat != null)
            //{
            //    GUILayout.Label($"Target => {(pawn.Combat.Target != null ? pawn.Combat.Target.gameObject.name : "Empty")}");
            //    GUILayout.Label($"Distance => {pawn.Combat.DistanceToTarget}");
            //    GUILayout.Label($"Angle => {pawn.Combat.AngleToTarget}");
            //}
            GUILayout.Label("===== Stats =====");
            //if (pawn.Status != null)
            //{
            //    foreach (Stat stat in pawn.Status.Stats)
            //    {
            //        GUILayout.Label($"{stat.Config.DisplayName}: {stat.CurrentValue:0.##}{(stat.Config.IsPercent ? "%" : "")}");
            //    }
            //}
            GUILayout.Label("===== Effects =====");
            //if (pawn.Effects != null)
            //{
            //    foreach (Effect effect in pawn.Effects.AllEffects)
            //    {
            //        GUILayout.Label($"{effect.Config.DisplayName}: {effect.Duration:0.0}");
            //    }
            //}
            GUILayout.Label("===== Locomotion =====");
            GUILayout.Label($"Move Direction => {pawn.MoveDirection}");
            if (pawn.Targetable != null)
            {
                GUILayout.Label($"Target => {pawn.Targetable}");
            }
            if (pawn.Interactable != null)
            {
                GUILayout.Label($"Interactable => {pawn.Interactable}");
            }
            if (pawn.ReachedDestination)
            {
                GUILayout.Label($"Reached Destination");
            }
            else
            {
                GUILayout.Label($"Remaining Distance => {pawn.RemainingDistance}");
            }
            //if (pawn.Locomotion != null)
            //{
            //    if (pawn.Locomotion.FollowTarget != null)
            //    {
            //        GUILayout.Label($"Is Following Target");
            //    }
            //    if (pawn.Locomotion.ReachedDestination)
            //    {
            //        GUILayout.Label($"Reached Destination");
            //    }
            //    else
            //    {
            //        GUILayout.Label($"Remaining Distance => {pawn.Locomotion.RemainingDistance}");
            //    }
            //}
            GUILayout.Label("===== Equipment =====");
            //if (pawn.Equipment != null)
            //{
            //    GUILayout.Label($"Weapon: {(pawn.Equipment.WeaponSlot.Config != null ? pawn.Equipment.WeaponSlot.Config.DisplayName : "Empty")}");
            //    foreach (ArmorSlot slot in pawn.Equipment.ArmorSlots)
            //    {
            //        GUILayout.Label($"{slot.Type.DisplayName}: {(slot.Config != null ? slot.Config.DisplayName : "Empty")}");
            //    }
            //}
            GUILayout.Label("===== Inventory =====");
            //if (pawn.Inventory != null)
            //{
            //    foreach (ItemStack stack in pawn.Inventory.Stacks)
            //    {
            //        GUILayout.Label($"{stack.Item.DisplayName}: {stack.Amount}");
            //    }
            //}
            GUILayout.Label("===== Detection =====");
            if (pawn.Sensor != null)
            {
                if (pawn.Sensor.DetectedObstacles != null)
                {
                    foreach (Collider2D collider in pawn.Sensor.DetectedObstacles)
                    {
                        GUILayout.Label($"Obstacles: {collider.gameObject.name}");
                    }
                }
                if (pawn.Sensor.DetectedTargets != null)
                {
                    foreach (PawnController target in pawn.Sensor.DetectedTargets)
                    {
                        GUILayout.Label($"Targets: {target.gameObject.name}");
                    }
                }
            }
            //if (pawn.Detection != null)
            //{
            //    foreach (PawnController target in pawn.Detection.DetectedEnemies)
            //    {
            //        GUILayout.Label($"Enemy: {target.gameObject.name}");
            //    }
            //    foreach (PawnController target in pawn.Detection.DetectedNeutrals)
            //    {
            //        GUILayout.Label($"Neutral: {target.gameObject.name}");
            //    }
            //    foreach (PawnController target in pawn.Detection.DetectedAllies)
            //    {
            //        GUILayout.Label($"Ally: {target.gameObject.name}");
            //    }
            //}
            serializedObject.ApplyModifiedProperties();
        }
    }
}