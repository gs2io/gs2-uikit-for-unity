using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Formation.Prefabs
{
    public static class Gs2FormationPropertyFormDeletePropertyFormButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Formation/Namespace/User/PropertyForm/Action/DeletePropertyForm", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Formation/Prefabs/PropertyFormDeletePropertyFormButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}