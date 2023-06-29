using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Mission.Prefabs
{
    public static class Gs2MissionCounterValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Mission/Counter/CounterValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Mission/Prefabs/CounterValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
