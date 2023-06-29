using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Inbox.Prefabs
{
    public static class Gs2InboxReadButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Inbox/Message/ReadButton", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Inbox/Prefabs/ReadButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
