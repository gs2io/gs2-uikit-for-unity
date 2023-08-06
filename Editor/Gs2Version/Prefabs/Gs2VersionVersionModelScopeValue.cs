using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Version.Prefabs
{
    public static class Gs2VersionVersionModelScopeValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Version/Namespace/VersionModel/Label/ScopeValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Version/Prefabs/VersionModelScopeValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}