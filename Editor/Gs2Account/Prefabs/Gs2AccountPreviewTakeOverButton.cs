using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Account.Prefabs
{
    public static class Gs2AccountAccountPreviewTakeOverButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Account/Namespace/Account/Action/PreviewTakeOver", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Account/Prefabs/AccountPreviewTakeOverButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}