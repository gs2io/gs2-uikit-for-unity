using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Chat.Prefabs
{
    public static class Gs2ChatCategoryModelRejectAccessTokenPostValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Chat/Namespace/CategoryModel/Label/RejectAccessTokenPostValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Chat/Prefabs/CategoryModelRejectAccessTokenPostValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}