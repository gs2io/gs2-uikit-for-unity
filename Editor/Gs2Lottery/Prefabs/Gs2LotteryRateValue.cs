using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Lottery.Prefabs
{
    public static class Gs2LotteryRateValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Lottery/LotteryModel/Probability/RateValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Lottery/Prefabs/RateValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
