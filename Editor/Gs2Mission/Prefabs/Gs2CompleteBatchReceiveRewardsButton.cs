using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Mission.Prefabs
{
    public static class Gs2MissionCompleteBatchReceiveRewardsButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Mission/Namespace/User/Complete/Action/BatchReceiveRewards", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Mission/Prefabs/CompleteBatchReceiveRewardsButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}