using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Buff.Prefabs
{
    public static class Gs2BuffBuffEntryModelList
    {
        [MenuItem("GameObject/UI/Game Server Services/Buff/Namespace/BuffEntryModelList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Buff/Prefabs/BuffEntryModelList.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}