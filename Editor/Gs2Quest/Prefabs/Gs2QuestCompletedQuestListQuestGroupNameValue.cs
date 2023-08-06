using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Quest.Prefabs
{
    public static class Gs2QuestCompletedQuestListQuestGroupNameValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Quest/Namespace/User/CompletedQuestList/Label/QuestGroupNameValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Quest/Prefabs/CompletedQuestListQuestGroupNameValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}