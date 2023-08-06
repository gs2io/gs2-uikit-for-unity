using UnityEditor;
using UnityEngine;

namespace Editor.Gs2LoginReward.Prefabs
{
    public static class Gs2LoginRewardBonusReceiveButton
    {
        [MenuItem("GameObject/UI/Game Server Services/LoginReward/Namespace/User/Bonus/Action/Receive", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2LoginReward/Prefabs/BonusReceiveButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}