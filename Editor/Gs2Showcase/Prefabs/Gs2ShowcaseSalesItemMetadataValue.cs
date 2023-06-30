using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Showcase.Prefabs
{
    public static class Gs2ShowcaseSalesItemMetadataValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Showcase/DisplayItem/SalesItemMetadataValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Showcase/Prefabs/SalesItemMetadataValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
