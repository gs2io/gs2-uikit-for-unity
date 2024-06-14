using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2LoginReward.Prefabs
{
    public static class Gs2LoginRewardReceiveStatusBonusModelNameValue
    {
        [MenuItem("GameObject/UI/Game Server Services/LoginReward/Namespace/User/ReceiveStatus/Label/BonusModelNameValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2LoginReward/Prefabs/ReceiveStatusBonusModelNameValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}