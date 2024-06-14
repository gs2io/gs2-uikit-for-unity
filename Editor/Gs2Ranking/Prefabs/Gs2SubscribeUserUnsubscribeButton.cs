using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Ranking.Prefabs
{
    public static class Gs2RankingSubscribeUserUnsubscribeButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Ranking/Namespace/User/RankingCategory/SubscribeUser/Action/Unsubscribe", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Ranking/Prefabs/SubscribeUserUnsubscribeButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}