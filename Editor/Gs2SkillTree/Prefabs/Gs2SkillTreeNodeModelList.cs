using UnityEditor;
using UnityEngine;

namespace Editor.Gs2SkillTree.Prefabs
{
    public static class Gs2SkillTreeNodeModelList
    {
        [MenuItem("GameObject/UI/Game Server Services/SkillTree/Namespace/NodeModelList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2SkillTree/Prefabs/NodeModelList.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}