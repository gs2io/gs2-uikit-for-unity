using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Showcase.Prefabs
{
    public static class Gs2ShowcaseDisplayItemDisplayItemList
    {
        [MenuItem("GameObject/UI/Game Server Services/Showcase/DisplayItemList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Showcase/Prefabs/DisplayItemList.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
