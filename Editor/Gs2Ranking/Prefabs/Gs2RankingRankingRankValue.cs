using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Ranking.Prefabs
{
    public static class Gs2RankingRankingRankValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Ranking/Namespace/User/Ranking/Label/RankValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Ranking/Prefabs/RankingRankValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}