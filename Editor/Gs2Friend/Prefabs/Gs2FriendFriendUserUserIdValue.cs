using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Friend.Prefabs
{
    public static class Gs2FriendFriendUserUserIdValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Friend/Namespace/User/Friend/FriendUser/Label/UserIdValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Friend/Prefabs/FriendUserUserIdValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}