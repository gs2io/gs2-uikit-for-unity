using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Limit.Prefabs
{
    public static class Gs2LimitCounterValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Limit/Counter/CounterValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Limit/Prefabs/CounterValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
