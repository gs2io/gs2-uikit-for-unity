using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Matchmaking.Prefabs
{
    public static class Gs2MatchmakingGatheringCancelMatchmakingButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Matchmaking/Namespace/User/Gathering/Action/CancelMatchmaking", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Matchmaking/Prefabs/GatheringCancelMatchmakingButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}