using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Experience.Prefabs
{
    public static class Gs2ExperienceExperienceValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Experience/Experience/Status/ExperienceValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Experience/Prefabs/ExperienceValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
