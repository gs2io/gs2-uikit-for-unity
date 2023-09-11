using UnityEditor;
using UnityEngine;

namespace Editor.Gs2SkillTree.Prefabs
{
    public static class Gs2SkillTreeStatusResetButton
    {
        [MenuItem("GameObject/UI/Game Server Services/SkillTree/Namespace/User/Status/Action/Reset", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2SkillTree/Prefabs/StatusResetButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}