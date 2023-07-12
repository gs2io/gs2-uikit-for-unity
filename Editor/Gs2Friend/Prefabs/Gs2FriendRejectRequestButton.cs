using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Friend.Prefabs
{
    public static class Gs2FriendRejectRequestButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Friend/ReceiveRequest/RejectRequestButton", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Friend/Prefabs/RejectRequestButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
