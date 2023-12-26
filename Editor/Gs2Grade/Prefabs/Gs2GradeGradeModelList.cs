using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Grade.Prefabs
{
    public static class Gs2GradeGradeModelList
    {
        [MenuItem("GameObject/UI/Game Server Services/Grade/Namespace/GradeModelList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Grade/Prefabs/GradeModelList.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}