using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Lottery.Prefabs
{
    public static class Gs2LotteryProbabilityList
    {
        [MenuItem("GameObject/UI/Game Server Services/Lottery/Namespace/User/ProbabilityList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Lottery/Prefabs/ProbabilityList.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}