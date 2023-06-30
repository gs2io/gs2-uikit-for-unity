using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Quest.Prefabs
{
    public static class Gs2QuestStartButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Quest/QuestGroup/Quest/StartButton", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Quest/Prefabs/StartButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
