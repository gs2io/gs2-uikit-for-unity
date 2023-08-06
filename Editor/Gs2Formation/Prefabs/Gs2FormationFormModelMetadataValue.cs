using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Formation.Prefabs
{
    public static class Gs2FormationFormModelMetadataValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Formation/Namespace/FormModel/Label/MetadataValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Formation/Prefabs/FormModelMetadataValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}