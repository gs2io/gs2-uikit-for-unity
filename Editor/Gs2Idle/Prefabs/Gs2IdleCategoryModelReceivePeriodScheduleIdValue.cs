using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Idle.Prefabs
{
    public static class Gs2IdleCategoryModelReceivePeriodScheduleIdValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Idle/Namespace/CategoryModel/Label/ReceivePeriodScheduleIdValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Idle/Prefabs/CategoryModelReceivePeriodScheduleIdValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}