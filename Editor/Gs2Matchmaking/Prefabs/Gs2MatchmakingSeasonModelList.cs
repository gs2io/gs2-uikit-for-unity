using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Matchmaking.Prefabs
{
    public static class Gs2MatchmakingSeasonModelList
    {
        [MenuItem("GameObject/UI/Game Server Services/Matchmaking/Namespace/SeasonModelList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Matchmaking/Prefabs/SeasonModelList.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}