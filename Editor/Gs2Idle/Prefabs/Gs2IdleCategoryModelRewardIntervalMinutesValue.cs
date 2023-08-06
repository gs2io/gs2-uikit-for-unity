using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Idle.Prefabs
{
    public static class Gs2IdleCategoryModelRewardIntervalMinutesValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Idle/Namespace/CategoryModel/Label/RewardIntervalMinutesValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Idle/Prefabs/CategoryModelRewardIntervalMinutesValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}