using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Lottery.Prefabs
{
    public static class Gs2LotteryLotteryModelModeValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Lottery/Namespace/LotteryModel/Label/ModeValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Lottery/Prefabs/LotteryModelModeValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}