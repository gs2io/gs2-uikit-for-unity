using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Ranking.Prefabs
{
    public static class Gs2RankingRankingPutScoreButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Ranking/Namespace/User/Ranking/Action/PutScore", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Ranking/Prefabs/RankingPutScoreButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}