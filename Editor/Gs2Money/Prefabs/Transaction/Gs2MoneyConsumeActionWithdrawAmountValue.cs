using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Money.Prefabs
{
    public static class Gs2MoneyConsumeActionWithdrawAmountValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Transaction/Money/Consume/WithdrawAmountValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Money/Prefabs/Transaction/WithdrawAmountValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
