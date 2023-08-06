using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Account.Prefabs
{
    public static class Gs2AccountTakeOverCreatedAtCountDownValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Account/Namespace/Account/TakeOver/Label/CreatedAtCountDownValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Account/Prefabs/TakeOverCreatedAtCountDownValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}