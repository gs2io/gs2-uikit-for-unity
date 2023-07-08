using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Money.Prefabs
{
    public static class Gs2MoneyWalletPaidValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Money/Wallet/WalletPaidValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Money/Prefabs/MoneyWalletPaidValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
