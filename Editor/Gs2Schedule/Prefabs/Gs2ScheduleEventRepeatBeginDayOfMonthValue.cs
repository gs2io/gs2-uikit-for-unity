using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Schedule.Prefabs
{
    public static class Gs2ScheduleEventRepeatBeginDayOfMonthValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Schedule/Namespace/User/Event/Label/RepeatBeginDayOfMonthValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Schedule/Prefabs/EventRepeatBeginDayOfMonthValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}